using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    [Serializable]
    public class EntityEquipment: IEnumerable<Equipment>, IEntityEquipment
    {
        [field: SerializeField] public HatScriptable Hat { get; set; } = null;
        [field: SerializeField] public WeaponScriptable Weapon { get; set; } = null;
        [field: SerializeField] public WingsScriptable Wings { get; set; } = null;
        
        public IEnumerator<Equipment> GetEnumerator() {
            return (new List<Equipment>() { 
                Hat, Weapon, Wings
            }).GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }
}