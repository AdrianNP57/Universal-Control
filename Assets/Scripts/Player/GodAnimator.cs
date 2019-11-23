using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO refactor
public class GodAnimator : MonoBehaviour
{
    // Fingers properties
    public GameObject[] indices;
    public FingerRange[] preThrowRanges;
    public int[] throwMaxs;

    // Animation
    public float throwTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void IdleAnimation()
    {

    }

    public void PreThrowAnimation(DraggingData data)
    {
        for(int i = 0; i < indices.Length; i++)
        {
            Vector3 rotation = indices[i].transform.localRotation.eulerAngles;
            rotation.z = Mathf.LerpAngle(preThrowRanges[i].min, preThrowRanges[i].max, data.clampedStrength);

            indices[i].transform.localRotation = Quaternion.Euler(rotation);
        }
    }

    public void ThrowAnimation()
    {
        StartCoroutine(RotateOverTime(throwTime));
    }

    private IEnumerator RotateOverTime(float seconds)
    {
        float time = 0;
        float[] initialValues = new float[indices.Length];

        for(int i = 0; i < indices.Length; i++)
        {
            initialValues[i] = indices[i].transform.localRotation.eulerAngles.z;
        }
        
        while(time < seconds)
        {
            for (int i = 0; i < indices.Length; i++)
            { 
                Vector3 rotation = indices[i].transform.localRotation.eulerAngles;
                rotation.z = Mathf.LerpAngle(initialValues[i], throwMaxs[i], time / seconds);

                indices[i].transform.localRotation = Quaternion.Euler(rotation);

                time += Time.deltaTime;
            }

            yield return new WaitForEndOfFrame();
        }

        for(int i = 0; i < indices.Length; i++)
        {
            Vector3 rotation = indices[i].transform.localRotation.eulerAngles;
            rotation.z = throwMaxs[i];

            indices[i].transform.localRotation = Quaternion.Euler(rotation);
        }
    }
}

[Serializable]
public struct FingerRange
{
    public int min;
    public int max;
}