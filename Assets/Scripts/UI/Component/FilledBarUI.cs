using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FilledBarUI : MonoBehaviour
{
    [SerializeField] private Image FillImage;
    public float MaxValue;
    public float CurrentValue;

    public void SetMaxValue(float _value)
    {
        if(_value== 0) Debug.LogError($"MaxValue is set to 0");
        MaxValue = _value;
        CurrentValue = _value;
        UpdateBar();
    }

    public void UpdateValue(float _value)
    {
        CurrentValue = _value;
        UpdateBar();
    }

    private void UpdateBar()
    {
        if(MaxValue == 0 )
            Debug.LogError($"MaxValue is 0 /{gameObject.name}");
        FillImage.fillAmount = CurrentValue/MaxValue;
    }
}
