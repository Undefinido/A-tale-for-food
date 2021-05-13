using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip defeatAudio, soundtrack, menuAudio, victoryAudio, forestSounds, damagesSound, footstepsSound, itemSound, lifeUpSound, wingsSound;
    public AudioClip menuA, ingameA;
    public AudioSource audSource;
    static AudioSource audioSrc, audioSrc2, sfxSrc;

    // Start is called before the first frame update
    void Start()
    {
        defeatAudio = Resources.Load<AudioClip>("Defeat");
        soundtrack = Resources.Load<AudioClip>("Ingame music");
        menuAudio = Resources.Load<AudioClip>("Menu music.wav");
        victoryAudio = Resources.Load<AudioClip>("Victory");
        forestSounds = Resources.Load<AudioClip>("Defeat");
        damagesSound = Resources.Load<AudioClip>("Defeat");
        footstepsSound = Resources.Load<AudioClip>("Defeat");
        itemSound = Resources.Load<AudioClip>("Defeat");
        lifeUpSound = Resources.Load<AudioClip>("Defeat");
        wingsSound = Resources.Load<AudioClip>("Defeat");

        audSource = GetComponent<AudioSource>();
        audioSrc = GetComponent<AudioSource>();
        audioSrc2 = GetComponent<AudioSource>();
        sfxSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlayDefeatAudio()
    {
        audioSrc.loop = false;
        audioSrc.PlayOneShot(defeatAudio);
    }

    public static void PlaySoundtrack()
    {
        audioSrc.loop = true;
        audioSrc.PlayOneShot(soundtrack);
    }
    public void PlaySoundtrack2()
    {
        audSource.loop = true;
        audSource.PlayOneShot(ingameA);
    }
    public void PlayMenuAudio2()
    {
        audSource.loop = true;
        audSource.PlayOneShot(menuA);
    }
    public static void PlayMenuAudio()
    {
        audioSrc.loop = true;
        audioSrc.PlayOneShot(menuAudio);
    }
    public static void PlayVictoryAudio()
    {
        audioSrc.loop = false;
        audioSrc.PlayOneShot(victoryAudio);
    }
    public static void PlayForestAudio()
    {
        audioSrc.loop = true;
        audioSrc2.PlayOneShot(forestSounds);
    }
    public static void PlayDamageAudio()
    {
        sfxSrc.loop = false;
        sfxSrc.PlayOneShot(damagesSound);
    }
    /*public static void PlayFootstepsAudio()
    {
        sfxSrc.loop = false;
        sfxSrc.PlayOneShot(damagesSound);
    }*/
    public static void PlayItemAudio()
    {
        sfxSrc.loop = false;
        sfxSrc.PlayOneShot(itemSound);
    }
    public static void PlayLifeUpSound()
    {
        sfxSrc.loop = false;
        sfxSrc.PlayOneShot(lifeUpSound);
    }
    /*
    public static void PlayWingsSound()
    {
        sfxSrc.loop = false;
        sfxSrc.PlayOneShot(wingsSound);
    }*/
}
