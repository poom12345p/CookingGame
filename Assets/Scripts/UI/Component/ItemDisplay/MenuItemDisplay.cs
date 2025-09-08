using System;
using AYellowpaper.SerializedCollections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuItemDisplay :ItemDisplay,IDataDisplay<MenuData>
{
    public Toggle Toggle => toggle;
    public Action<MenuData> onToggled;
    [SerializeField]TMP_Text startext;
    [SerializeField]TMP_Text Nametext;
    [SerializeField] private Toggle toggle;
    
    private MenuData menuData;
    public new MenuData Data()
    {
        return menuData;
    }

    private void Awake()
    {
        Toggle.onValueChanged.AddListener((isOn) =>
        {
            if (isOn)
            {
                onToggled?.Invoke(menuData);
                Toggle.interactable = false;
            }
            else
            {
                Toggle.interactable = true;
            }
        });
    }

    public void setDisplay(MenuData data)
    {
        menuData = data;
        Nametext.SetText(data.itemData.Name);
        startext.SetText(genStartTest(data.Rarity));
       setDisplay(data.itemData);
    }
    
    private string genStartTest(int count)
    {
        string text = "";
        for (int i = 0; i < count; i++)
        {
            text += "<sprite=0>";
        }

        return text;
    }

    public override void onGet()
    {
        base.onGet();
    }

    public override void onRelease()
    {
        base.onRelease();
        onToggled = null;
    }
}
