﻿using UnityEngine;

namespace DefaultNamespace
{
    
    [CreateAssetMenu(fileName = "Equipment", menuName = "Equipment/Hat")]
    public class HatScriptable : EquipmentScriptable
    {
        public override EquipmentTypes Types { get => EquipmentTypes.HAT; }
        public override int HealthModifier { get => health; }
        public override int MovementModifier { get => movement; }
        public override int AttackModifier { get => attack; }
        public override int DefenceModifier { get => defence; }
        public override GameObject Prefab { get => prefab; }
        
        [SerializeField] private GameObject prefab;
        [SerializeField] private int health = 0;
        [SerializeField] private int movement = 0;
        [SerializeField] private int attack = 0;
        [SerializeField] private int defence = 0;
    }
}