﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    private bool isOn = false;
    void Awake()
    {
    }

    void OnEnable()
    {
    }

    public void update()
    {
        Debug.Log(Time.timeScale);
    }

    public void Toggle()
    {
        SoundDelegate.Instance.PlayEffectSound(EffectSoundType.Button);

        if (!isOn)
        {
            On();
        }
        else
        {
            Off();
        }
    }

    public void On()
    {
        isOn = true;
        Time.timeScale = 0f;
        Time.fixedDeltaTime = 0f;
        gameObject.SetActive(true);
    }

    public void Off()
    {
        isOn = false;
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

    public void OnResumeButtonDown()
    {
        SoundDelegate.Instance.PlayEffectSound(EffectSoundType.Button);
        Off();
    }

    public void OnCheckPointButtonDown()
    {
        SoundDelegate.Instance.PlayEffectSound(EffectSoundType.Button);
        Off();
        PlayManager.Instance.Return();
    }

    public void OnTitleButtonDown()
    {
        SoundDelegate.Instance.PlayEffectSound(EffectSoundType.Button);
        GlobalData.SetChangeScene("Scenes/MainMenu");
        Time.timeScale = 1f;
        FadeTool.Instance.FadeInOut(1f, 0.5f, LoadScene);
    }

    public void LoadScene()
    {
        SceneManager.LoadScene("Scenes/LoadingScene");
    }
    public void OnExitButtonDown()
    {
        SoundDelegate.Instance.PlayEffectSound(EffectSoundType.Button);
        Application.Quit();
    }

}
