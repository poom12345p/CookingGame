using System;
using UnityEngine;

public static class GameHelper 
{
    public static bool isAllowCooking(MenuData data)
    {

        var valid = GameManager.Instance.CookingController.IsCurrentCookingCompete;

        foreach (var ingredientData in data.Ingredients)
        {
            valid &= GameManager.Instance.PlayerData.GetItemQuantity(ingredientData.Ingredient.Key) >= ingredientData.Amount;
        }

        valid &= GameManager.Instance.PlayerData.Energy > GameConst.Energy.EnergyPerCooking;
        
        return valid;
    }

}
