using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEditor;
using UnityEngine;
[Serializable]
public class PlayerData 
{
    [Serializable]
    public struct CookingSaveData
    {
        public string MenuKey;
        public long StartTime;
    }
    
    [Serializable]
    public struct ItemSaveData
    {
        public string MenuKey;
        public int Amount;
    }



    public List<ItemSaveData> itemsList = new();
    public CookingSaveData CookingSave;
    public int Energy;
    public  long  LastesEnergeyRegenTime;
   
    private Dictionary<string, int> items = new ();
    public PlayerData()
    {
        items = new ()
        {
            {"rice",10},
            {"carrot",10},
            {"vegetable",10},
            {"egg",10}
            
        };
        Energy = 100;
        CookingSave = new CookingSaveData();
        LastesEnergeyRegenTime = DateTime.Now.ToBinary();
    }

    #region inventory

        public int GetItemQuantity(string key)
        {
            return items.GetValueOrDefault(key);
        }
        
        public void AddItemInInventory(string itemID, int amount)
        {
            if(amount <0)return;
            if(!items.TryGetValue(itemID, out var qtt))return;
            items[itemID] += amount;
        }
        
        public void RemoveItemInInventory(string itemID, int amount)
        {
            if(amount <0)return;
            if(!items.TryGetValue(itemID, out var qtt))return;
            items[itemID] -= amount;
        }

    #endregion


    public void SerializeItemsDictToList()
    {
        itemsList.Clear();
        foreach (var item in items)
        {
            itemsList.Add(new ItemSaveData()
            {
                MenuKey = item.Key,
                Amount = item.Value
            });
        }
    }
    
    public void SerializeItemsListToDict()
    {
        items.Clear();
        foreach (var item in itemsList)
        {
            items.Add(item.MenuKey, item.Amount);
              
        }
    }


}
