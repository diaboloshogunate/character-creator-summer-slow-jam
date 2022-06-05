using System;
using UnityEngine;

namespace DefaultNamespace
{
    [Serializable]
    public class EntityStats: IEntityStats
    {
        [field: SerializeField] public int HealthStat { get; private set; }
        [field: SerializeField] public int MovementStat { get; private set; }
        [field: SerializeField] public int AttackStat { get; private set; }
        [field: SerializeField] public int DefenceStat { get; private set; }
    }
}