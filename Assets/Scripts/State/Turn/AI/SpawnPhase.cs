using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using Pathfinding;
using UnityEngine;

namespace State.Turn.AI
{
    public class SpawnPhase : TurnState
    {
        public SpawnPhase(Context context) : base(context) {}

        public override void Enter(TurnState previous)
        {
            GridGraph grid = AstarPath.active.data.gridGraph;
            List<GridNode> spawnPoints = grid.nodes.Where(node =>
            {
                if (!node.Walkable) return false;
                if (!Context.EnemySpawnBounds.bounds.Contains((Vector3)node.position)) return false;
                RaycastHit[] collisions = new RaycastHit[1];
                if (Physics.RaycastNonAlloc((Vector3)node.position, Vector3.up, collisions, 1f, Context.UnitLayersMask) > 0) return false;

                return true;
            }).ToList();

            int maxUnitsSpawned = Mathf.Min(Context.Turn, 10, spawnPoints.Count);
            int howManyUnitsToSpawn = Random.Range(0, maxUnitsSpawned);
            for (int i = 0; i <= howManyUnitsToSpawn; i++)
            {
                int spawnIndex = Random.Range(0, spawnPoints.Count);
                GraphNode spawnNode = spawnPoints[spawnIndex];
                spawnPoints.RemoveAt(spawnIndex);

                Unit unit = Context.UnitFactory.BuildUnit(false);
                unit.gameObject.transform.position = (Vector3)spawnNode.position;
                Context.OurUnits.Add(unit);
            }
            
            Context.StateMachine.Transition(new ActionPhase(Context));
        }
    }
}