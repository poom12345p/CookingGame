using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sigleton;

public class GameManager : PersistentMonoSingleton<GameManager>
{
    #region public
    public PlayerData PlayerData =>  PlayerDataController.ReadSave();
    public Dictionary<string, MenuData> MenuDatabase => menuDict;
    #endregion
    
    #region Controllor
    public CookingController CookingController;
    public PlayerDataController PlayerDataController;
    #endregion
    
    
    [SerializeField]private MenuDatabase menuDatabase;
    [SerializeField]private AudioClip bgm;
    #region private
    private Dictionary<string,MenuData> menuDict=new Dictionary<string, MenuData>();
    private List<IGameController> gameControllers = new ();
    #endregion

    protected override void OnInitialize()
    {
        base.OnInitialize();

        foreach (var menu in menuDatabase.Menus)
        {
            menuDict.Add(menu.itemData.Key,menu);
        }
        
        CookingController = new CookingController();
        PlayerDataController = new PlayerDataController();
        gameControllers.Add(CookingController);
        gameControllers.Add(PlayerDataController);

        foreach (var controller in gameControllers)
        {
            controller.subScribeEventHandlers();
        }
        
        foreach (var controller in gameControllers)
        {
            controller.startGame();
        }
        SoundManager.Instance.PlayBGM(bgm);
    }

    private void OnDisable()
    {
        foreach (var controller in gameControllers)
        {
            controller.onClear();
        }
    }
}
