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
    private MapViewer viewer;
    private bool isFirstSaved = false;

    void Awake()
    {
        if (instance == null) instance = this;
        else
        {
            Debug.LogError("Singleton Error! : " + this.name);
            Destroy(this);
        }
        SaveManager.FirstLoad();
        viewer = GameObject.Find("Canvas").transform.Find("MapViewer").GetComponent<MapViewer>();
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    void Start()
    {
        SaveManager.ApplySave();
    }

    void Update()
    {
        if (MyInput.GetKeyDown(MyKeyCode.Map))
        {
            viewer.Toggle();
        }
        if (!isFirstSaved)
        {
            SaveManager.SaveToFile();
            isFirstSaved = true;
        }
    }

    public void Defeat()
    {
        player.gameObject.SetActive(false);
        FadeTool.Instance.FadeInOut(1f, ReturnToCheckPoint);
    }

    public void ReturnToCheckPoint()
    {
        player.gameObject.SetActive(true);
        SaveManager.ApplySave();
    }

    public bool isTestMode = true;

    private Player player = null;
    public Player Player
    {
        get { return player; } set { player = value; }
    }

}

