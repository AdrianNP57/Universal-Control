﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRestarter : MonoBehaviour
{
    // Parameters

    // External references
    private GameObject asteroid;
    private GameObject god;

    private void Awake()
    {
        asteroid = GameObject.FindGameObjectWithTag("Asteroid");
        god = GameObject.FindGameObjectWithTag("God");
    }

    public void RestartLevel()
    {
        asteroid.GetComponent<Throwable>().Restart();

        god.GetComponent<GodAnimator>().RestartPosition();
        god.GetComponent<GodRotator>().RestartRotation();

        foreach(Attractor attractor in FindObjectsOfType<Attractor>())
        {
            attractor.Clear();
        }
    }
}
