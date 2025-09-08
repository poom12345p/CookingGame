using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ObjectPooling;
using UnityEngine;
using UnityEngine.UI;

public class MultiPagesDataDisplay<T,U>: MonoBehaviour where U : MonoBehaviour,IPoolable,IDataDisplay<T>
{
    public MultiItemsDisplay<T,U>  MultiItemsDisplay=> multiItemsDisplay;
    
    [Header("UI Elements")]
    [SerializeField]MultiItemsDisplay<T,U>  multiItemsDisplay;
    [SerializeField] private Transform pageMarkContent;
    [SerializeField] private pageMark pageMarkPrefab;
    [SerializeField] private ToggleGroup toggleGroup;
    [SerializeField] private Button previousButton;
    [SerializeField] private Button nextButton;
    [Header("Setting")] 
    [SerializeField] private int pageSize = 4;

    private List<T> datas = new();
    private List<pageMark> pageMarks = new List<pageMark>();
    private int pageCount;
    private int currentPageIndex;

    private void Awake()
    {
        nextButton.onClick.AddListener(nextPage);
        previousButton.onClick.AddListener(previousPage);
    }

    public void setDisplayItems(List<T> items)
    {
        datas = items;
        pageCount = Mathf.CeilToInt((float)items.Count / (float)pageSize);
        setPageMark(pageCount);
        showPage(0);
    }
    
    
    public void showPage(int i)
    {
        if (i < 0 || i >= pageCount)
        {
            multiItemsDisplay.setDisplayItems(Enumerable.Empty<T>());
            return;
        }
        currentPageIndex = i;
        var pageData = getDataInPage(i);
        multiItemsDisplay.setDisplayItems(pageData);
        pageMarks[i].Toggle.isOn = true;
        
        previousButton.interactable = true;
        nextButton.interactable = true;
        
        if(i==0)previousButton.interactable = false;
        if(i==pageCount-1)nextButton.interactable = false;
    }

    private void nextPage()
    {
        showPage(currentPageIndex+1);
    }
    
    private void previousPage()
    {
        showPage(currentPageIndex-1);
    }

    private IEnumerable<T> getDataInPage(int i)
    {
        if (datas == null) yield break;
        
        if(i*pageSize >= datas.Count) yield break;

        int maxindex = Mathf.Min((i+1)*pageSize, datas.Count);

        for(int j = i*pageSize; j < maxindex; j++) yield return datas[j];
    }

    private void setPageMark(int pageCount)
    {
        foreach (var mark in  pageMarks)
        {
            ObjectPoolManager.Instance.Release(mark);
        }
        pageMarks.Clear();
        for (int i = 0; i < pageCount; i++)
        {
            var obj =ObjectPoolManager.Instance.Get( pageMarkPrefab);
            obj.transform.SetParent(pageMarkContent,false);
            obj.Toggle.group = toggleGroup;
            pageMarks.Add(obj);
        }
    }

    private void OnDisable()
    {
        if(!gameObject.scene.isLoaded) return;
        foreach (var mark in  pageMarks)
        {
            ObjectPoolManager.Instance.Release(mark);
        }
        pageMarks.Clear();
    }
}
