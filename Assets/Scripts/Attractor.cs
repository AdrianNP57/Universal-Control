using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    private const float G = 0.6674f;

    private Rigidbody rb;
    private static List<Rigidbody> objectsInArea = new List<Rigidbody>();

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        foreach (Rigidbody objectInArea in objectsInArea)
        {
            Attract(objectInArea);
        }
    }

    void Attract(Rigidbody objectInArea)
    {
        Vector3 direction = rb.position - objectInArea.position;
        float distance = direction.magnitude;

        if (distance == 0f)
            return;

        float forceMagnitude = G * (rb.mass * objectInArea.mass) / Mathf.Pow(distance, 2);
        Vector3 force = direction.normalized * forceMagnitude;

        objectInArea.AddForce(force);
    }

    private void OnTriggerEnter(Collider other)
    {
        objectsInArea.Add(other.gameObject.GetComponent<Rigidbody>());
    }

    private void OnTriggerExit(Collider other)
    {
        objectsInArea.Remove(other.gameObject.GetComponent<Rigidbody>());
    }
}