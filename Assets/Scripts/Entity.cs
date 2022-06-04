﻿using System;
using Pathfinding;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace DefaultNamespace
{
    
    [RequireComponent(typeof(Seeker))]
    [RequireComponent(typeof(AILerp))]
    public class Entity : MonoBehaviour, IEntityStats, IEntityActions
    {
        [field: Header("Stats")]
        [field: SerializeField] public float HealthStat { get; private set; } = 10;
        [field: SerializeField] public float MovementStat { get; private set; } = 5;
        [field: SerializeField] public float AttackStat { get; private set; } = 1;
        [field: SerializeField] public float DefenceStat { get; private set; } = 1;
        
        [field: Header("Gear")]
        // todo

        [field: Header("References")]
        [field: SerializeField] private Tilemap Tilemap { get; set; } = null;// todo promote into a service for managing/selecting tiles

        public Seeker Seeker { get; private set; } = null;
        public AILerp AILerp { get; private set; } = null;

        private void Start()
        {
            Seeker = GetComponent<Seeker>();
            AILerp = GetComponent<AILerp>();
        }

        public void Move(Vector3 destination)
        {
            Vector3 cellCenterPosition = Tilemap.GetCellCenterWorld(Tilemap.WorldToCell(destination));
            
            Path p = Seeker.StartPath (transform.position, cellCenterPosition);
            p.BlockUntilCalculated();// force path to calculate now instead of multithreading
            if (p.GetTotalLength() > MovementStat) return;
            AILerp.SetPath(p);
        }

        public void Damage(float amt)
        {
            HealthStat -= amt;
            if (HealthStat <= 0) Die();
        }

        public void Die()
        {
            Destroy(gameObject);
        }

        public void Attack(Entity entity)
        {
            entity.Damage(AttackStat);
        }
    }
}