using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace{
    public class EquipmentUI : MonoBehaviour
    {
        private Image icon;
        [SerializeField]
        private Sprite[] sprites;
        public EquipmentScriptable equipmentScriptable{get;private set;}
        // Start is called before the first frame update
        public void Setup(EquipmentScriptable equipmentScriptable){
            this.equipmentScriptable = equipmentScriptable;
            icon = transform.Find("Image").GetComponent<Image>();
            if(equipmentScriptable is HatScriptable)  icon.sprite = sprites[0];
            else if(equipmentScriptable is WeaponScriptable)  icon.sprite = sprites[1];
            else if (equipmentScriptable is WingsScriptable)  icon.sprite = sprites[2];
            else return;
           

        }
    }
}

