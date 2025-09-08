using System;
using PrimeTween;
using UnityEngine;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{
    public Canvas Canvas=>_canvas;
    
    [SerializeField] protected Canvas _canvas;
    [SerializeField] protected GraphicRaycaster _raycaster;
    [SerializeField]protected CanvasGroup _canvasGroup;
    
    private Tween _tween;

    public void Show()
    {
        if(gameObject.activeInHierarchy)return;
        gameObject.SetActive(true);
        _raycaster.enabled = true;
        _canvasGroup.alpha = 0f;
        _tween.Stop();
        _tween = Tween.Alpha(_canvasGroup, 1f,0.5f);
       
    }

    public void Hide()
    {
        if(!gameObject.activeInHierarchy)return;

        _tween = HideTween();
        _tween.OnComplete(
            () =>
            {
                gameObject.SetActive(false);
            }
        );
    }
    
    public void Close()
    {
        if(!gameObject.activeInHierarchy)return;
        _tween = HideTween();
        _tween.OnComplete(() => Destroy(this.gameObject),true);
    }
    
    public void HideImmediately()
    {
        if(!gameObject.activeInHierarchy)return;
        _canvasGroup.alpha = 0f;
        gameObject.SetActive(false);
    }

    private Tween HideTween()
    {
        _raycaster.enabled = false;
        _canvasGroup.alpha = 1f;
        _tween.Stop();
        var newtween = Tween.Alpha(_canvasGroup, 0f,0.5f);
        return newtween;
    }
}
