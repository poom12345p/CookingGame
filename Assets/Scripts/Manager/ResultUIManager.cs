using System;
using UnityEngine;

public class ResultUIManager : MonoBehaviour
{
   [SerializeField] CookingResultUI cookingResultUIPrefab;

   private void OnEnable()
   {
      GameManager.Instance.CookingController.OnCompleteCooking += ShowResultUI;
   }

   private void OnDisable()
   {
      if(!gameObject.scene.isLoaded) return;
      GameManager.Instance.CookingController.OnCompleteCooking -= ShowResultUI;
   }

   private void ShowResultUI(MenuData menuData)
   {
      var ui = Instantiate(cookingResultUIPrefab);
      ui.HideImmediately();
      ui.setDisplay(menuData);
      ui.Show();
   }

}
