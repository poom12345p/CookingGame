using System;
using UnityEngine;
using UnityEngine.UI;

public class CookingResultUI : BaseUI,IDataDisplay<MenuData>
{
   [SerializeField]Button closeButton;
   [SerializeField] private MenuItemDisplay itemDisplay;
   private MenuData menuData;
   
   private void Awake()
   {
      closeButton.onClick.AddListener(Close);
   }
   

   public MenuData Data()
   {
      return menuData;
   }

   public void setDisplay(MenuData data)
   {
      menuData = Data();
      itemDisplay.setDisplay(data);
   }
}
