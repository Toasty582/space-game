using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static Planet;
using static Manager;
using static EnvironmentData;


// ----------------------------------------- ONE UNITY UNIT = 50 * 10^6 km = 1/3 AU --------------------------------------------------------------------------------


public class RandomSystem : MonoBehaviour
{
    public TMP_InputField systemIdInput;
    private void Start() {
        manager.environment = new EnvironmentData();
        for (int i = 0; i < 500; i++) {
            NewSystem(i);
        }
        LoadSystem(0);
    }

    public void ManualLoadSystem() {
        LoadSystem(System.Convert.ToInt32(systemIdInput.text));
        Debug.Log("Loaded System " + systemIdInput.text);
    }

    public void LoadSystem(int systemID) {
        int fullSystemID = systemID * 50;

        // Destroy the existing star system
        foreach(GameObject n in manager.activeObjects) {
            Destroy(n);
        }

        // Recreate the activeObjects array
        manager.activeObjects = new GameObject[50];

        // Iterate through every object in the system
        for (int i = fullSystemID; i < fullSystemID + 50; i++) {
            int partialID = i - fullSystemID;
            // Check if the object exists
            if (manager.environment.objects[i] != null) {
                // Check what type of object it is
                switch ((string)manager.environment.objects[i][0]) {
                    case "SYSTEM":
                        // If it is a system, instantiate that system item
                        manager.activeObjects[0] = Instantiate(manager.systemPrefabs[0], Vector3.zero, Quaternion.Euler(0, 0, 0));
                        // Assign the system ID
                        manager.activeObjects[partialID].GetComponent<SystemObject>().ID = i;
                        // Assign the planet count
                        manager.activeObjects[partialID].GetComponent<SystemObject>().PlanetCount = (int)manager.environment.objects[i][1];
                        // Log the system's creation
                        Debug.Log("System Insantiated");
                        break;

                    case "STAR":
                        // If it is a star, instantiate that star
                        manager.activeObjects[partialID] = Instantiate(manager.starPrefabs[(int)manager.environment.objects[i][1]], Vector3.zero, Quaternion.Euler(0, 0, 0), manager.activeObjects[0].transform);
                        manager.activeStar = manager.activeObjects[partialID];
                        // Log the star's creation
                        Debug.Log("Star Instantiated");
                        break;

                    case "PLANET":
                        // If it is a planet, instantiate that planet
                        manager.activeObjects[partialID] = Instantiate(manager.planetPrefabs[(int)manager.environment.objects[i][1]], Vector3.zero, Quaternion.Euler(0, 0, 0), manager.activeObjects[0].transform);
                        // Assign the planet ID
                        manager.activeObjects[partialID].GetComponent<Planet>().Id = i;
                        // Assign the planet distance
                        manager.activeObjects[partialID].GetComponent<Planet>().Distance = (float)manager.environment.objects[i][2];
                        // Move the planet to its place
                        manager.activeObjects[partialID].transform.Rotate(new Vector3(0, (float)manager.environment.objects[i][3], 0));
                        manager.activeObjects[partialID].transform.Translate(transform.forward * (float)manager.environment.objects[i][2]);
                        // Log the planet's creation
                        Debug.Log("Planet " + (partialID) + " Insantiated");
                        break;

                    default:
                        break;
                }
            }
        }
    }

    void NewSystem(int systemID) {
        // Calculate the full ID from the system ID
        int fullID = systemID * 50;
        // Calculate a random planet amount
        int planetCount = Random.Range(1, 8);
        // Set the full ID to a system object
        manager.environment.objects[fullID] = new object[] { "SYSTEM", planetCount };

        // Increment the full ID
        fullID++;
        // Choose a random star prefab
        int randomStarIndex = Random.Range(0, manager.starPrefabs.Length);
        // Set the full ID to a star object
        manager.environment.objects[fullID] = new object[] { "STAR", randomStarIndex };

        // Iterate through all the planets
        for (int i = 0; i < planetCount; i++) {
            // Increment the full ID
            fullID++;
            // Choose a random planet prefab
            int randomPlanetIndex = Random.Range(0, manager.planetPrefabs.Length);
            // Set the full ID to a planet object
            manager.environment.objects[fullID] = new object[] { "PLANET", randomPlanetIndex, 0, 0f };
            // Loop until the orbit is confirmed as valid
            bool validDistance = false;
            while (!validDistance) {
                // Require proof that the orbit is invalid
                validDistance = true;
                // Generate a new potential orbit distance and assign it to the planet
                float distanceCandidate = Random.Range(5f, 50f);
                manager.environment.objects[fullID][2] = distanceCandidate;
                // Iterate through every other created object in the system
                for (i = systemID * 50; i < fullID; i++) {
                    // Check if the object is a planet
                    if ((string)manager.environment.objects[i][0] == "PLANET") {
                        // If so, check if its orbit is within 1 unit of the potential orbit
                        if(distanceCandidate < ((float)manager.environment.objects[i][2] + 1f) && distanceCandidate > ((float)manager.environment.objects[i][2] - 1f)) {
                            // If so, the potential orbit is declared invalid
                            validDistance = false;
                        }
                    }
                }
            }
            // Set a random orbit angle
            manager.environment.objects[fullID][3] = Random.Range(0f, 359f);
        }
    }
}
