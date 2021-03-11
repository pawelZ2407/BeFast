﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;

    public static AudioManager instance { get { return _instance; } }

    [SerializeField] AudioClip laserConstant;
    [SerializeField] AudioClip smallBall;
    [SerializeField] AudioClip bigBall;
    [SerializeField] AudioClip bigBallCharge;

    [SerializeField] AudioSource backgroundMusicSource;
    [SerializeField] AudioSource SFXAudioSource;


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void PlayConstantLaser()
    {
        SFXAudioSource.clip = laserConstant;
        SFXAudioSource.loop = true;
        SFXAudioSource.Play();
    }
    public void PlaySmallDefault()
    {
        SFXAudioSource.loop = false;
        SFXAudioSource.PlayOneShot(smallBall);
    }
    public void PlayBigDefault()
    {
        SFXAudioSource.loop = false;
        SFXAudioSource.PlayOneShot(bigBall);
    }
    public void PlayBigCharge()
    {
        SFXAudioSource.loop = true;
        SFXAudioSource.clip = bigBallCharge;
        SFXAudioSource.Play();
    }
    public void StopSFXSound()
    {
        SFXAudioSource.loop = false;
        SFXAudioSource.Stop();
    }
}