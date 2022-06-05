using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using DG.Tweening;
namespace DefaultNamespace{
    public class CardModel : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler,IPointerEnterHandler,IPointerExitHandler
    {
        [SerializeField]
        private string _name;
        [SerializeField]
        private Image _image;
        [SerializeField]
        private CardType _type;

        [SerializeField]
        private string space;
        [SerializeField]
        private string player;
        [SerializeField]
        private float scale;

        private Transform _grid;
        private Camera _camera;

        void Start(){
            _grid = transform.parent;
            _camera = Camera.main;
        }
        
        public void OnBeginDrag(PointerEventData pointerData){
            transform.SetParent(transform.parent.parent);
        }
        public void OnDrag(PointerEventData pointerData){
            transform.position = Input.mousePosition;
        }
        public void OnEndDrag(PointerEventData pointerData){
            
            Vector3 dropPosition;
            if(!DropOrReturn(out dropPosition)){
                transform.SetParent(_grid);
                return;
            }
            DropAnim();
        }

        public void OnPointerEnter(PointerEventData pointerData){
            SelectionAnim();
        }
        public void OnPointerExit(PointerEventData pointerData){
            DeselctionAnim();
        }
        
        private bool DropOrReturn(out Vector3 dropPosition){
            dropPosition = Vector3.zero;
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 100)){
                Debug.Log(hit.collider.name);
                dropPosition = hit.collider.gameObject.transform.position;
                if(_type == CardType.GEAR){
                    Debug.Log("Checking Gear");
                    if(hit.collider.CompareTag(player))
                        hit.collider.GetComponent<Entity>().AddGear(GetComponent<GearProp>());
                        return true;
                }
                else{
                    Debug.Log("Checking Trap");
                    if(hit.collider.CompareTag(space))
                        return true;
                }
                    
            }
            return false;

        }

        //-Anim
        private async void SelectionAnim(){
            GetComponent<RectTransform>().DOScale(Vector3.one*scale,.2f);
            await GetComponent<RectTransform>().DOBlendablePunchRotation(Vector3.one*2,.25f).AsyncWaitForCompletion();
        }
        private async void DeselctionAnim(){
            await GetComponent<RectTransform>().DOScale(Vector3.one,.2f).AsyncWaitForCompletion();
        }
        private async void DropAnim(){
            await GetComponent<RectTransform>().DOBlendablePunchRotation(Vector3.one*5,.25f,20).AsyncWaitForCompletion();
            await GetComponent<RectTransform>().DOScale(Vector3.zero,.5f).AsyncWaitForCompletion();
        }
    }

    public enum CardType{
        GEAR,
        TRAP,
        MOB
    }
}
