using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitySingleton : MonoBehaviour
{
    private static bool spawned = false;
    void Awake()
    {
        if (!spawned)
        {
            spawned = true;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }
}