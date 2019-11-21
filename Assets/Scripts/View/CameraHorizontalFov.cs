using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class CameraHorizontalFov : MonoBehaviour
{
    // Parameters
    public float hFov;

    // Components
    private Camera camera;

    private void Awake()
    {
        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        float hFovRad = hFov * Mathf.Deg2Rad;
        float camH = Mathf.Tan(hFovRad * 0.5f) / camera.aspect;
        float vFovRad = Mathf.Atan(camH) * 2;

        camera.fieldOfView = vFovRad * Mathf.Rad2Deg;
    }
}
