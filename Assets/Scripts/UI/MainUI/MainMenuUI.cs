using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : BaseUI
{
   [SerializeField] private Button startButton;
   [SerializeField] private Button clearSaveButton;
   [SerializeField] private Button settingButton;
   [SerializeField] private Button exitButton;

   [SerializeField] private CookingCanvas cookingCanvasPrefab;
   [SerializeField] private SettingUI settingUIPrefab;
   private CookingCanvas cookingCanvas;
   private void Awake()
   {
      startButton.onClick.AddListener( onClickStart);
      clearSaveButton.onClick.AddListener( onClickClearSave);
      settingButton.onClick.AddListener( onClickSetting);
      exitButton.onClick.AddListener( onClickExit);
   }

   private void onClickStart()
   {
      var cookingCanvas = Instantiate(cookingCanvasPrefab);
      cookingCanvas.HideImmediately();
      cookingCanvas.Show();
   }

   private void onClickClearSave()
   {
      GameManager.Instance.PlayerDataController.ClearSave();
   }
   
   private void onClickSetting()
   {
      var ui = Instantiate(settingUIPrefab);
      ui.HideImmediately();
      ui.Show();
   }
   private void onClickExit()
   {
      Application.Quit();
   }
}
