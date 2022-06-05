using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace DefaultNamespace{
    public class GearProp : MonoBehaviour
    {
        [SerializeField]
        private GearType type;
        public GearType Type{get=>type;}
        [SerializeField]
        private float health;
        public float Health{get=>health;}
        [SerializeField]
        private float attack;
        public float Attack{get=>attack;}
        [SerializeField]
        private float defense;
        public float Defense{get=>defense;}
        [SerializeField]
        private float movement;
        public float Movement{get=>movement;}
    }

    public enum GearType{
        WEAPON,
        ARMOR,
        CLOTHING,
        SHOES
    }
}
