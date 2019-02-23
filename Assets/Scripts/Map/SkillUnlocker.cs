using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillUnlocker : InteractObject
{
    public int ID_Skill;
    // Start is called before the first frame update
    void Start()
    {
    }
    public void Unlock()
    {
        SaveManager.SetSkillUnlockInfo(ID_Skill, true);
    }

    protected override void Action()
    {
        Unlock();
    }
}
