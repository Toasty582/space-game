using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static RandomSystem;
using static Manager;

public class Planet : MonoBehaviour{
    // Fields
    private float orbitCircumference = 0f;
    private float distance = 0f;
    private float orbitalPeriod = 0f;
    private float orbitalSpeed = Mathf.Infinity;


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
}
