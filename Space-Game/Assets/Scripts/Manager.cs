using UnityEngine;
using SaveData;

public class Manager : MonoBehaviour
{
    #region Singleton
    // Singleton Instantiation
    public static Manager manager = null;

    private void Awake() {
        if (manager == null) {
            manager = this;
        } else if (manager != this) {
            Destroy(gameObject);
        }
    }
    #endregion

    #region Variables for other scripts
    // Variables that may need to be accessed from multiple places -- Can be referenced with manager.variableName
    [HideInInspector]
    public GameObject[] starPrefabs;
    [HideInInspector]
    public GameObject[] planetPrefabs;
    [HideInInspector]
    public GameObject[] systemPrefabs;

    public GameObject activeStar;
    public GameObject[] activeObjects;

    [HideInInspector]
    public EnvironmentData env;
    #endregion

    public GameObject pauseMenu;
    public GameObject saveMenu;
    public GameObject loadMenu;

    bool gamePaused = false;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            TogglePause();
        }
    }

    public void TogglePause() {
        if (gamePaused == false) {
            pauseMenu.GetComponent<Canvas>().enabled = true;
            gamePaused = true;
        } else {
            pauseMenu.GetComponent<Canvas>().enabled = false;
            gamePaused = false;
        }
    }
}
