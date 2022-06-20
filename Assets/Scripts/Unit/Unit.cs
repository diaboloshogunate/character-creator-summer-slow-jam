using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using Pathfinding;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Tilemaps;

namespace DefaultNamespace
{
    
    [RequireComponent(typeof(Seeker))]
    [RequireComponent(typeof(AILerp))]
    public class Unit : MonoBehaviour, IUnitActions
    {
        [field: SerializeField] public bool IsPlayerUnit { get; private set; }= true;
        [field: SerializeField] public Vector3 DefaultTarget { get; set; } = Vector3.zero;
        [field: SerializeField] public BaseBaseEntityStats BaseStats { get; private set; } = null;
        [field: SerializeField] public UnitEquipment Equipment { get; private set; } = null;
        public EntityStats Stats { get; private set; } = new EntityStats();

        public Seeker Seeker { get; private set; } = null;
        public AILerp AILerp { get; private set; } = null;

        [SerializeField] private Transform wingHolder;

        private void Awake()
        {
            Seeker = GetComponent<Seeker>();
            AILerp = GetComponent<AILerp>();
        }

        private void Start()
        {
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
        
        private void OnEquipmentAddedEvent(EquipmentScriptable arg0) {UpdateMaxStats(); AddGearMesh(arg0);}
        private void OnEquipmentRemovedEvent(EquipmentScriptable arg0) {UpdateMaxStats(); RemoveGearMesh(arg0);}

        public async Task Move(Vector3 destination)
        {
            Tilemap map = GameManager.Instance.Tilemap;
            Vector3 cellCenterPosition = map.GetCellCenterWorld(map.WorldToCell(destination));
            
            Path fullPath = Seeker.StartPath(transform.position, cellCenterPosition);
            fullPath.BlockUntilCalculated();// force path to calculate now instead of multithreading
            float cost = fullPath.path.Sum(node => fullPath.GetTraversalCost(node));
            while (cost > Stats.Movement.Value)
            {
                fullPath.path.RemoveAt(fullPath.path.Count - 1);
                cost = fullPath.path.Sum(node => fullPath.GetTraversalCost(node));
            }
            
            Path reducedPath = Seeker.StartPath(transform.position, (Vector3) fullPath.path.Last().position);
            reducedPath.BlockUntilCalculated();// force path to calculate now instead of multithreading
            Vector3 targetPosition = (Vector3)reducedPath.path.Last().position;
            float distanceFromTarget = Vector3.Distance(transform.position, targetPosition);

            while (distanceFromTarget >= 0.1f)
            {
                distanceFromTarget = Vector3.Distance(transform.position, targetPosition);
                await Task.Yield();
            }
        }

        public async Task Damage(int amt)
        {
            Stats.Health.Value -= Mathf.Max(Stats.Defence.Value - amt, 0);
        }

        public async Task Heal(int amt)
        {
            Stats.Health.Value += Mathf.Max(amt, 0);
        }

        public async Task Die()
        {
            Destroy(gameObject);
        }

        public async Task Attack(Unit unit)
        {
            unit.Damage(Stats.Attack.Value);
        }

        public Task Refresh()
        {
            // todo
            return null;
        }

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

        private async void AddGearMesh(EquipmentScriptable arg0){
            GameObject obj = Instantiate(arg0.Prefab,transform) as GameObject;
            if(arg0 is HatScriptable) obj.transform.SetParent(wingHolder);
            else if(arg0 is WeaponScriptable) obj.transform.SetParent(wingHolder);
            else if (arg0 is WingsScriptable)obj.transform.SetParent(wingHolder);
            obj.transform.position = Vector3.zero;
            obj.transform.rotation = Quaternion.identity;

            
            await obj.transform.DOScale(Vector3.one,.5f).AsyncWaitForCompletion();
            obj.transform.DOPunchRotation(Vector3.one*3,.2f);
        }
        private void RemoveGearMesh(EquipmentScriptable arg0){
            if(arg0 is HatScriptable) Remove(wingHolder.GetChild(0).gameObject);
            else if(arg0 is WeaponScriptable) Remove(wingHolder.GetChild(0).gameObject);
            else if (arg0 is WingsScriptable) Remove(wingHolder.GetChild(0).gameObject);
        }

        private async void Remove(GameObject item){
            item.transform.DOShakeScale(.5f,.5f);
            await item.transform.DOScale(Vector3.zero,1f).AsyncWaitForCompletion();         
            Destroy(item);
        }

    }
}