using System;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

public class CookPotAnimController : MonoBehaviour
{
    [SerializeField] private SkeletonGraphic skeletonGraphic;
    [SerializeField] private SpineGraphicAnimationController skeletonGraphicAnimationController;
    [SerializeField]private SpineGraphicAnimationController.SpineAnimationData idleAnimation;
    [SerializeField]private SpineGraphicAnimationController.SpineAnimationData cookIdleAnimation;
    [SerializeField]private List<SpineGraphicAnimationController.SpineAnimationData> startCookAnimation;
    [SerializeField]private List<SpineGraphicAnimationController.SpineAnimationData> endCookAnimation;
    

    private void Start()
    {
        setIdleAnimation();

    }
    
    #region enable controll
    private void OnEnable()
    {
        GameManager.Instance.CookingController.OnStartCooking += setStartCookAnimation;
        GameManager.Instance.CookingController.OnCompleteCooking += setEndCookAnimation;
    }
    private void OnDisable()
    {
        if(!this.gameObject.scene.isLoaded) return;
        GameManager.Instance.CookingController.OnStartCooking -= setStartCookAnimation;
        GameManager.Instance.CookingController.OnCompleteCooking -= setEndCookAnimation;
    }    

    #endregion

    #region setAnimation
    private void setStartCookAnimation(CookingController.CookingData data)
    {
        skeletonGraphicAnimationController.setAnimation(startCookAnimation);
        skeletonGraphicAnimationController.addAnimation( getIdle());
    }
    
   
    private void setEndCookAnimation(MenuData data)
    {
        skeletonGraphicAnimationController.setAnimation(endCookAnimation);
        skeletonGraphicAnimationController.addAnimation( getIdle());
    }
    
    private void setIdleAnimation()
    {
            skeletonGraphicAnimationController.setAnimation(getIdle());
    }

    private SpineGraphicAnimationController.SpineAnimationData getIdle()
    {
        switch (GameManager.Instance.CookingController.CurrentCookingState)
        {
            case CookingController.CookingState.IDLE:
                return idleAnimation;

            case CookingController.CookingState.COOKING:
                return cookIdleAnimation;
            default:
                return idleAnimation;
        }
    }

    #endregion
    
}
