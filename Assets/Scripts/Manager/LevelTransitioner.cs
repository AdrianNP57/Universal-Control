using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransitioner : MonoBehaviour
{
    // Parameters
    public float transitionTime;
    public float waitingTime;
    public int levelCount;

    // State variables
    private bool called = false;

    // External references
    private TransitionCircleBehaviour circle;

    // Start is called before the first frame update
    void Awake()
    {
        circle = FindObjectOfType<TransitionCircleBehaviour>();
    }

    private void Start()
    {
        circle.Shrink(transitionTime, GameObject.FindGameObjectWithTag("BlackHole"));
    }

    public void NextLevel()
    {
        if(!called)
        {
            StartCoroutine(NextLevelCR());
        }

        called = true;
    }

    private IEnumerator NextLevelCR()
    {
        circle.Expand(transitionTime, GameObject.FindGameObjectWithTag("BlackHole"));

        yield return new WaitForSeconds(transitionTime + waitingTime);

        int targetBuildIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if(targetBuildIndex <= levelCount)
        {
            SceneManager.LoadScene(targetBuildIndex);
        }
        else
        {
            SceneManager.LoadScene("MainMenu");

        }
    }
}
