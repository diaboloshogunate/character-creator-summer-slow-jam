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
        [field: SerializeField] public BaseBaseEntityStats BaseStats { get; private set; } = null;
        [field: SerializeField] public EntityEquipment Equipment { get; private set; } = null;
        public EntityStats Stats { get; private set; } = null;
        [field: SerializeField] private Tilemap Tilemap { get; set; } = null; // todo promote into a service for managing/selecting tiles

        public Seeker Seeker { get; private set; } = null;
        public AILerp AILerp { get; private set; } = null;

        private void Start()
        {
            Seeker = GetComponent<Seeker>();
            AILerp = GetComponent<AILerp>();
            UpdateMinStats();
            UpdateMaxStats();
            SetStatsValueToMax();
        }

        public void OnEnable()
        {
            UpdateMinStats();
            UpdateMaxStats();
            Equipment.AddedEvent += OnEquipmentAddedEvent;
            Equipment.RemovedEvent += OnEquipmentRemovedEvent;
        }

        public void OnDisable()
        {
            Equipment.AddedEvent -= OnEquipmentAddedEvent;
            Equipment.RemovedEvent -= OnEquipmentRemovedEvent;
        }
        
        private void OnEquipmentAddedEvent(EquipmentScriptable arg0) => UpdateMaxStats();
        private void OnEquipmentRemovedEvent(EquipmentScriptable arg0) => UpdateMaxStats();

        public void Move(Vector3 destination)
        {
            Vector3 cellCenterPosition = Tilemap.GetCellCenterWorld(Tilemap.WorldToCell(destination));
            
            Path p = Seeker.StartPath (transform.position, cellCenterPosition);
            p.BlockUntilCalculated();// force path to calculate now instead of multithreading
            if (p.GetTotalLength() > Stats.Movement.Value) return;
            AILerp.SetPath(p);
        }

        public void Damage(int amt) => Stats.Health.Value -= Mathf.Max(Stats.Defence.Value - amt, 0);
        
        public void Heal(int amt) => Stats.Health.Value += Mathf.Max(amt, 0);

        public void Die() => Destroy(gameObject);

        public void Attack(Entity entity) => entity.Damage(Stats.Attack.Value);

        private void UpdateMinStats()
        {
            Stats.Health.Min   = 0;
            Stats.Movement.Min = 0;
            Stats.Attack.Min   = 0;
            Stats.Defence.Min  = 0;
        }

        private void UpdateMaxStats()
        {
            Stats.Health.Max   = BaseStats.HealthStat + Equipment.Sum(equipment => equipment.HealthModifier);
            Stats.Movement.Max = BaseStats.MovementStat + Equipment.Sum(equipment => equipment.MovementModifier);
            Stats.Attack.Max   = BaseStats.AttackStat + Equipment.Sum(equipment => equipment.AttackModifier);
            Stats.Defence.Max  = BaseStats.DefenceStat + Equipment.Sum(equipment => equipment.DefenceModifier);
        }

        private void SetStatsValueToMax()
        {
            Stats.Health.Value   = Stats.Health.Max;
            Stats.Movement.Value = Stats.Movement.Max;
            Stats.Attack.Value   = Stats.Attack.Max;
            Stats.Defence.Value  = Stats.Defence.Max;
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