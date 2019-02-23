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
        NoticeUI.Instance.MakeNotice(ID_Skill + "번 스킬을 획득했습니다", 3f);
        SaveManager.SetSkillUnlockInfo(ID_Skill, true);
        Destroy(gameObject);
    }

    protected override void Action()
    {
        Unlock();
    }
}
