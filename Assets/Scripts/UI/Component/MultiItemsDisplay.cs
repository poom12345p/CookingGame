using System;
using System.Collections.Generic;
using ObjectPooling;
using UnityEditor;
using UnityEngine;

public class MultiItemsDisplay<T,U> : MonoBehaviour where U : MonoBehaviour,IPoolable,IDataDisplay<T>
{
    public List<U> Elemnets => uiElemnets;

  [SerializeField] private Transform content;
  [SerializeField] private U prefab;
  protected List<U> uiElemnets = new();

  public Action<U> onSpawnUI;
  public Action<U> onDeSpawnUI;
  

  public void setDisplayItems(IEnumerable<T> items)
  {
      foreach (var element in uiElemnets)
      {
          onDeSpawnUI?.Invoke(element);
          ObjectPoolManager.Instance.Release(element);
      }
      uiElemnets.Clear();

      foreach (var item in  items)
      {
          var obj =ObjectPoolManager.Instance.Get(prefab);
          obj.transform.SetParent(content,false);
          obj.setDisplay(item);
          uiElemnets.Add(obj);
          onSpawnUI?.Invoke(obj);
      }
  }
  
  
  
}
