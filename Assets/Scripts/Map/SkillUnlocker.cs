using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillUnlocker : InteractObject
{
    public int ID_Skill;
    public string noticeStr;
    // Start is called before the first frame update
    void Start()
    {
    }
    public void Unlock()
    {
        NoticeUI.Instance.MakeNotice(noticeStr, 3f);
        SaveManager.SetSkillUnlockInfo(ID_Skill, true);
        Destroy(gameObject);
    }

    protected override void Action()
    {
        Unlock();
    }
}
