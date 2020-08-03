using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Planet;
using static Manager;

public class RandomSystem : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)) {
            GenerateNewSystem(Random.Range(1, 8));
        }
    }

    void GenerateNewSystem(int planetQuantity) {
        // Destroy the existing system
        Destroy(instance.star);
        foreach(GameObject planet in instance.planets) {
            Destroy(planet);
        }

        // Redefine the planets array
        instance.planets = new GameObject[planetQuantity];
        instance.planetDistances = new int[planetQuantity];

        // Create the star
        int randomStarIndex = Random.Range(0, instance.starPrefabs.Length);
        instance.star = Instantiate(instance.starPrefabs[randomStarIndex], instance.starPosition);
        Debug.Log("Star Instantiated");

        for(int i = 0; i < planetQuantity; i++) {
            // Create the planet
            int randomPlanetIndex = Random.Range(0, instance.planetPrefabs.Length);
            instance.planets[i] = Instantiate(instance.planetPrefabs[randomPlanetIndex], instance.starPosition);
            instance.planets[i].AddComponent<Planet>();

            bool distanceConfirmed = false;
            int distanceCandidate;
            while(distanceConfirmed == false) {
                distanceCandidate = Random.Range(5, 50);
                distanceConfirmed = CheckDistance(distanceCandidate);
            }

            // Configure the planet
            instance.planets[i].GetComponent<Planet>().ConfigurePlanet(Random.Range(5, 50), i);
            Debug.Log("Planet " + i + " Instantiated");
            
        }
    }
    bool CheckDistance(int distanceCandidate) {
        foreach(int distance in instance.planetDistances) {
            if(distance == distanceCandidate) {
                return false;
            }
        }
        return true;
    }
}
