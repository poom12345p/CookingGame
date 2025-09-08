using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingController  :IGameController
{
    #region struct

    public enum CookingState
    {
        IDLE = 0,
        COOKING =1
    }
    public struct CookingData
    {
        public MenuData MenuData;
        public DateTime StartTime;
    }

    #endregion

    public CookingState CurrentCookingState =>cookingState;
    
    #region callback

    public Action<CookingData> OnStartCooking;
    public Action<float> OnCookingTimeUpdate;
    public Action<MenuData> OnCompleteCooking;

    #endregion
    
    private Coroutine cookingCoroutine;
    private CookingState cookingState;

    private CookingData currentCookingData = new()
    {
        MenuData = null,
        StartTime = DateTime.Now,
    };
    
    public bool IsCurrentCookingCompete=> currentCookingData.MenuData == null || (DateTime.Now-currentCookingData.StartTime).TotalSeconds > currentCookingData.MenuData.CookingTime ; 
    
    
    public void starCooking(MenuData menuData)
    {
        if(!GameHelper.isAllowCooking(menuData)) return;
        if(menuData == null)return;
        var playerData = GameManager.Instance.PlayerData;
        foreach (var ingredientData in  menuData.Ingredients)
        {
            playerData.RemoveItemInInventory(ingredientData.Ingredient.Key, ingredientData.Amount);
        }

        currentCookingData = new CookingData()
        {
            MenuData = menuData,
            StartTime = DateTime.Now,
        };
        
        GameManager.Instance.PlayerDataController.UseEnergy(GameConst.Energy.EnergyPerCooking);
        cookingState = CookingState.COOKING;
        OnStartCooking?.Invoke(currentCookingData);
        cookingCoroutine = GameManager.Instance.StartCoroutine(cooking());
    }
    

    public IEnumerator cooking()
    {
        while (( DateTime.Now-currentCookingData.StartTime).TotalSeconds < currentCookingData.MenuData.CookingTime)
        {
           var timeleft =currentCookingData.MenuData.CookingTime -( DateTime.Now-currentCookingData.StartTime).TotalSeconds;
            OnCookingTimeUpdate?.Invoke((float)timeleft);
            yield return null;
        }
        
        OnCookingTimeUpdate?.Invoke(0f);
        cookingState = CookingState.IDLE;
        OnCompleteCooking?.Invoke(currentCookingData.MenuData);
    }

    #region GameController
    
    public void subScribeEventHandlers() { }

    public void startGame()
    {
        var playerData = GameManager.Instance.PlayerData;
        if(!GameManager.Instance.MenuDatabase.TryGetValue(playerData.CookingSave.MenuKey, out MenuData menuData)) return;
        currentCookingData = new CookingData()
        {
            MenuData = menuData,
            StartTime = DateTime.FromBinary(playerData.CookingSave.StartTime)
        };
        cookingState = CookingState.COOKING;
        cookingCoroutine = GameManager.Instance.StartCoroutine(cooking());
    }

    public void onClear() { }
    #endregion
}
