using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DefaultNamespace;
using Pathfinding;
using UnityEngine;

namespace State.Turn
{
    public class ActionPhase : TurnState
    {
        private class UnitAction
        {
            public Unit Unit { get; }
            public Vector3 MoveTarget { get; set; }
            public Unit EnemyTarget { get; set; }
            
            public UnitAction(Unit unit) => Unit = unit;
        }
        
        public ActionPhase(Context context) : base(context) {}

        public override async void Enter(TurnState previous)
        {
            List<UnitAction> actions = new List<UnitAction>();
            Context.OurUnits.ForEach(unit =>
            {
                GraphNode startNode = AstarPath.active.GetNearest(unit.gameObject.transform.position).node;
                UnitAction unitUnitAction = new UnitAction(unit);
                unitUnitAction.MoveTarget = unit.DefaultTarget;

                PathUtilities.BFS(startNode, unit.Stats.Movement.Value, -1, node =>
                {
                    if (!node.Walkable) return false;

                    if (!unitUnitAction.EnemyTarget)
                    {
                        Unit enemy = GetEnemyUnitAtNode(node, unit);
                        if (enemy)
                        {
                            GraphNode freeSpace = GetFreeAdjacentSpace(node);
                            unitUnitAction.MoveTarget = (Vector3)freeSpace.position;
                            unitUnitAction.EnemyTarget = enemy;
                        }
                    }

                    return true;
                });

                actions.Add(unitUnitAction);
            });

            await DoAction(actions);
        }

        private async Task DoAction(List<UnitAction> actions)
        {
            foreach (UnitAction unitAction in actions)
            {
                await unitAction.Unit.Move(unitAction.MoveTarget);
                if(unitAction.EnemyTarget)  
                    await unitAction.Unit.Attack(unitAction.EnemyTarget);
            }

            Context.GameManager.NextTurn(Context);
        }

        private Unit GetEnemyUnitAtNode(GraphNode node, Unit ourUnit)
        {
            RaycastHit[] hits = new RaycastHit[1];
            int unitHits      = Physics.RaycastNonAlloc((Vector3) node.position, Vector3.up, hits, 1f, Context.UnitLayersMask);

            for (int i = 0; i < unitHits; i++)
            {
                Unit otherUnit = hits[i].collider.gameObject.GetComponent<Unit>();
                if (otherUnit.IsPlayerUnit != ourUnit.IsPlayerUnit)
                    return otherUnit;
            }

            return null;
        }

        private GraphNode GetFreeAdjacentSpace(GraphNode node)
        {
            GraphNode freeNode = null;

            node.GetConnections(connectedNode =>
            {
                RaycastHit[] hits = new RaycastHit[1];
                int unitHits      = Physics.RaycastNonAlloc((Vector3) node.position, Vector3.up, hits, 1f, Context.UnitLayersMask);
                if (unitHits == 0)
                    freeNode = connectedNode;
            });

            return freeNode;
        }
    }
}