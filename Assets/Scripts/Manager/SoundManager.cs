using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioSource bgmAudio;
    public GameObject player;
    AudioSource effectAudio;
    public Slider bgmSlider;
    public Slider effectSlider;

    public Toggle toggle;

    void Start()
    {
        effectAudio = player.GetComponent<AudioSource>();
        toggle.isOn = true;
    }


    void Update()
    {
        // Volume modification via slider
        bgmAudio.volume = bgmSlider.value;
        effectAudio.volume = effectSlider.value;

        if (toggle.isOn == false)
        {
            bgmAudio.volume = 0;
            effectAudio.volume = 0;
        }

    }


    private void OnToggleClick(Toggle toggle, bool isOn)
    {
        // Turning sound on or off via toggle
        bgmAudio.enabled = true;
        effectAudio.enabled = true;
    }
}
