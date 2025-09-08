using System;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

public class SpineGraphicAnimationController : MonoBehaviour
{
   [Serializable]
   public struct SpineAnimationData
   {
      [SpineAnimation]
      public string animation;

      public bool Loop;
   }
   
   [SerializeField] private SkeletonGraphic skeletonGraphic;

   public void setAnimation( List<SpineAnimationData> animationDatas)
   {
      skeletonGraphic.AnimationState.SetAnimation(0, animationDatas[0].animation,animationDatas[0].Loop);

      for (int i = 1; i <animationDatas.Count; i++)
      {
         skeletonGraphic.AnimationState.AddAnimation(0,animationDatas[i].animation, animationDatas[i].Loop,0);
      }
   }
   
   public void setAnimation( SpineAnimationData animationDatas)
   {

         skeletonGraphic.AnimationState.SetAnimation(0,animationDatas.animation, animationDatas.Loop);
   }
   
   public void addAnimation( SpineAnimationData animationDatas)
   {
      skeletonGraphic.AnimationState.AddAnimation(0,animationDatas.animation, animationDatas.Loop,0);
   }
   
}
