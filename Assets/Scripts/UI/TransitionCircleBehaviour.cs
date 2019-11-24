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

    void Awake()
    {
        rect = GetComponent<RectTransform>();

        rect.anchoredPosition = CalcCenter();
        rect.sizeDelta = CalcMaxSize() * Vector2.one;
    }

    private void Update()
    {
        float maxSize = CalcMaxSize();
        float sizeChangePerSecond = maxSize / animationTime;

        rect.anchoredPosition = CalcCenter();

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

    public void Shrink(float time)
    {
        shrink = true;
        animationTime = time;
    }

    public void Expand(float time)
    {
        expand = true;
        animationTime = time;
    }

    private Vector2 CalcCenter()
    {
        Vector3 worldCenter = GameObject.FindGameObjectWithTag("BlackHole").transform.position;

        return Camera.main.WorldToScreenPoint(worldCenter);
    }

    private float CalcMaxSize()
    {
        return 2 * Mathf.Max(Screen.width, Screen.height);
    }
}
