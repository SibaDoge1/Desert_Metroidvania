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
    private Key linkedKey;

    void Start()
    {
        myID = Map.Instance.PotalWithLocks.IndexOf(this);
        SaveManager.AddPotalLockInfo(myID);
        isUnlocked = SaveManager.saveData.potalLockInfo[myID];
    }
    protected override void Action()
    {
        if (isUnlocked)
        {
            StartCoroutine("Fade");
            return;
        }
        if (linkedKey.transform.parent == EquipManager.Instance.transform)
        {
            Unlock();
        }
    }

    public void Unlock()
    {
        isUnlocked = true;
        SaveManager.SetPotalLockInfo(myID, true);
    }
}
