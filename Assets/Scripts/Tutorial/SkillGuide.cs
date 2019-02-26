using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Skill
{
    Dash,
    JumpSkill,
    DashSkill,
    Attack3,
    Attack2
}
public class SkillGuide : MonoBehaviour
{
    public Skill mySkill;
    void Start()
    {
        PlayManager.Instance.AddSkillFunc(mySkill, Trigger);
    }

    void Trigger()
    {
        Vector3 targetScreenPos = Camera.main.WorldToViewportPoint(transform.position);
        if (targetScreenPos.x >= 0f && targetScreenPos.x <= 1f && targetScreenPos.y >= 0f && targetScreenPos.y <= 1f)
        {
            PlayManager.Instance.DeleteSkillFunc(mySkill, Trigger);
            gameObject.SetActive(false);
        }
    }
}
