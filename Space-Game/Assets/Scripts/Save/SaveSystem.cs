using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using SaveData;
using static Manager;

public static class SaveSystem {
    
    public static void SaveEnvironment() {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/saves/environment.sav";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, manager.env);
        stream.Close();
    }

    public static EnvironmentData LoadEnvironment() {
        string path = Application.persistentDataPath + "/saves/environment.sav";

        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            EnvironmentData data = formatter.Deserialize(stream) as EnvironmentData;
            stream.Close();

            return data;

        } else {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
