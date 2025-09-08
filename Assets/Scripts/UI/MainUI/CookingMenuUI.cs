using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class CookingMenuUI : MonoBehaviour
{
    [SerializeField] private MenuDatabase menuDatabase;
    [SerializeField] private TMP_InputField tmpInputField;
    [SerializeField] private TMP_Dropdown dropdown;
    [SerializeField] private MultiPageMenuDataDisplay multiMenuPageDataDisplay;
    [SerializeField] private CookingDetailUI cookingDetailUI;
    [SerializeField]private ToggleGroup toggleGroup;
    
    private Filtter< MenuData,string> namefilter =new Filtter< MenuData,string>((data) => data.itemData.Name,
        (name,filter) => name.Contains(filter),
            "");
    private Filtter< MenuData,int> rarityfilter =new Filtter< MenuData,int>((data) => data.Rarity,(star,filter) => star == filter,0);
   private List<MenuData> menus => menuDatabase.Menus;
   
    private void OnEnable()
    {
        tmpInputField.onSubmit.AddListener((value) =>
        {
            OnFitterUpdate();
        });
        dropdown.onValueChanged.AddListener((value) =>
        {
            OnFitterUpdate();
        });

        multiMenuPageDataDisplay.MultiItemsDisplay.onSpawnUI += (ui) =>
        {
            ui.onToggled += OnSelectMenu;
            ui.Toggle.group = toggleGroup;
            ui.Toggle.isOn = ui.Data() == cookingDetailUI.Data();
        };
        
        ShowMenus(menus);
    }

    private void OnFitterUpdate()
    {
        ShowMenus(menus);
    }
    
    private void ShowMenus(List<MenuData> menuDatas)
    {
        var nameFilteredData = namefilter.GetFilteredList(menus, tmpInputField.text);
        var rarityFilterFilteredData = rarityfilter.GetFilteredList(nameFilteredData, dropdown.value);
        multiMenuPageDataDisplay.setDisplayItems(  rarityFilterFilteredData.OrderBy(data=>data.Rarity).ToList());
    }

    private void OnSelectMenu(MenuData menu)
    {
        cookingDetailUI.setDisplay(menu);
    }

    private void OnDisable()
    {
        if(!this.gameObject.scene.isLoaded) return;
        foreach (var menu in multiMenuPageDataDisplay.MultiItemsDisplay.Elemnets)
        {
            menu.onToggled -= OnSelectMenu;
        }
    }
}
