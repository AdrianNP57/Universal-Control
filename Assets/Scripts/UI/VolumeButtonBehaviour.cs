using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeButtonBehaviour : MonoBehaviour
{
    // Paramaters
    public Sprite volumeOff;
    public Sprite volumeOn;

    // References
    private AudioSource music;
    private AudioSource fx;

    // Initial
    private float initialMusicVolume;
    private float initialFxVolume;

    // State
    private bool soundEnabled = true;

    // Components
    private Image image;

    void Start()
    {
        image = GetComponent<Image>();

        music = FindObjectOfType<BackgroundMusicBehaviour>().gameObject.GetComponent<AudioSource>();
        fx = FindObjectOfType<AudioEffectPlayer>().gameObject.GetComponent<AudioSource>();

        initialMusicVolume = music.volume;
        initialFxVolume = fx.volume;
    }

    public void OnClicked()
    {
        ChangeVolumeState(!soundEnabled);
    }

    private void ChangeVolumeState(bool enable)
    {
        music.volume = enable ? initialMusicVolume : 0f;
        fx.volume = enable ? initialFxVolume : 0f;

        image.sprite = enable ? volumeOff : volumeOn;
        soundEnabled = enable;

        if(enable)
        {
            music.Stop();
            music.Play();
        }
    }
}
