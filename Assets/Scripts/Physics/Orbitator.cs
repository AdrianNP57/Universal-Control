using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbitator : MonoBehaviour
{
    // Parameters
    public Transform orbitCenter;
    public float orbitationVelocity;

    // State
    private float currentAngle;

    // Initial values
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    void Awake()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = initialPosition;
        transform.rotation = initialRotation;

        transform.RotateAround(orbitCenter.position, Vector3.forward, currentAngle);

        currentAngle += orbitationVelocity * Time.deltaTime;
    }
}
