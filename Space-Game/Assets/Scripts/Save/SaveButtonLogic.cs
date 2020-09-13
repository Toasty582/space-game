using UnityEngine;
using TMPro;
using static SaveSystem;

public class SaveButtonLogic : MonoBehaviour
{
    public void SetActiveSave() {
        string saveName = gameObject.GetComponent<TextMeshProUGUI>().text;
        saveSystem.SetActiveSave(saveName);
    }
}
