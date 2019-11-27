using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEffectPlayer : MonoBehaviour
{
    // Audio clips
    public AudioClip select;

    // Components
    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void OnSelect()
    {
        source.PlayOneShot(select);
    }
}
