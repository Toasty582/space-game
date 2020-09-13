using UnityEngine;
using SaveData;
using static Manager;

public class SaveManager : MonoBehaviour
{
    public void Save() {
        SaveSystem.SaveEnvironment();
    }

    public void Load() {
        EnvironmentData envData = SaveSystem.LoadEnvironment();
        manager.env.environment = envData.environment;
    }
}
