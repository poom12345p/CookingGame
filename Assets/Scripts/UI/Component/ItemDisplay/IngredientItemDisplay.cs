using TMPro;
using UnityEngine;

public class IngredientItemDisplay : ItemDisplay,IDataDisplay<MenuIngredientData>
{
   [SerializeField] TMP_Text requireText;
   private MenuIngredientData menuIngredientData;
   public void setDisplay(MenuIngredientData data)
   {
       menuIngredientData = data;
      setDisplay(data.Ingredient);
      requireText.SetText($"{GameManager.Instance.PlayerData.GetItemQuantity(data.Ingredient.Key)}/{data.Amount}");
   }

   public new MenuIngredientData Data()
   {
     return menuIngredientData;
   }
   
}
