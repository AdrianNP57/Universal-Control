using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitySingleton : MonoBehaviour
{
    // TODO this is so nasty but works for now
    private static int spawned = 0;

    void Awake()
    {
        if (spawned < 2)
        {
            spawned++;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }
}