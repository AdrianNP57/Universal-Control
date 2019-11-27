using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunPlanetCollisionBehaviour : MonoBehaviour
{
    // Parameters
    public float timeBeforeRestart;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Sun"))
        {
            Restart();
        }
        else if (collision.gameObject.CompareTag("Planet"))
        {
            StartCoroutine(RestartAfterDelay());
        }
    }

    private IEnumerator RestartAfterDelay()
    {
        yield return new WaitForSeconds(timeBeforeRestart);

        Restart();
    }

    private void Restart()
    {
        FindObjectOfType<LevelRestarter>().RestartLevel();
    }
}
