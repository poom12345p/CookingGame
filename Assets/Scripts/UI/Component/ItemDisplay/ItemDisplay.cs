using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ItemDisplay : MonoBehaviour,IPoolable,IDataDisplay<ItemData>
{
    [SerializeField] private Image display;
    public ItemData Data()
    {
        return itemData;
    }

    protected ItemData itemData;
    public virtual void setDisplay(ItemData itemData)
    {
        this.itemData=itemData;
        display.sprite = itemData.Sprite;
    }

    public GameObject OriginPref { get; set; }
    public virtual void onGet()
    {
        gameObject.SetActive(true);
    }

    public virtual  void onRelease()
    {
        gameObject.SetActive(false);
        transform.SetParent(null,false);
    }

    public void onDestroy()
    {

    }
}
