﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundObject : MonoBehaviour
{
    private bool isPlayed = false;
    private AudioSource src;
    public void Play()
    {
        src = transform.GetComponent<AudioSource>();
        src.Play();
        isPlayed = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayed && !src.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}