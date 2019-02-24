using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{ 

    void Awake()
    {
    }

    void OnEnable()
    {
    }

    public void On()
    {
        Time.timeScale = 0f;
        gameObject.SetActive(true);
    }

    public void Off()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

    public void OnResumeButtonDown()
    {
        Off();
    }

    public void OnCheckPointButtonDown()
    {
        PlayManager.Instance.Return();
    }

    public void OnTitleButtonDown()
    {
        
    }

    public void OnExitButtonDown()
    {
        Application.Quit();
    }
}
