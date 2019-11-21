using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO perhaps make this more eficient
public class DottedLineRenderer : MonoBehaviour
{
    // Object to display as a dot in the line
    public GameObject dotPrefab;

    // Positions of the line
    [HideInInspector]
    public List<Vector3> positions = new List<Vector3>();

    // Dots to display
    private List<GameObject> dots = new List<GameObject>();

    void Update()
    {
        foreach(GameObject dot in dots)
        {
            Destroy(dot);
        }

        dots.Clear();

        foreach(Vector3 position in positions)
        {
            GameObject dot = Instantiate(dotPrefab, transform);
            dot.transform.position = position;

            dots.Add(dot);
        }
    }
}
