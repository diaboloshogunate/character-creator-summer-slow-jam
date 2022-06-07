using System.Linq;
using Pathfinding;
using UnityEngine;

namespace DefaultNamespace
{
    
    [RequireComponent(typeof(Seeker))]
    [RequireComponent(typeof(AILerp))]
    public class Entity : MonoBehaviour, IEntityActions
    {
        [field: SerializeField] public BaseBaseEntityStats BaseStats { get; private set; } = null;
        [field: SerializeField] public EntityEquipment Equipment { get; private set; } = null;
        public EntityStats Stats { get; private set; } = new EntityStats();

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
            Vector3 cellCenterPosition = GameManager.Instance.Tilemap.GetCellCenterWorld(GameManager.Instance.Tilemap.WorldToCell(destination));
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
            Stats.Health.Max   = BaseStats.HealthStat + Equipment.Sum(equipment => equipment ? equipment.HealthModifier : 0);
            Stats.Movement.Max = BaseStats.MovementStat + Equipment.Sum(equipment => equipment ? equipment.MovementModifier : 0);
            Stats.Attack.Max   = BaseStats.AttackStat + Equipment.Sum(equipment => equipment ? equipment.AttackModifier : 0);
            Stats.Defence.Max  = BaseStats.DefenceStat + Equipment.Sum(equipment => equipment ? equipment.DefenceModifier : 0);
        }

        private void SetStatsValueToMax()
        {
            Stats.Health.Value   = Stats.Health.Max;
            Stats.Movement.Value = Stats.Movement.Max;
            Stats.Attack.Value   = Stats.Attack.Max;
            Stats.Defence.Value  = Stats.Defence.Max;
        }
    }
}