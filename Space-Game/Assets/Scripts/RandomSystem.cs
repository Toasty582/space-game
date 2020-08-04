using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Planet;
using static Manager;


// ----------------------------------------- ONE UNITY UNIT = 50 * 10^6 km = 1/3 AU --------------------------------------------------------------------------------


public class RandomSystem : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)) {
            // Generate a star system with 1-8 planets
            GenerateNewSystem(Random.Range(1, 8));
        }
    }

    void GenerateNewSystem(int planetQuantity) {
        // Destroy the existing star system
        Destroy(manager.star);
        foreach(GameObject planet in manager.planets) {
            Destroy(planet);
        }

        // Recreate the planets array
        manager.planets = new GameObject[planetQuantity];

        // Randomly pick a star from the prefabs
        int randomStarIndex = Random.Range(0, manager.starPrefabs.Length);
        manager.star = Instantiate(manager.starPrefabs[randomStarIndex], Vector3.zero, Quaternion.Euler(0, 0, 0));

        // Log the star's creation
        Debug.Log("Star Instantiated");

        // Generate the planets
        for(int id = 0; id < planetQuantity; id++) {
            CreatePlanet(id);
        }
    }

    void CreatePlanet(int planetID) {
        // Pick the type of planet and create it
        int randomPlanetType = Random.Range(0, manager.planetPrefabs.Length);
        manager.planets[planetID] = Instantiate(manager.planetPrefabs[randomPlanetType], Vector3.zero, Quaternion.Euler(0, 0, 0));

        // Assign the planet ID
        manager.planets[planetID].GetComponent<Planet>().Id = planetID;

        // Loop until the orbit is confirmed as valid
        bool validOrbit = false;
        while(!validOrbit) {
            // Generate a new potential orbit distance and assign it to the planet
            float planetDistance = Random.Range(5f, 50f);
            manager.planets[planetID].GetComponent<Planet>().Distance = planetDistance;

            // Check the orbit for conflicts with other orbits
            validOrbit = OrbitConflictCheck(planetID);
        }
        

        // Move the planet to a randomly generated orbit and a randomly generated point on that orbit
        float planetInitialPosition = Random.Range(0f, 359f);
        manager.planets[planetID].transform.Rotate(new Vector3(0, planetInitialPosition, 0));
        float distance = manager.planets[planetID].GetComponent<Planet>().Distance;
        manager.planets[planetID].transform.Translate(distance, 0, distance);

        // Log the planet's creation
        Debug.Log("Planet " + planetID + " Instantiated");
    }

    bool OrbitConflictCheck(int planetID) {
        // Iterate through all the planets in the planet array
        foreach (GameObject checkPlanet in manager.planets) {

            // Check to make sure there are other planets in the array
            if (checkPlanet) {

                // Check if the planet being used for comparison is the same planet as the one being checked
                if (checkPlanet.GetComponent<Planet>().Id != planetID) {

                    // If not, check if the planets' orbits are within 0.5 units of one another
                    if (checkPlanet.GetComponent<Planet>().Distance < (manager.planets[planetID].GetComponent<Planet>().Distance + 0.5f) && checkPlanet.GetComponent<Planet>().Distance > (manager.planets[planetID].GetComponent<Planet>().Distance - 0.5f)) {

                        // If so, return false
                        return false;
                    }
                }
            }
        }
        // If the planet passed through all the checks without being flagged as too close, return true
        return true;
    }
}
