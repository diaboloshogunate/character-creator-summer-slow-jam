﻿using UnityEngine;

namespace DefaultNamespace
{
    
    [CreateAssetMenu(fileName = "Wings", menuName = "Equipment/Wings")]
    public class WingsScriptable : EquipmentScriptable
    {
        public override EquipmentTypes Types { get => EquipmentTypes.WINGS; }
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