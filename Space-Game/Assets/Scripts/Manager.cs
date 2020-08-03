using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    // Singleton Instantiation
    public static Manager manager = null;

    void Awake() {
        if (manager == null) {
            manager = this;
        } else if (manager != this) {
            Destroy(gameObject);
        }
    }

    // Variables that may need to be accessed from multiple places
    public GameObject[] starPrefabs;
    public GameObject[] planetPrefabs;

    public Transform starPosition;

    public GameObject star;
    public GameObject[] planets;

}
