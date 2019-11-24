using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LevelButtonBehaviour : MonoBehaviour, IPointerClickHandler
{
    // Parameters
    public int levelIndex;

    public void OnPointerClick(PointerEventData eventData)
    {
        SceneManager.LoadScene(levelIndex);
    }
}
