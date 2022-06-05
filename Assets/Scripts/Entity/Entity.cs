using System;
using System.Collections.Generic;
using System.Linq;
using Pathfinding;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace DefaultNamespace
{
    
    [RequireComponent(typeof(Seeker))]
    [RequireComponent(typeof(AILerp))]
    public class Entity : MonoBehaviour, IEntityActions
    {
        [field: SerializeField] public CalculatedEntityStats Stats { get; private set; } = null;
        [field: SerializeField] private Tilemap Tilemap { get; set; } = null; // todo promote into a service for managing/selecting tiles

        public Seeker Seeker { get; private set; } = null;
        public AILerp AILerp { get; private set; } = null;

        private void Start()
        {
            Seeker = GetComponent<Seeker>();
            AILerp = GetComponent<AILerp>();
            
            props = new List<GearProp>();
        }

        public void Move(Vector3 destination)
        {
            Vector3 cellCenterPosition = Tilemap.GetCellCenterWorld(Tilemap.WorldToCell(destination));
            
            Path p = Seeker.StartPath (transform.position, cellCenterPosition);
            p.BlockUntilCalculated();// force path to calculate now instead of multithreading
            if (p.GetTotalLength() > Stats.MovementStat) return;
            AILerp.SetPath(p);
        }

        public void Damage(int amt)
        {
            // BaseStats.HealthStat -= amt;
            // if (Stats.HealthStat <= 0) Die();
        }

        public void Die()
        {
            Destroy(gameObject);
        }

        public void Attack(Entity entity)
        {
            entity.Damage(Stats.AttackStat);
        }

        public void AddGear(GearProp prop){
            HealthStat +=prop.Health;
            AttackStat +=prop.Attack;
            DefenceStat +=prop.Defense;
            MovementStat += prop.Movement;
            props.Add(prop);
        }
    }
}