using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardModel : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    [SerializeField]
    private string name;
    [SerializeField]
    private Image image;
    [SerializeField]
    private CardType type;

    [SerializeField]
    private string space;
    [SerializeField]
    private string player;

    private Transform grid;
    private Camera camera;

    void Start(){
        grid = transform.parent;
        camera = Camera.main;
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
            transform.SetParent(grid);
            return;
        }
        Debug.Log("Card Dropped");
    }
    
    private bool DropOrReturn(out Vector3 dropPosition){
        dropPosition = Vector3.zero;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 100)){
            Debug.Log(hit.collider.name);
            dropPosition = hit.point;
            if(type == CardType.GEAR){
                Debug.Log("Checking Gear");
                if(hit.collider.CompareTag(player))
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
}

public enum CardType{
    GEAR,
    TRAP,
    MOB
}
