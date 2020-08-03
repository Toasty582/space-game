using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static RandomSystem;
using static Manager;

public class Planet : MonoBehaviour{

    // Properties
    public int Distance { get; set; }
    public int Id { get; private set; }


    // Methods
    public void ConfigurePlanet(int planetDistance, int planetID) {
        float planetInitialPosition = Random.Range(0f, 359f);

        this.gameObject.transform.Rotate(new Vector3(0, planetInitialPosition, 0));
        this.gameObject.transform.Translate(planetDistance, 0, planetDistance);

        Distance = planetDistance;
        Id = planetID;
    }
}
