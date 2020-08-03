using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static RandomSystem;
using static Manager;

public class Planet : MonoBehaviour{
    // Fields
    private float orbitCircumference;
    private float distance;
    private float orbitalPeriod;
    private float orbitalSpeed;
    
    private int id;


    // Properties
    public float Circumference { get; }
    public float Distance {
        get {
            return distance;
        }
        set {
            distance = value;
            orbitalPeriod = Mathf.Sqrt(Mathf.Pow(distance, 3));
            orbitCircumference = Mathf.PI * Mathf.Pow(distance, 2);
            orbitalSpeed = orbitCircumference / orbitalPeriod;
        }
    }

    public float OrbitalPeriod { get; }
    public float OrbitalSpeed { get; }
   
    public int Id { get; private set; }

    public GameObject PlanetInstance { get; private set; }

    // Methods
    public void ConfigurePlanet(float planetDistance, int planetID) {
        float planetInitialPosition = Random.Range(0f, 359f);

        PlanetInstance = instance.planets[planetID];
        PlanetInstance.transform.Rotate(new Vector3(0, planetInitialPosition, 0));
        PlanetInstance.transform.Translate(planetDistance, 0, planetDistance);

        Distance = planetDistance;
        Id = planetID;
    }

    void Awake() {
        
    }
}
