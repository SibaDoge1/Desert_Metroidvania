using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotalWithLock : Potal
{
    [SerializeField]
    public int myID { get; private set; }
    [SerializeField]
    private bool isUnlocked;
    [SerializeField]
    public Key linkedKey;

    void Start()
    {
        myID = Map.Instance.PotalWithLocks.IndexOf(this);
        isUnlocked = SaveManager.GetPotalLockInfo(myID);
    }
    protected override void Action()
    {
        if (isUnlocked)
        {
            ChangeStage();
            return;
        }
        if (linkedKey.transform.parent == EquipManager.Instance.transform)
        {
            Unlock();
        }
    }

    public void Unlock()
    {
        if(SaveManager.GetSkillUnlockInfo(myID) == true)
        {
            return;
        }
        NoticeUI.Instance.MakeNotice("잠금을 풀었습니다!", 3f);
        isUnlocked = true;
        SaveManager.SetPotalLockInfo(myID, true);
    }
}
