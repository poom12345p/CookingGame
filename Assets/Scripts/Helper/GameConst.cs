using UnityEngine;

public  static class GameConst
{
    public class Energy
    {
        public const int MaxEnergy = 100;
        public const float EnergyRegenRate = 5f;
        public const int EnergyPerCooking = 10;   
    }

    public class SavePath
    {
        private const string SaveFileName = "/save.json";
        public static string PlayerDataTargetPath => Application.persistentDataPath + SaveFileName;
    }
}
