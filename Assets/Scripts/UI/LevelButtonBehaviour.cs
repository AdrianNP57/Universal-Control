using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButtonBehaviour : MonoBehaviour, IPointerClickHandler
{
    // Parameters
    public int levelIndex;
    public float transitionTime;
    public float waitingTime;

    // External references
    private TransitionCircleBehaviour circle;

    private void Awake()
    {
        circle = FindObjectOfType<TransitionCircleBehaviour>();
        circle.GetComponent<Image>().rectTransform.sizeDelta = Vector2.zero;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        FindObjectOfType<AudioEffectPlayer>().OnSelect();
        StartCoroutine(LoadLevelCR());
    }

    private IEnumerator LoadLevelCR()
    {
        circle.Expand(transitionTime, gameObject);

        yield return new WaitForSeconds(transitionTime + waitingTime);

        SceneManager.LoadScene(levelIndex);
    }
}
