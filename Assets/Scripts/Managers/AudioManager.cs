using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    public AudioSource srcButton;
    public AudioSource srcSpawn;
    public AudioSource srcGruntFirst;
    public AudioSource srcGruntSecond;

    private bool isFirstGruntPlayed = false;

    private bool mute = false;
    public bool Mute
    {
        get => mute;
        set
        {
            mute = value;
            foreach (Transform child in transform)
            {
                child.GetComponent<AudioSource>().mute = value;
            }
        }
    }

    public static AudioManager Instance
    {
        get => instance;
    }

    private void Awake()
    {
        if (instance != null)
        {
            this.gameObject.SetActive(false);
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void PlayButtonSound()
    {
        srcButton.Play();
    }

    public void PlaySpawn()
    {
        srcSpawn.Play();
    }

    public void PlayGrunt()
    {
        if (!isFirstGruntPlayed)
        {
            srcGruntFirst.Play();
        }
        else
        {
            srcGruntSecond.Play();
        }

        isFirstGruntPlayed = !isFirstGruntPlayed;
    }

    public void PlayOneShot(AudioClip sound)
    {
    }
}
