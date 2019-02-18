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

    void Awake()
    {
        if (instance == null) instance = this;
        else
        {
            Debug.LogError("Singleton Error! : " + this.name);
            Destroy(this);
        }
        SaveManager.LoadAll();
        viewer = GameObject.Find("Canvas").transform.Find("MapViewer").GetComponent<MapViewer>();
    }

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            viewer.Toggle();
        }
    }

    public bool isTestMode = true;

    private Player player = null;
    public Player Player
    {
        get { return player; } set { player = value; }
    }
}

