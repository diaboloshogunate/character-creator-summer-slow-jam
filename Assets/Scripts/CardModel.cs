using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        [SerializeField] 
        private EquipmentScriptable _equipmentScriptable;

        private Transform _grid;
        private GameObject _highlight;
        private Camera _camera;

        void Start(){
            _grid = transform.parent;
            _highlight = transform.Find("Highlight").gameObject;
            _camera = Camera.main;

            _highlight.SetActive(false);
            GetComponent<RectTransform>().DOMoveY(0,.2f);
        }
        
        public void OnBeginDrag(PointerEventData pointerData){
            transform.SetParent(transform.parent.parent);
        }
        public void OnDrag(PointerEventData pointerData){
            transform.position = Input.mousePosition;
        }
        public async void OnEndDrag(PointerEventData pointerData){
            
            Vector3 dropPosition;
            if(!DropOrReturn(out dropPosition)){
                transform.SetParent(_grid);
                return;
            }
            await DropAnim();
            gameObject.SetActive(false);
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
                if(_type == CardType.EQUIPMENT){
                    Debug.Log("Checking Gear");
                    if(hit.collider.CompareTag(player))
                        hit.collider.GetComponent<Entity>().Equipment.AddEquipment(_equipmentScriptable);
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
            _highlight.SetActive(true);
            GetComponent<RectTransform>().DOScale(Vector3.one*scale,.2f);
            GetComponent<RectTransform>().DOMoveY(100,.2f);
            await GetComponent<RectTransform>().DOBlendablePunchRotation(Vector3.one*2,.25f).AsyncWaitForCompletion();
        }
        private async void DeselctionAnim(){
            _highlight.SetActive(false);
            GetComponent<RectTransform>().DOMoveY(0,.2f);
            GetComponent<RectTransform>().DOScale(Vector3.one,.2f);
            
        }
        private async Task<bool> DropAnim(){
            await GetComponent<RectTransform>().DOBlendablePunchRotation(Vector3.one*5,.25f,20).AsyncWaitForCompletion();
            await GetComponent<RectTransform>().DOScale(Vector3.zero,.5f).AsyncWaitForCompletion();
            return true;
        }
    }

    public enum CardType{
        EQUIPMENT,
        TRAP,
        MOB
    }
}
