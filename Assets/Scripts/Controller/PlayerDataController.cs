using System;
using System.Collections;
using System.IO;
using UnityEngine;

public class PlayerDataController : IGameController
{
    private PlayerData playerData;
    public Action<int> OnEnergyRegen;
    public Action<int> OnEnergyUsed;

    private Coroutine energeyCoroutine;

    private const bool showLog = true;

    public PlayerDataController()
    {
        ReadSave();
    }

    #region Energey

    public void UseEnergy(int amount)
    {
        if (playerData.Energy < amount) return;
        playerData.Energy -= amount;
        if (energeyCoroutine == null && playerData.Energy < GameConst.Energy.MaxEnergy)
        {
            playerData.LastesEnergeyRegenTime = DateTime.Now.ToBinary();
            energeyCoroutine = GameManager.Instance.StartCoroutine(regenEnergy());
        }
        OnEnergyUsed?.Invoke(playerData.Energy);
    }
    

private IEnumerator regenEnergy()
    {
        var lastTime = DateTime.FromBinary(playerData.LastesEnergeyRegenTime);
        while (playerData.Energy < GameConst.Energy.MaxEnergy)
        {
            var diffsecs = (DateTime.Now - lastTime).TotalSeconds;
            if (diffsecs > GameConst.Energy.EnergyRegenRate)
            {
                var totalEnergy = Mathf.FloorToInt((float)(diffsecs/GameConst.Energy.EnergyRegenRate));
                playerData.Energy +=totalEnergy;
                playerData.Energy = Mathf.Clamp(playerData.Energy, 0, GameConst.Energy.MaxEnergy);
                lastTime =  lastTime.AddSeconds(totalEnergy*GameConst.Energy.EnergyRegenRate);
                playerData.LastesEnergeyRegenTime = lastTime.ToBinary();
                OnEnergyRegen?.Invoke(playerData.Energy);
            }
            yield return null;
        }

        energeyCoroutine = null;
    }

    private void onStartCooking(CookingController.CookingData data)
    {
        playerData.CookingSave = new PlayerData.CookingSaveData()
        {
            MenuKey = data.MenuData.itemData.Key,
            StartTime = data.StartTime.ToBinary()
        };
        WriteSave();
    }
    
    private void onEndCooking(MenuData data)
    {
        if (playerData.CookingSave.MenuKey != data.itemData.Key)
        {
            Debug.LogError($"Cooking Data invalid: LastedCooking = {playerData.CookingSave.MenuKey } / currently end{ data.itemData.Key}");
            return;
        }
        playerData.CookingSave = new PlayerData.CookingSaveData()
        {
            MenuKey = "",
            StartTime = new DateTime().ToBinary()
        };
        WriteSave();

    }

    #endregion

    #region Save/load

    public void WriteSave()
    {
        WriteSave(playerData);
    }

    private void WriteSave(PlayerData _playerData)
    {
        _playerData.SerializeItemsDictToList();
        File.WriteAllText(GameConst.SavePath.PlayerDataTargetPath, JsonUtility.ToJson( _playerData));
        if (showLog) 
            Debug.Log($"<color=green>[IOManager] [Logging] :</color> Write Save PlayerData to JSON");
    }

    public PlayerData ReadSave()
    {
        if (playerData != null)
            return playerData; //Return If Already Read Save.

       

        if (!File.Exists(GameConst.SavePath.PlayerDataTargetPath))
        {
            if (showLog)
                Debug.Log("<color=green>[IOManager] [Logging] :</color> Don't have file.");

            playerData = new PlayerData();
            return playerData;
        }

        var _jsonText = File.ReadAllText(GameConst.SavePath.PlayerDataTargetPath);
        playerData =JsonUtility.FromJson<PlayerData>(_jsonText);;
        
        if (showLog)
            Debug.Log("<color=green>[IOManager] [Logging] :</color> Read Save " + GameConst.SavePath.PlayerDataTargetPath);

        playerData.SerializeItemsListToDict();
        return playerData;
    }

    public void ClearSave()
    {

        playerData = new PlayerData();

        WriteSave();
        if (showLog)
            Debug.Log("ClearSave");
    }

    #endregion
    
    #region GameController

    public void subScribeEventHandlers()
    {
        GameManager.Instance.CookingController.OnStartCooking += onStartCooking;
        GameManager.Instance.CookingController.OnCompleteCooking += onEndCooking;
    }

    public void startGame()
    {
        if (playerData.Energy < GameConst.Energy.MaxEnergy)
        {
            energeyCoroutine = GameManager.Instance.StartCoroutine(regenEnergy());
        }
    }

    public void onClear()
    {
        GameManager.Instance.CookingController.OnStartCooking -= onStartCooking;
        GameManager.Instance.CookingController.OnCompleteCooking -= onEndCooking;
    }
    #endregion
}