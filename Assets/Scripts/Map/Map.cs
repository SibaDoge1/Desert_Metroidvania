using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private Stage from;
    private Stage to;
    private Potal toPotal;
    public void ChangeStageRoutine(Stage _from, Stage _to, Potal _toPotal)
    {
        from = _from;
        to = _to;
        toPotal = _toPotal;
        FadeTool.Instance.FadeInOut(0.5f, ChangeStage);
    }

    public void ChangeStage()
    {
        to.Active();
        PlayManager.Instance.Player.transform.position = new Vector3(toPotal.transform.position.x, toPotal.transform.position.y, PlayManager.Instance.Player.transform.position.z);
        PlayManager.Instance.Player.transform.SetParent(to.transform.Find("Objects"));
        CurStage = to;
        if (from != null && from != to)
            from.DeActive();
        Debug.Log("Stage : " + CurStage.gameObject.name);
    }

    public void ChangeStage(Stage _from, string toStr, Vector2 pos)
    {
        Stage _to = GameObject.Find("Map").transform.Find(toStr).GetComponent<Stage>();
        _to.Active();
        PlayManager.Instance.Player.transform.position = new Vector3(pos.x, pos.y, PlayManager.Instance.Player.transform.position.z);
        PlayManager.Instance.Player.transform.SetParent(_to.transform.Find("Objects"));
        CurStage = _to;
        if(_from != null && _from != _to)
            _from.DeActive();
        Debug.Log("Stage : " + CurStage.gameObject.name);
    }
}
