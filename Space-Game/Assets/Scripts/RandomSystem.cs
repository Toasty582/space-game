using UnityEngine;
using TMPro;
using static Manager;
using static SaveTranslator;
using SaveData;


public class RandomSystem : MonoBehaviour
{
    public TMP_InputField systemIdInput;
    private void Start() {
        manager.env = new EnvironmentData();
        for (int i = 0; i < 500; i++) {
            NewSystem(i);
        }
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
            if (manager.env.environment[i] != null) {
                // Check what type of object it is
                switch ((string)manager.env.environment[i][(int)EnvironmentType]) {
                    case "SYSTEM":
                        // If it is a system, instantiate that system item
                        manager.activeObjects[0] = Instantiate(manager.systemPrefabs[0], Vector3.zero, Quaternion.Euler(0, 0, 0));
                        // Assign the system ID
                        manager.activeObjects[partialID].GetComponent<SystemObject>().ID = i;
                        // Assign the planet count
                        manager.activeObjects[partialID].GetComponent<SystemObject>().PlanetCount = (int)manager.env.environment[i][1];
                        // Log the system's creation
                        Debug.Log("System Insantiated");
                        break;

                    case "STAR":
                        // If it is a star, instantiate that star
                        manager.activeObjects[partialID] = Instantiate(manager.starPrefabs[(int)manager.env.environment[i][(int)EnvironmentPrefab]], Vector3.zero, Quaternion.Euler(0, 0, 0), manager.activeObjects[0].transform);
                        manager.activeStar = manager.activeObjects[partialID];
                        // Log the star's creation
                        Debug.Log("Star Instantiated");
                        break;

                    case "PLANET":
                        // If it is a planet, instantiate that planet
                        manager.activeObjects[partialID] = Instantiate(manager.planetPrefabs[(int)manager.env.environment[i][(int)EnvironmentPrefab]], Vector3.zero, Quaternion.Euler(0, 0, 0), manager.activeObjects[0].transform);
                        // Assign the planet ID
                        manager.activeObjects[partialID].GetComponent<Planet>().Id = i;
                        // Assign the planet distance
                        manager.activeObjects[partialID].GetComponent<Planet>().Distance = (float)manager.env.environment[i][(int)EnvironmentPlanetDistance];
                        // Move the planet to its place
                        manager.activeObjects[partialID].transform.Rotate(new Vector3(0, Random.Range(0f, 360f), 0));
                        manager.activeObjects[partialID].transform.Translate(transform.forward * (float)manager.env.environment[i][(int)EnvironmentPlanetDistance]);
                        // Log the planet's creation
                        Debug.Log("Planet " + (partialID) + " Instantiated");
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
        manager.env.environment[fullID] = new object[] { "SYSTEM", planetCount };

        // Increment the full ID
        fullID++;
        // Choose a random star prefab
        int randomStarIndex = Random.Range(0, manager.starPrefabs.Length);
        // Set the full ID to a star object
        manager.env.environment[fullID] = new object[] { "STAR", randomStarIndex };

        // Iterate through all the planets
        for (int i = 0; i < planetCount; i++) {
            // Increment the full ID
            fullID++;
            // Choose a random planet prefab
            int randomPlanetIndex = Random.Range(0, manager.planetPrefabs.Length);
            // Set the full ID to a planet object
            manager.env.environment[fullID] = new object[] { "PLANET", randomPlanetIndex, 0f};
            // Loop until the orbit is confirmed as valid
            bool validDistance = false;
            while (!validDistance) {
                // Require proof that the orbit is invalid
                validDistance = true;
                // Generate a new potential orbit distance and assign it to the planet
                float distanceCandidate = Random.Range(5f, 50f);
                manager.env.environment[fullID][(int)EnvironmentPlanetDistance] = distanceCandidate;
                // Iterate through every other created object in the system
                for (int n = systemID * 50; n < fullID; n++) {
                    // Check if the object is a planet
                    if ((string)manager.env.environment[n][0] == "PLANET") {
                        // If so, check if its orbit is within 1 unit of the potential orbit
                        if(distanceCandidate < ((float)manager.env.environment[n][(int)EnvironmentPlanetDistance] + 1f) && distanceCandidate > ((float)manager.env.environment[n][(int)EnvironmentPlanetDistance] - 1f)) {
                            // If so, the potential orbit is declared invalid
                            validDistance = false;
                        }
                    }
                }
            }
        }
    }
}
