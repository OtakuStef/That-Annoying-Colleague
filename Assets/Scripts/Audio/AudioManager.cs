using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
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
    public void StopSFX()
    {
        SFXSource.Stop();
    }
    public AudioClip CurrentClip
    {
        get { return SFXSource.clip; }
    }
}
