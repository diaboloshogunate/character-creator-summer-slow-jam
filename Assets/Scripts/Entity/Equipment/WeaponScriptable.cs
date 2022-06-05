using UnityEngine;

namespace DefaultNamespace
{
    
    [CreateAssetMenu(fileName = "Weapon", menuName = "Equipment/Weapon")]
    public class WeaponScriptable : EquipmentScriptable
    {
        public override EquipmentTypes Types { get => EquipmentTypes.WEAPON; }
        public override int HealthModifier { get => health; }
        public override int MovementModifier { get => movement; }
        public override int AttackModifier { get => attack; }
        public override int DefenceModifier { get => defence; }
        
        [SerializeField] private int health = 0;
        [SerializeField] private int movement = 0;
        [SerializeField] private int attack = 0;
        [SerializeField] private int defence = 0;
    }
}