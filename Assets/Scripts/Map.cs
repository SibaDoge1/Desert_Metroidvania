using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    private static Map instance = null;
    public static Map Instance
    {
        get { return instance; }
    }

    public Stage CurStage { get; private set; }

    void Awake()
    {
        if (instance == null) instance = this;
        else
        {
            Debug.LogError("Singleton Error! : " + this.name);
            Destroy(gameObject);
        }
    }

    void Start()
    {
        CurStage = Player.Instance.transform.parent.parent.GetComponent<Stage>();
        Debug.Log("Satge : " + CurStage.gameObject.name);
    }

    public void changeStage(Stage from, Stage to)
    {
        CurStage = to;
        Debug.Log("Satge : " + CurStage.gameObject.name);
    }
}
