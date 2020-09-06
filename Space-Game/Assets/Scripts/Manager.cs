using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ----------------------------------------- ONE UNITY UNIT = 50 * 10^6 km = 1/3 AU --------------------------------------------------------------------------------


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

    public EnvironmentData environment;

    public int timestep = 2628000; // Number of in-game seconds that pass every real life second

}
