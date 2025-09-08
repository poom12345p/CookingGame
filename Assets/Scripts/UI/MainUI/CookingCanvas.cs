using System;
using UnityEngine;
using UnityEngine.UI;

public class CookingCanvas : BaseUI
{
    [SerializeField] private Button closeButton;

    private void Awake()
    {
        closeButton.onClick.AddListener(Close);
    }
}
