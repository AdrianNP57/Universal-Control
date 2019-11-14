using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
    private DottedLineRenderer line;

    public int dotsCount;
    public float dotsStepSeconds;

    private Attractor[] attractors;

    private Vector3 position;
    private Vector3 velocity;
    private Vector3 acceleration;

    private float timeUntilDot;

    // Start is called before the first frame update
    void Awake()
    {
        line = GetComponent<DottedLineRenderer>();
        attractors = FindObjectsOfType<Attractor>();
    }

    public void OnDragging(DraggingData data)
    {
        line.positions.Clear();

        position = transform.position;
        acceleration = data.forceMultiplier * data.direction / data.mass;
        velocity = acceleration * 0.5f * Time.fixedDeltaTime;
        timeUntilDot = 0;

        for (int i = 0; i < dotsCount * (dotsStepSeconds / Time.fixedDeltaTime); i++)
        {
            SimulatePosition(data);
        }
    }

    private void SimulatePosition(DraggingData data)
    {
        Vector3 newPosition = position + velocity * Time.fixedDeltaTime + 0.5f * acceleration * Mathf.Pow(Time.fixedDeltaTime, 2);
        Vector3 newAcceleration = Vector3.zero;

        foreach (Attractor attractor in attractors)
        {
            newAcceleration += CalculateAttractorAcceleration(data, attractor);
        }

        Vector3 newVelocity = velocity + (acceleration + newAcceleration) * 0.5f * Time.fixedDeltaTime;

        position = newPosition;
        velocity = newVelocity;
        acceleration = newAcceleration;

        timeUntilDot -= Time.fixedDeltaTime;
        if(timeUntilDot < 0)
        {
            line.positions.Add(position);
            timeUntilDot = dotsStepSeconds;
        }
    }

    private Vector3 CalculateAttractorAcceleration(DraggingData data, Attractor attractor)
    {
        float distance = Vector3.Distance(position, attractor.transform.position);

        if(distance < attractor.GetRadius())
        {
            return attractor.CalculateForce(position, data.mass) / data.mass;
        }

        return Vector3.zero;
    }
}
