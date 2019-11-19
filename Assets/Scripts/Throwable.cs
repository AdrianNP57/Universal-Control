using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Throwable : MonoBehaviour
{
    public float forceMultiplier;
    public DraggingEvent onDragging;

    private Rigidbody rb;

    private Vector2 dragStart;
    private Vector2 dragEnd;

    private bool pendingThrow;
    private Vector3 initialPosition;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        pendingThrow = false;
        initialPosition = rb.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            dragStart = Input.mousePosition;
        }

        if(Input.GetButton("Fire1"))
        {
            DraggingData data;
            Vector2 mousePosition = Input.mousePosition;

            data.direction = dragStart - mousePosition;
            data.forceMultiplier = forceMultiplier;
            data.mass = rb.mass;

            onDragging.Invoke(data);
        }

        if(Input.GetButtonUp("Fire1"))
        {
            dragEnd = Input.mousePosition;
            pendingThrow = true;
        }

        if(Input.GetKeyUp("r"))
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.position = initialPosition;
        }
    }

    private void FixedUpdate()
    {
        if(pendingThrow)
        {
            Vector2 direction = dragStart - dragEnd;
            rb.AddForce(forceMultiplier * direction);
        }

        pendingThrow = false;
    }
}

[Serializable]
public struct DraggingData
{
    public Vector2 direction;
    public float forceMultiplier;
    public float mass;
}

[Serializable]
public class DraggingEvent : UnityEvent<DraggingData> { }