﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Throwable : MonoBehaviour
{
    // Events
    public DraggingEvent onDragging;
    public UnityEvent onThrow;

    // Drag gesture 
    private Vector2 dragStart;
    private Vector2 dragEnd;

    // Throw properties
    public float forceMultiplier;
    public float minThrowMagnitude;
    public float maxThrowMagnitude;
    public float maxStartPointRange;

    private Vector2 throwVector = Vector2.zero;
    private bool validStart = false;
    private bool alreadyThrown = false;

    // Object componets
    private Rigidbody rb;
    
    // Initial values
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        initialPosition = rb.position;
        initialRotation = rb.rotation;
    }

    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            dragStart = Input.mousePosition;
            validStart = ((Vector2)(ToWorldPoint(dragStart) - initialPosition)).magnitude < maxStartPointRange;
        }

        if(validStart && !alreadyThrown)
        {
            if (Input.GetButton("Fire1"))
            {
                DraggingData data;

                dragEnd = Input.mousePosition;

                data.direction = CalcThrowVector();
                data.mass = rb.mass;
                data.clampedStrength = data.direction.magnitude / maxThrowMagnitude;

                onDragging.Invoke(data);
            }

            if (Input.GetButtonUp("Fire1"))
            {
                dragEnd = Input.mousePosition;
                throwVector = CalcThrowVector();
                    
                if(throwVector.magnitude > 0)
                {
                    onThrow.Invoke();
                    alreadyThrown = true;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (throwVector.magnitude > 0)
        {
            rb.AddForce(CalcThrowVector());
        }

        throwVector = Vector2.zero;
    }

    public void Restart()
    {
        rb.position = initialPosition;
        rb.rotation = initialRotation;

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        alreadyThrown = false;
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
        return Camera.main.ScreenToWorldPoint(new Vector3(point.x, point.y, -Camera.main.transform.position.z));
    }

    private void OnBecameInvisible()
    {
        if(gameObject.CompareTag("Asteroid"))
        {
            FindObjectOfType<LevelRestarter>()?.RestartLevel();
        }
    }
}

[Serializable]
public struct DraggingData
{
    public Vector2 direction;
    public float mass;
    public float clampedStrength;
}

[Serializable]
public class DraggingEvent : UnityEvent<DraggingData> { }