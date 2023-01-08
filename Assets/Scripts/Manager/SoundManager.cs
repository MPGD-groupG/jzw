using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioSource bgmAudio;
    public GameObject player;
    public GameObject weapon;
    AudioSource effectPlayerAudio;
    AudioSource effectWeaponAudio;

    public Slider bgmSlider;
    public Slider effectSlider;

    public Toggle toggle;

    void Start()
    {
        effectPlayerAudio = player.GetComponent<AudioSource>();
        effectWeaponAudio = weapon.GetComponent<AudioSource>();
        toggle.isOn = true;
    }


    void Update()
    {
        // Volume modification via slider
        bgmAudio.volume = bgmSlider.value;
        effectPlayerAudio.volume = effectSlider.value;
        effectWeaponAudio.volume = effectSlider.value;

        if (toggle.isOn == false)
        {
            bgmAudio.volume = 0;
            effectPlayerAudio.volume = 0;
            effectWeaponAudio.volume = 0;
        }

    }


    private void OnToggleClick(Toggle toggle, bool isOn)
    {
        // Turning sound on or off via toggle
        bgmAudio.enabled = true;
        effectPlayerAudio.enabled = true;
        effectWeaponAudio.enabled = true;
    }
}
