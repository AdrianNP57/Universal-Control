using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodRotator : MonoBehaviour
{
    // Target
    private Vector3 rotateAround;

    // Initial values
    private Vector3 initialPos;
    private Quaternion initialRot;

    private void Awake()
    {
        rotateAround = GameObject.FindGameObjectWithTag("Asteroid").transform.position;

        initialPos = transform.position;
        initialRot = transform.rotation;
    }

    public void OnDragging(DraggingData data)
    {
        float angle = Vector3.SignedAngle(Vector3.right, data.direction, Vector3.forward);

        transform.position = initialPos;
        transform.rotation = initialRot;

        transform.RotateAround(rotateAround, Vector3.forward, angle);
    }

    public void RestartRotation()
    {
        transform.position = initialPos;
        transform.rotation = initialRot;
    }
}
