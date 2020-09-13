using UnityEngine;
using SaveData;

public class Manager : MonoBehaviour
{
    // Singleton Instantiation
    public static Manager manager = null;

    private void Awake() {
        if (manager == null) {
            manager = this;
        } else if (manager != this) {
            Destroy(gameObject);
        }
    }

    // Variables that may need to be accessed from multiple places -- Can be referenced with manager.variableName
    public GameObject[] starPrefabs;
    public GameObject[] planetPrefabs;
    public GameObject[] systemPrefabs;

    public GameObject activeStar;
    public GameObject[] activeObjects;

    public EnvironmentData env;
}
