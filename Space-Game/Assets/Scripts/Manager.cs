using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager instance = null;

    void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }
    }


    public GameObject[] starPrefabs;
    public GameObject[] planetPrefabs;

    public Transform starPosition;

    public GameObject star;
    public GameObject[] planets;
    public int[] planetDistances;

}
