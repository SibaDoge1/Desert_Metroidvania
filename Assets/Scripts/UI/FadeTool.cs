﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void voidFunc();

public class FadeTool : MonoBehaviour
{
    private static FadeTool instance;
    public static FadeTool Instance
    {
        get { return instance; }
    }

    private Image fade;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null) instance = this;
        else
        {
            Debug.LogError("Singleton Error! : " + this.name);
            Destroy(this);
        }
        fade = GameObject.Find("Canvas").transform.Find("Fade").GetComponent<Image>();
    }

    public void FadeOut(float time, voidFunc func)
    {
        StartCoroutine(FadeOutRoutine(time, func));
    }

    public void FadeIn(float time, voidFunc func)
    {
        StartCoroutine(FadeInRoutine(time, func));
    }
    public void FadeInOut(float time, voidFunc func)
    {
        StartCoroutine(FadeInOutRoutine(time, func));
    }

    IEnumerator FadeOutRoutine(float time, voidFunc func)
    {
        Color DefaultCol = fade.color;
        Color col = fade.color;
        float curtime = 0f;
        while (col.a < 1)
        {
            col = fade.color;
            curtime += Time.deltaTime;
            col.a = Mathf.Lerp(DefaultCol.a, 1, curtime / time);
            fade.color = col;
            yield return null;
        }
        if (func != null)
        {
            func();
        }
    }

    IEnumerator FadeInRoutine(float time, voidFunc func)
    {
        Color DefaultCol = fade.color;
        Color col = fade.color;
        float curtime = 0f;
        while (col.a > 0)
        {
            col = fade.color;
            curtime += Time.deltaTime;
            col.a = Mathf.Lerp(DefaultCol.a, 0, curtime / time);
            fade.color = col;
            yield return null;
        }
        if (func != null)
        {
            func();
        }
    }

    IEnumerator FadeInOutRoutine(float time, voidFunc func)
    {
        Color DefaultCol = fade.color;
        Color col = fade.color;
        float curtime = 0f;
        while (col.a < 1)
        {
            col = fade.color;
            curtime += Time.deltaTime;
            col.a = Mathf.Lerp(DefaultCol.a, 1, curtime / time);
            fade.color = col;
            yield return null;
        }
        if (func != null)
        {
            func();
        }
        DefaultCol = fade.color;
        col = fade.color;
        curtime = 0f;
        while (col.a > 0)
        {
            col = fade.color;
            curtime += Time.deltaTime;
            col.a = Mathf.Lerp(DefaultCol.a, 0, curtime / time);
            fade.color = col;
            yield return null;
        }
    }
}