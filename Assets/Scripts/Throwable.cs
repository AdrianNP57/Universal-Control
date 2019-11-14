using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    public float forceMultiplier;

    private Rigidbody rb;

    private Vector2 dragStart;
    private Vector2 dragEnd;

    private bool pendingThrow;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        pendingThrow = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            dragStart = Input.mousePosition;
        }

        if(Input.GetButtonUp("Fire1"))
        {
            dragEnd = Input.mousePosition;
            pendingThrow = true;
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
