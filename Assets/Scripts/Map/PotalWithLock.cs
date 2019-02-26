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
        else if (linkedKey != null && linkedKey.transform.parent == PlayManager.Instance.Player.equip.transform)
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
        isUnlocked = true;
        SaveManager.SetPotalLockInfo(myID, true);
    }
}
