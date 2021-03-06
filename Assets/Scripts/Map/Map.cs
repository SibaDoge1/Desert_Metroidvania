﻿using System.Collections;
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
    public List<EventTrigger> EventTriggers;

    void Awake()
    {
        if (instance == null) instance = this;
        else
        {
            Debug.LogWarning("Singleton Error! : " + this.name);
            Destroy(gameObject);
        }
    }

    void Start()
    {
        CurStage = PlayManager.Instance.Player.transform.parent.parent.GetComponent<Stage>();
        ChangeBgm(CurStage.name);
        Debug.Log("start Stage : " + CurStage.gameObject.name);
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
        PlayManager.Instance.Player.IsMovable = false;
        FadeTool.Instance.FadeInOut(0.5f, 0.2f, ChangeStage, ()=>{PlayManager.Instance.Player.IsMovable = true;});
    }

    public void ChangeStage()
    {
        to.Active();
        PlayManager.Instance.Player.transform.position = new Vector3(toPotal.spawnPoint.x, toPotal.spawnPoint.y, PlayManager.Instance.Player.transform.position.z);
        PlayManager.Instance.Player.transform.SetParent(to.transform.Find("Objects"));
        CurStage = to;
        if (from != null && from != to)
            from.DeActive();
        ChangeBgm(to.name);
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
        ChangeBgm(toStr);
        Debug.Log("Stage : " + CurStage.gameObject.name);
    }

    public void ChangeBgm(string toStr)
    {
        switch (toStr)
        {
            case "Stage_A2": SoundDelegate.Instance.PlayBGM(BGM.A1A2); break;
            case "Stage_A3": SoundDelegate.Instance.PlayBGM(BGM.A1A2); break;
            case "Stage_B1": SoundDelegate.Instance.PlayBGM(BGM.B1B2); break;
            case "Stage_B2": SoundDelegate.Instance.PlayBGM(BGM.B1B2); break;
            case "Stage_C1": SoundDelegate.Instance.PlayBGM(BGM.C1C2); break;
            case "Stage_C2": SoundDelegate.Instance.PlayBGM(BGM.C1C2); break;
            case "Stage_C3":
                SoundDelegate.Instance.PlayBGM(BGM.C3);
                if (SaveManager.GetIsClear() == true)
                    SoundDelegate.Instance.StopBGM(); break;
        }
    }

    public bool CheckOutSide(Vector2 pos)
    {
        return CurStage.CheckOutSide(pos);
    }

    public void ResetCurrentStage()
    {
        CurStage.ResetStage();
    }
}
