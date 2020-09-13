using UnityEngine;
using static Manager;


public class Planet : MonoBehaviour{
    // Fields

    LineRenderer lineRenderer;


    // Properties
    public float Distance { set; get; }
   
    public int Id { get; set; }

    // Methods
    private void Awake() {
        lineRenderer = gameObject.GetComponent<LineRenderer>();
    }

    private void Update() {
        Orbit();
    }

    void Orbit() {
        Circle(Distance, manager.activeStar.transform.position);
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
