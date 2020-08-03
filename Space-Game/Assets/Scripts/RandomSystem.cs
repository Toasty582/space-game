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
        Destroy(instance.star);
        foreach(GameObject planet in instance.planets) {
            Destroy(planet);
        }

        instance.planets = new GameObject[planetQuantity];

        int randomStarIndex = Random.Range(0, instance.starPrefabs.Length);
        instance.star = Instantiate(instance.starPrefabs[randomStarIndex], instance.starPosition);
        Debug.Log("Star Instantiated");

        for(int i = 0; i < planetQuantity; i++) {
            int randomPlanetIndex = Random.Range(0, instance.planetPrefabs.Length);
            instance.planets[i] = Instantiate(instance.planetPrefabs[randomPlanetIndex], instance.starPosition);
            instance.planets[i].GetComponent<Planet>().ConfigurePlanet(Random.Range(5f, 50f), i);
            Debug.Log("Planet " + i + " Instantiated");
            
        }
    }
}
