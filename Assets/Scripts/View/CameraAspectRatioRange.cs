using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class CameraAspectRatioRange : MonoBehaviour
{
    // Parameters
    public float minAspect;
    public float maxAspect;

    // Components
    private Camera camera;

    void Awake()
    {
        camera = GetComponent<Camera>();
    }

    void Update()
    {
        float aspect = (float)Screen.width / (float)Screen.height;

        if (aspect <= minAspect)
        {
            Rect rect = camera.rect;
            float scaleHeight = aspect / minAspect;

            rect.width = 1f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1f - scaleHeight) / 2f;

            camera.rect = rect;
        }
        else if(aspect >= maxAspect)
        {
            float scalewidth = maxAspect / aspect;
            Rect rect = camera.rect;

            rect.width = scalewidth;
            rect.height = 1f;
            rect.x = (1f - scalewidth) / 2f;
            rect.y = 0;

            camera.rect = rect;
        }
        else
        {
            Rect rect = camera.rect;

            rect.width = rect.height = 1;
            rect.x = rect.y = 0;

            camera.rect = rect;
        }
    }
}
