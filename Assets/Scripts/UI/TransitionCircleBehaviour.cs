using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionCircleBehaviour : MonoBehaviour
{
    // State variables
    private bool expand = false;
    private bool shrink = false;
    private float animationTime;

    // Components
    private RectTransform rect;

    // Parameters
    public bool startInvisible;

    void Awake()
    {
        rect = GetComponent<RectTransform>();

        if(!startInvisible)
        {
            rect.sizeDelta = CalcMaxSize() * Vector2.one;
        }
    }

    private void Update()
    {
        float maxSize = CalcMaxSize();
        float sizeChangePerSecond = maxSize / animationTime;

        if(shrink)
        {
            rect.sizeDelta -= sizeChangePerSecond * Time.deltaTime * Vector2.one;

            if(rect.sizeDelta.x <= 0)
            {
                rect.sizeDelta = Vector2.zero;
                shrink = false;
            }
        }

        if (expand)
        {
            rect.sizeDelta += sizeChangePerSecond * Time.deltaTime * Vector2.one;
        }
    }

    public void Shrink(float time, GameObject center)
    {
        shrink = true;
        animationTime = time;

        rect.anchoredPosition = CalcCenter(center);
    }

    public void Expand(float time, GameObject center)
    {
        expand = true;
        animationTime = time;

        rect.anchoredPosition = CalcCenter(center);
    }

    private Vector2 CalcCenter(GameObject go)
    {
        Vector3 worldCenter = go.transform.position;

        return Camera.main.WorldToScreenPoint(worldCenter);
    }

    private float CalcMaxSize()
    {
        return 2 * Mathf.Max(Screen.width, Screen.height);
    }
}
