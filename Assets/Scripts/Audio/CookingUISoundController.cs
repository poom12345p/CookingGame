using System;
using UnityEngine;

public class CookingUISoundController : MonoBehaviour
{
   [SerializeField] private AudioClip cookingCompleteClip;
   [SerializeField] private AudioClip cookingStartClip;

   private void OnEnable()
   {
      GameManager.Instance.CookingController.OnStartCooking += playStartSound;
      GameManager.Instance.CookingController.OnCompleteCooking += playCompleteSound;
   }

   private void OnDisable()
   {
      GameManager.Instance.CookingController.OnStartCooking -= playStartSound;
      GameManager.Instance.CookingController.OnCompleteCooking -= playCompleteSound;
   }

   private void playStartSound(CookingController.CookingData  cookingData)
   {
      SoundManager.Instance.PlaySound(cookingStartClip);
   }
   private void playCompleteSound(MenuData menuData)
   {
      SoundManager.Instance.PlaySound(cookingCompleteClip);
   }
   
}
