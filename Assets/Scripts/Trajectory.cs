using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
    // Current iteration properties
    private Vector3 position;
    private Vector3 velocity;
    private Vector3 acceleration;
    private Vector3 newAcceleration;
    private List<Attractor> influencingAttractors = new List<Attractor>();

    // Trajectory properties
    public int dotsCount;
    public float dotsStepSeconds;
    private float timeUntilDot;

    // Line renderer
    private DottedLineRenderer line;

    void Awake()
    {
        line = GetComponent<DottedLineRenderer>();
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
        position = CalcNewPosition(position, velocity, acceleration);
        newAcceleration = CalcNewAcceleration(position, data.mass);
        velocity = CalcNewVelocity(velocity, acceleration, newAcceleration);
        acceleration = newAcceleration;

        UpdateLine();
        UpdateInfluecingAttractors();
    }

    private Vector3 CalcNewPosition(Vector3 pos, Vector3 vel, Vector3 acc)
    {
        return pos + vel * Time.fixedDeltaTime + 0.5f * acc * Mathf.Pow(Time.fixedDeltaTime, 2);
    }

    private Vector3 CalcNewVelocity(Vector3 vel, Vector3 acc, Vector3 newAcc)
    {
        return vel + (acc + newAcc) * 0.5f * Time.fixedDeltaTime;
    }

    private Vector3 CalcNewAcceleration(Vector3 pos, float mass)
    {
        Vector3 newAcc = Vector3.zero;

        foreach (Attractor attractor in influencingAttractors)
        {
            newAcc += attractor.CalculateForce(pos, mass) / mass;
        }

        return newAcc;
    }

    private void UpdateLine()
    {
        timeUntilDot -= Time.fixedDeltaTime;

        if (timeUntilDot < 0)
        {
            line.positions.Add(position);
            timeUntilDot = dotsStepSeconds;
        }
    }

    private void UpdateInfluecingAttractors()
    {
        GameObject asteroid = GameObject.FindGameObjectWithTag("Player");
        SphereCollider asteroidCollider = asteroid.GetComponent<SphereCollider>();

        float asteroidScale = asteroid.transform.localScale.x;
        float sphereRadius = asteroidCollider.radius * asteroidScale;
        Vector3 sphereCenter = position + asteroidCollider.center * asteroidScale;

        Collider[] attractorsColliders = Physics.OverlapSphere(sphereCenter, sphereRadius);

        influencingAttractors.Clear();

        foreach (Collider collider in attractorsColliders)
        {
            Attractor attractor = collider.gameObject.GetComponent<Attractor>();

            if (collider.isTrigger && attractor != null)
            {
                influencingAttractors.Add(attractor);
            }
        }
    }
}
