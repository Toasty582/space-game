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

    LineRenderer lineRenderer;


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
    void Awake() {
        lineRenderer = gameObject.GetComponent<LineRenderer>();
    }

    void Update() {
        Orbit();
    }

    void Orbit() {
        transform.RotateAround(manager.star.transform.position, Vector3.up, (360 / orbitalPeriodSeconds) * manager.timestep);
        Circle(Distance, manager.star.transform.position);
    }

    void Circle(float radius, Vector3 offset, float theta_scale = 0.01f) {
        float sizeValue = (2.0f * Mathf.PI) / theta_scale;
        int size = (int)sizeValue;
        size++;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.positionCount = size;

        Vector3 pos;
        float theta = 0f;
        for (int i = 0; i < size; i++) {
            theta += (2.0f * Mathf.PI * theta_scale);
            float x = radius * Mathf.Cos(theta);
            float z = radius * Mathf.Sin(theta);
            pos = new Vector3(x, 0, z);
            pos += offset;
            lineRenderer.SetPosition(i, pos);
        }
    }
}
