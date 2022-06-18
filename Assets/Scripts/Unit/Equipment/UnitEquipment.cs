using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace
{
    [Serializable]
    public class UnitEquipment: IEnumerable<EquipmentScriptable>, IUnitEquipment
    {
        [field: SerializeField] public HatScriptable Hat { get; private set; } = null;
        [field: SerializeField] public WeaponScriptable Weapon { get; private set; } = null;
        [field: SerializeField] public WingsScriptable Wings { get; private set; } = null;

        public UnityAction<EquipmentScriptable> AddedEvent;
        public UnityAction<EquipmentScriptable> RemovedEvent;

        public void AddEquipment(EquipmentScriptable equipmentScriptable)
        {
            if(equipmentScriptable is HatScriptable) Hat = (HatScriptable) equipmentScriptable;
            else if(equipmentScriptable is WeaponScriptable) Weapon = (WeaponScriptable) equipmentScriptable;
            else if (equipmentScriptable is WingsScriptable) Wings = (WingsScriptable) equipmentScriptable;
            else return;

            AddedEvent?.Invoke(equipmentScriptable);
        }

        public void RemoveEquipment(EquipmentScriptable equipmentScriptable)
        {
            if (Hat == equipmentScriptable) Hat = null;
            else if (Weapon == equipmentScriptable) Weapon = null;
            else if (Wings == equipmentScriptable) Wings = null;
            else return;
            RemovedEvent?.Invoke(equipmentScriptable);
        }

        public IEnumerator<EquipmentScriptable> GetEnumerator() {
            return (new List<EquipmentScriptable>() { 
                Hat, Weapon, Wings
            }).GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }
}