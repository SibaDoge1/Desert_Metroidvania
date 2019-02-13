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
        else
        {
            Debug.LogError("Singleton Error! : " + this.name);
            Destroy(this);
        }
    }

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Character>();
        //PlayerPrefs.SetString("Map", "A2");
    }

    void Update()
    {

    }

    public bool isTestMode = true;

    private Character player = null;
    public Character Player
    {
        get { return player; } set { player = value; }
    }
}

