using System;
using System.Linq;
using UnityEngine;

namespace DefaultNamespace
{
    [Serializable]
    public class CalculatedEntityStats: IEntityStats
    {
        [field: SerializeField] public EntityStats Stats { get; private set; } = null;
        [field: SerializeField] public EntityEquipment Equipment { get; private set; } = null;
        public int HealthStat { get => Stats.HealthStat + Equipment.Sum(equipment => equipment.HealthModifier); }
        public int MovementStat { get => Stats.MovementStat + Equipment.Sum(equipment => equipment.MovementModifier); }
        public int AttackStat { get => Stats.AttackStat + Equipment.Sum(equipment => equipment.AttackModifier); }
        public int DefenceStat { get => Stats.DefenceStat + Equipment.Sum(equipment => equipment.DefenceModifier); }
    }
}