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
            Debug.LogWarning("Singleton Error! : " + this.name);
            Destroy(this);
        }
        SaveManager.FirstLoad(GlobalData.isNewStart);
        viewer = GameObject.Find("Canvas").transform.Find("MapViewer").GetComponent<MapViewer>();
        player = GameObject.Find("Player").GetComponent<Player>();
        player.MakePalette();
    }

    void Start()
    {
        SaveManager.ApplySave();
        FadeTool.Instance.FadeIn(1f, null);
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

    public void Return()
    {
        FadeTool.Instance.FadeInOut(1f, 1f, ReturnToCheckPoint);
    }

    public void ReturnToCheckPoint()
    {
        player.Reset();
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

