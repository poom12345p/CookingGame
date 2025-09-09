using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObject/MenuData")]
[Serializable]
public class MenuData : ScriptableObject
{
   [field: SerializeField] public ItemData itemData { get; private set; } =null;
   [field: SerializeField] public List<MenuIngredientData>  Ingredients  { get; private set; } = new ();
   [field: SerializeField] public int Rarity { get; private set; } = 1;
   [field: SerializeField] public int CookingTime { get; private set; } = 10;

   public void setItemData(ItemData newItemData)
   {
      this.itemData = newItemData;
   }

}
