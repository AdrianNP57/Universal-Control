using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Throwable : MonoBehaviour
{
    // Drag gesture properties
    public DraggingEvent onDragging;

    private Vector2 dragStart;
    private Vector2 dragEnd;

    // Throw properties
    public float forceMultiplier;
    public float minThrowMagnitude;
    public float maxThrowMagnitude;
    private bool pendingThrow;

    // Object componets
    private Rigidbody rb;
    
    // Initial values
    private Vector3 initialPosition;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        pendingThrow = false;
        initialPosition = rb.position;
    }

    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            dragStart = Input.mousePosition;
        }

        if(Input.GetButton("Fire1"))
        {
            DraggingData data;

            dragEnd = Input.mousePosition;

            data.direction = CalcThrowVector();
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
            rb.AddForce(CalcThrowVector());
        }

        pendingThrow = false;
    }

    private Vector2 CalcThrowVector()
    {
        Vector2 worldStart = ToWorldPoint(dragStart);
        Vector2 worldEnd = ToWorldPoint(dragEnd);

        Vector2 throwVector = forceMultiplier * (worldStart - worldEnd);

        if(throwVector.magnitude < minThrowMagnitude)
        {
            throwVector = Vector2.zero;
        }
        else if(throwVector.magnitude > maxThrowMagnitude)
        {
            throwVector = throwVector.normalized * maxThrowMagnitude;
        }

        return throwVector;
    }

    private Vector3 ToWorldPoint(Vector2 point)
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(point.x, point.y, Camera.main.nearClipPlane));
    }
}

[Serializable]
public struct DraggingData
{
    public Vector2 direction;
    public float mass;
}

[Serializable]
public class DraggingEvent : UnityEvent<DraggingData> { }