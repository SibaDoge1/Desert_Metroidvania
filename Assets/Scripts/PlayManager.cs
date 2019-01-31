using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    private static PlayManager instance = null;
    public static PlayManager Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        if (instance == null) instance = this;
    }

    void Start()
    {
        PlayerPrefs.SetString("Map", "A2");
        CameraManager.Instance.ResetMapInfo();
    }

    void Update()
    {

    }
}

