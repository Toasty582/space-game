using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static RandomSystem;
using static Manager;


// ----------------------------------------- ONE UNITY UNIT = 50 * 10^6 km = 1/3 AU --------------------------------------------------------------------------------


public class Planet : MonoBehaviour{
    // Fields
    private float orbitCircumference = 0f; // Orbit Circumference in Unity units
    private float distance = 0f; // Orbit radius in Unity units
    private float orbitalPeriod = 0f; // Orbital period in years
    private float orbitalPeriodSeconds = 0f; // Orbital period in seconds
    private float orbitalSpeed = Mathf.Infinity; // Orbital Speed in Unity units per year


    // Properties
    public float Circumference { get; }
    public float Distance {
        get {
            return distance;
        }
        set {
            distance = value;
            orbitCircumference = Mathf.PI * Mathf.Pow(distance, 2);

            orbitalPeriod = Mathf.Sqrt(Mathf.Pow((distance * 3), 3));
            orbitalPeriodSeconds = orbitalPeriod * 31536000;

            if (orbitalPeriod != 0f) {
                orbitalSpeed = orbitCircumference / orbitalPeriod;
            } else {
                orbitalSpeed = Mathf.Infinity;
            }
        }
    }

    public float OrbitalPeriod { get; }
    public float OrbitalSpeed { get; }
   
    public int Id { get; set; }

    // Methods
    void Update() {
        Orbit();
    }

    void Orbit() {
        transform.RotateAround(manager.star.transform.position, Vector3.up, (360 / orbitalPeriodSeconds) * manager.timestep);
    }
}
