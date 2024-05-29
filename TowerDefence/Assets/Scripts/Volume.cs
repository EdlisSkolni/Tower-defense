using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    [HeaderAttribute("Mixer")]
    public AudioMixer audioMixer;

    [HeaderAttribute("Sliders")]
    public Slider MusicSlider;
    public Slider soundsSlider;

    public void Start()
    {
        if (PlayerPrefs.HasKey("musicSettings"))
        {
            load();
        }
        else
        {
            setMusic();
            setSounds();
        }
    }

    public void setMusic()
    {
        float volume = MusicSlider.value;
        audioMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("musicSettings", volume);
    }
    public void setSounds()
    {
        float volume = soundsSlider.value;
        audioMixer.SetFloat("Sounds", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("soundsSettings", volume);
    }

    private void load()
    {
        MusicSlider.value = PlayerPrefs.GetFloat("musicSettings");
        soundsSlider.value = PlayerPrefs.GetFloat("soundsSettings");
        setMusic();
        setSounds();
    }
}
