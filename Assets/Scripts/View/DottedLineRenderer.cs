using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO perhaps make this more eficient
public class DottedLineRenderer : MonoBehaviour
{
    // Object to display as a dot in the line
    public GameObject dotPrefab;
    public float dotScale;

    // Positions of the line
    [HideInInspector]
    public List<Vector3> positions = new List<Vector3>();

    // Dots to display
    private GameObject[] dots = new GameObject[200];

    private void Awake()
    {
        for(int i = 0; i < dots.Length; i++)
        {
            dots[i] = Instantiate(dotPrefab, transform);
            dots[i].transform.localScale = dotScale * Vector3.one;
        }

        DisableAll();
    }

    void Update()
    {
        DisableAll();

        for (int i = 0; i < positions.Count && i < dots.Length; i++)
        {
            dots[i].transform.position = positions[i];
            dots[i].SetActive(true);
        }
    }

    private void DisableAll()
    {
        for(int i = 0; i < dots.Length; i++)
        {
            dots[i].SetActive(false);
        }
    }
}
