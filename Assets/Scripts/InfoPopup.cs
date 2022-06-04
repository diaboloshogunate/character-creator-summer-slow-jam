using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;
public class InfoPopup : MonoBehaviour
{
    private CanvasGroup _popup;
    private Camera _camera;
    private bool _isSelected =false;
    private bool  _isActive=false;
    // Start is called before the first frame update
    void Start()
    {
        _popup = GetComponent<CanvasGroup>();
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
