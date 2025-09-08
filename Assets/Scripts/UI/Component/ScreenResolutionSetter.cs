using System;
using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScreenResolutionSetter : MonoBehaviour
{
    public struct ScreenResolutionOption
    {
        public int Width;
        public int Height;
    }
    
    [SerializeField]Toggle fullScreenToggle;
    [SerializeField]TMP_Dropdown dropdown;


    private Dictionary<string,Vector2Int> resolutionOptions = new ()
    {
        {"1920x1080",new (1920,1080)},
        {"1600x900",new (1600,900)},
        {"1280x720",new (1280,720)},
    };

    private void Awake()
    {

        dropdown.AddOptions(resolutionOptions.Keys.ToList());
        fullScreenToggle.SetIsOnWithoutNotify(  ScreenResoultionManager.Instance.FullScreen);
        var index = dropdown.options.FindIndex((op) =>
            op.text.Equals($"{ScreenResoultionManager.Instance.Width}x{ScreenResoultionManager.Instance.Height}"));
        if(index < 0) index = 0;
        dropdown.SetValueWithoutNotify(index);
        dropdown.onValueChanged.AddListener((i) =>
        {
            var screenRes =resolutionOptions.GetValueOrDefault( dropdown.options[i].text);
            ScreenResoultionManager.Instance.SetResolution(screenRes.x, screenRes.y);
        });
        fullScreenToggle.onValueChanged.AddListener((ison) =>
        {
            ScreenResoultionManager.Instance.SetResolution(ison);
        });

    }



    private void OnDisable()
    {

    }
}
