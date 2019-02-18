using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy //이거 상속으로 보스 만들어주셈
{
    [SerializeField]
    public int myID { get; private set; }
    [SerializeField]
    private bool isKilled;

    protected override void Start()
    {
        base.Start();
        myID = Map.Instance.bosses.IndexOf(this);
        SaveManager.AddBossKillInfo(myID);
        isKilled = SaveManager.saveData.BossKillInfo[myID];
    }

    protected override IEnumerator Attack(float atk, float atkSpd)
    {
        throw new System.NotImplementedException();
    }

    protected override IEnumerator Patrol()
    {
        throw new System.NotImplementedException();
    }

    protected override IEnumerator Trace()
    {
        throw new System.NotImplementedException();
    }

    public void Killed() //죽을 때 불러주셈
    {
        SaveManager.SetBossKillInfo(myID, true);
    }
}
