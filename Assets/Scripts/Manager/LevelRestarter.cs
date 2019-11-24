using System.Collections;
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


    private void Update()
    {
        if (Input.GetKeyUp("r"))
        {
            RestartLevel();
        }
    }

    public void RestartLevel()
    {
        asteroid.GetComponent<Throwable>().RestartPosition();

        god.GetComponent<GodAnimator>().RestartPosition();
        god.GetComponent<GodRotator>().RestartRotation();
    }
}
