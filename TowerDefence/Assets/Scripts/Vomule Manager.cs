using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VomuleManager : MonoBehaviour
{
    [HeaderAttribute("Source")]
    public AudioSource musicSource;
    public AudioSource effectsSource;

    [HeaderAttribute("Sounds")]
    public AudioClip background;
    public AudioClip hitFromNormalAndDMG;
    public AudioClip hitFromFreeze;
    public AudioClip boom;
    public AudioClip enemyDed;
    public AudioClip enemyHitStructures;
    public AudioClip turretHit;
    public static VomuleManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void playEffect(AudioClip clip)
    {
        effectsSource.PlayOneShot(clip);
    }
}
