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
        circle = Object.FindObjectOfType<TransitionCircleBehaviour>();
    }

    private void Start()
    {
        circle.Shrink(transitionTime);
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
        circle.Expand(transitionTime);

        yield return new WaitForSeconds(transitionTime + waitingTime);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);        
    }
}
