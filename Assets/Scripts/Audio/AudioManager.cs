using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    [Header("----- Audio Source -----")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource gameMusicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("----- Audio Clip -----")]
    //public AudioClip background_main_menu;
    public AudioClip game_music;
    public AudioClip background_busy_office;
    public AudioClip steps_01;
    public AudioClip steps_running_01;
    public AudioClip got_hit_01;
    public AudioClip powerup_pickup;
    public AudioClip first_blood;

    private void Start()
    {
        musicSource.clip = background_busy_office;
        gameMusicSource.clip = game_music;
        musicSource.Play();
        gameMusicSource.Play();
    }

    public void PlaySFX(AudioClip clip, bool loop)
    {
        //SFXSource.PlayOneShot(clip);
        SFXSource.clip = clip;
        SFXSource.loop = loop;
        SFXSource.Play();
    }

    public void PlaySFXOneShot(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
    public void StopSFX()
    {
        SFXSource.Stop();
    }
    public AudioClip CurrentClip
    {
        get { return SFXSource.clip; }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
