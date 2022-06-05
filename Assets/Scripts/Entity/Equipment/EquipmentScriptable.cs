using UnityEngine;

namespace DefaultNamespace
{
    public abstract class EquipmentScriptable : ScriptableObject
    {
        public abstract EquipmentTypes Types { get; }
        public abstract int HealthModifier { get; }
        public abstract int MovementModifier { get; }
        public abstract int AttackModifier { get; }
        public abstract int DefenceModifier { get; }
    }
}