using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CookingDetailUI : MonoBehaviour,IDataDisplay<MenuData>
{
   [SerializeField]MultiIngredientsDisplay multiIngredientsDisplay;
   [SerializeField]Button startButton;
   [SerializeField]TMP_Text timeLeftText;
   [SerializeField]FilledBarUI filledEnergyBar;
   [SerializeField]TMP_Text energyText;
   private MenuData menuData;
   public MenuData Data()
   {
      return menuData;
   }

   private void OnEnable()
   {
      startButton.onClick.AddListener(()=>
      {
         GameManager.Instance.CookingController.starCooking(menuData);
      });
      GameManager.Instance.CookingController.OnStartCooking += onUpdateIngredient;
      GameManager.Instance.CookingController.OnCookingTimeUpdate += onUpdateTimeLeft;
      GameManager.Instance.CookingController.OnCompleteCooking +=  onUpdateStartButton;
      GameManager.Instance.PlayerDataController.OnEnergyUsed += onEnergyUpdate;
      GameManager.Instance.PlayerDataController.OnEnergyRegen += onEnergyUpdate;
      filledEnergyBar.SetMaxValue(GameConst.Energy.MaxEnergy);
      onEnergyUpdate(GameManager.Instance.PlayerData.Energy);
      onUpdateStartButton(menuData);
   }

   public void setDisplay(MenuData data)
   {
      menuData = data;
      multiIngredientsDisplay.setDisplayItems(data.Ingredients); 
      onUpdateStartButton(data);
      
   }

   #region update callback
   public void onUpdateIngredient(CookingController.CookingData data)
   {
      setDisplay(menuData); 
   }
   public void onUpdateStartButton(MenuData data)
   {

      if (menuData == null)
      {
         startButton.interactable = false;
         return;
      }
      startButton.interactable = GameHelper.isAllowCooking(menuData);
   }

   public void onUpdateTimeLeft(float timeLeft)
   {
      TimeSpan t = TimeSpan.FromSeconds( timeLeft);

      string timeFormat = string.Format("{0:D2}:{1:D2}",
         t.Minutes,
         t.Seconds);
      timeLeftText.SetText( timeFormat);
   }
   public void onEnergyUpdate(int energy)
   {
      filledEnergyBar.UpdateValue(energy);
      energyText.SetText($"{energy}/{GameConst.Energy.MaxEnergy}");
   }

   #endregion

   private void OnDisable()
   {
      if(!this.gameObject.scene.isLoaded) return;
      GameManager.Instance.CookingController.OnStartCooking -= onUpdateIngredient;
      GameManager.Instance.CookingController.OnCookingTimeUpdate -= onUpdateTimeLeft;
      GameManager.Instance.CookingController.OnCompleteCooking -=  onUpdateStartButton;
      GameManager.Instance.PlayerDataController.OnEnergyUsed -= onEnergyUpdate;
      GameManager.Instance.PlayerDataController.OnEnergyRegen -= onEnergyUpdate;
   }
}
