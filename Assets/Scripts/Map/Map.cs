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

    public Stage CurStage { get; set; }
    public List<Boss> bosses;
    public List<Stage> stages;
    public List<PotalWithLock> PotalWithLocks;

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
        CurStage = PlayManager.Instance.Player.transform.parent.parent.GetComponent<Stage>();
        Debug.Log("Stage : " + CurStage.gameObject.name);
    }

    public void SetCurStage(string name)
    {
        CurStage = GameObject.Find(name).GetComponent<Stage>();
    }

    public void changeStage(Stage from, Stage to)
    {
        CurStage = to;
        Debug.Log("Satge : " + CurStage.gameObject.name);
    }
}
