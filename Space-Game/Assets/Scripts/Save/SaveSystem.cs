using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using SaveData;
using TMPro;
using static Manager;

public class SaveSystem : MonoBehaviour {

    #region Singleton
    // Singleton Instantiation
    public static SaveSystem saveSystem = null;

    private void Awake() {
        if (saveSystem == null) {
            saveSystem = this;
        } else if (saveSystem != this) {
            Destroy(gameObject);
        }

        saveFolderPath = Application.persistentDataPath + "/Saves/";
    }
    #endregion

    public GameObject saveCanvas;
    public GameObject loadCanvas;
    public GameObject saveButtonPrefab;
    public TMP_InputField saveNameInput;

    [HideInInspector]
    public string saveFolderPath;


    public void OpenSaveMenu() {
        manager.saveMenu.GetComponent<Canvas>().enabled = true;
        int buttonCount = 0;
        foreach (string directory in Directory.GetDirectories(saveFolderPath)) {
            GameObject button = Instantiate(saveButtonPrefab, saveCanvas.transform);

            string[] saveName = directory.Split('/');
            button.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = saveName[saveName.Length - 1];

            button.GetComponent<RectTransform>().Translate(new Vector3(0f, -40f * buttonCount,0f));
            buttonCount++;
        }
    }

    public void CloseSaveMenu() {
        manager.saveMenu.GetComponent<Canvas>().enabled = false;
    }

    public void OpenLoadMenu() {
        manager.loadMenu.GetComponent<Canvas>().enabled = true;
        int buttonCount = 0;
        foreach (string directory in Directory.GetDirectories(saveFolderPath)) {
            GameObject button = Instantiate(saveButtonPrefab, loadCanvas.transform);

            string[] saveName = directory.Split('/');
            button.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = saveName[saveName.Length - 1];

            button.GetComponent<RectTransform>().Translate(new Vector3(0f, -40f * buttonCount, 0f));
            buttonCount++;
        }
    }

    public void CloseLoadMenu() {
        manager.loadMenu.GetComponent<Canvas>().enabled = false;
    }

    public void Save() {
        saveFolderPath = Path.Combine(saveFolderPath, saveNameInput.text);
        if (!Directory.Exists(saveFolderPath)) {
            Directory.CreateDirectory(saveFolderPath);
        }
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(saveFolderPath + "/environment.sav", FileMode.Create);

        formatter.Serialize(stream, manager.env);
        stream.Close();
        CloseSaveMenu();
    }

    public void Load() {
        saveFolderPath = Path.Combine(saveFolderPath, saveNameInput.text);
        if (File.Exists(saveFolderPath + "/environment.sav")) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(saveFolderPath + "/environment.sav", FileMode.Open);

            EnvironmentData data = formatter.Deserialize(stream) as EnvironmentData;
            stream.Close();

            manager.env.environment = data.environment;

        } else {
            Debug.LogError("Save file not found in " + saveFolderPath + "/environment.sav");
        }
        CloseLoadMenu();
    }

    public void SetActiveSave(string saveName) {
        saveNameInput.text = saveName;
    }
}
