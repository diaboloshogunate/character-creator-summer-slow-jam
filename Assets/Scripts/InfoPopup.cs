using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.UI;


using DG.Tweening;
namespace DefaultNamespace{
    public class InfoPopup : MonoBehaviour
    {
        private CanvasGroup _popup;
        private Text _health,_movement,_attack,_defense;
        private Camera _camera;
        private bool _isSelected =false;
        private bool  _isActive=false;
        // Start is called before the first frame update
        void Start()
        {
            _popup = GetComponent<CanvasGroup>();
            _health = transform.Find("Health").GetComponent<Text>();
            _attack = transform.Find("Attack").GetComponent<Text>();
            _defense = transform.Find("Defense").GetComponent<Text>();
            _movement = transform.Find("Movement").GetComponent<Text>();

            _camera = Camera.main;
        }

        // Update is called once per frame
        void Update()
        {
            if(!_isSelected)
                CheckWorldSpace();
        }
        private async void CheckWorldSpace(){
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 100,LayerMask.GetMask("Object"))){
                _isActive = true;
                Entity entity = hit.collider.GetComponent<Entity>();
                if(entity == null)
                    return;
                _health.text = "Health:"+entity.HealthStat;
                _attack.text = "Health:"+entity.AttackStat;
                _defense.text = "Health:"+entity.DefenceStat;
                _movement.text = "Health:"+entity.MovementStat;
                await SelectionAnim();
                return;
            }
            if(_isActive){
                _isActive = false;
                await DeselectionAnim();
            }

        }


        //-Anim
        private async Task<bool> SelectionAnim(){
            await _popup.DOFade(1,.6f).AsyncWaitForCompletion();
            return true;
        }
        private async Task<bool> DeselectionAnim(){
            await _popup.DOFade(0,.6f).AsyncWaitForCompletion();
            return true;
        }
    }
}