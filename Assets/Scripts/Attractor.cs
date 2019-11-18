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

    private void Attract(Rigidbody objectInArea)
    {
        objectInArea.AddForce(CalculateForce(objectInArea.position, objectInArea.mass));
    }

    private void OnTriggerEnter(Collider other)
    {
        objectsInArea.Add(other.gameObject.GetComponent<Rigidbody>());
    }

    private void OnTriggerExit(Collider other)
    {
        objectsInArea.Remove(other.gameObject.GetComponent<Rigidbody>());
    }

    public Vector3 CalculateForce(Vector3 position, float mass)
    {
        Vector3 direction = rb.position - position;
        float distance = direction.magnitude;

        if (distance == 0f)
            return Vector3.zero;

        float forceMagnitude = G * (rb.mass * mass) / Mathf.Pow(distance, 2);

        return direction.normalized * forceMagnitude;
    }

    public float GetRadius()
    {
        foreach (SphereCollider collider in GetComponents<SphereCollider>())
        {
            if(collider.isTrigger)
            {
                return collider.radius * transform.localScale.x;
            }
        }

        return 0f;
    }
}