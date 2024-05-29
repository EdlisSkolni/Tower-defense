using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VomuleManager : MonoBehaviour
{
    [HeaderAttribute("Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource effectsSource;

    [HeaderAttribute("Sounds")]
    public AudioClip background; //here in start
    public AudioClip hitFromNormalAndDMG; //implemented
    public AudioClip hitFromFreeze; //implemented
    public AudioClip boom; //implemented
    public AudioClip enemyDed; //implemented
    public AudioClip enemyHitStructures; //implemented
    public AudioClip turretHit; //implemented
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
