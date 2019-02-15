﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{    
    protected override void Awake() //attackInfo
    {
        AttackInfo[] tempInfos = new AttackInfo[2];

        tempInfos[0].attackRange = new Vector2(1f, 1f);
        tempInfos[0].hitBoxPostion = new Vector2(1f, 0f);
        tempInfos[0].damage = 1;
        tempInfos[0].duration = 0.15f;
        tempInfos[0].preDelay = 0f;
        tempInfos[0].postDelay = 0.1f;

        tempInfos[1].attackRange = new Vector2(5f, 1f);
        tempInfos[1].hitBoxPostion = new Vector2(-2.5f, 0f);
        tempInfos[1].damage = 1;
        tempInfos[1].duration = 0.15f;
        tempInfos[1].preDelay = 0f;
        tempInfos[1].postDelay = 0.1f;

        foreach (var tempInfo in tempInfos)
        {
            attackInfos.Add(tempInfo);
        }
    }

    public override void Action(float atk, float atkSpd) // 최종적으로 이걸로 공격함
    {
        if (!onAttack)
        {
            AttackInfo tempInfo = attackInfos[0];
            tempInfo.damage += atk;
            tempInfo.duration *= atkSpd;
            tempInfo.preDelay *= atkSpd;
            tempInfo.postDelay *= atkSpd;
            StartCoroutine(Action_Attack(tempInfo));
        }
    }

    public override void DashAttack(float atk, float atkSpd)
    {
        AttackInfo tempInfo = attackInfos[1];
        tempInfo.damage += atk;
        tempInfo.duration *= atkSpd;
        tempInfo.preDelay *= atkSpd;
        tempInfo.postDelay *= atkSpd;
        StartCoroutine(Action_DashAttack(tempInfo));
    }    

    IEnumerator Action_Attack(AttackInfo info)
    {
        onAttack = true;

        yield return new WaitForSeconds(info.preDelay);

        CombatSystem.Instance.InstantiateHitBox(info, gameObject.transform);

        yield return new WaitForSeconds(info.postDelay + info.duration);

        onAttack = false;
    }

    IEnumerator Action_DashAttack(AttackInfo info)
    {
        onAttack = true;

        yield return new WaitForSeconds(info.preDelay);

        CombatSystem.Instance.InstantiateHitBox(info, gameObject.transform);

        yield return new WaitForSeconds(info.postDelay + info.duration);

        onAttack = false;
    }
    /*
    void OnTriggerStay2D(Collider2D col)   //임시로 만든 거
    {

        if (onAttack == true && col.transform.parent.tag == "Enemy" && col.transform.parent.GetComponent<Character>().IsSuper <= 0f)
        {
            Debug.Log("attack!");

            col.transform.parent.GetComponent<Character>().GetDamage(atk);

        }
    }
    */
}