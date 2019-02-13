using System.Collections;
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

    public override void Action() // 최종적으로 이걸로 공격함
    {
        if (!onAttack)
            StartCoroutine(Action_Attack());
    }

    public override void DashAttack()
    {
        StartCoroutine(Action_DashAttack());
    }    

    IEnumerator Action_Attack()
    {
        onAttack = true;

        yield return new WaitForSeconds(attackInfos[0].preDelay);

        CombatSystem.Instance.InstantiateHitBox(attackInfos[0], gameObject.transform);

        yield return new WaitForSeconds(attackInfos[0].postDelay + attackInfos[0].duration);

        onAttack = false;
    }

    IEnumerator Action_DashAttack()
    {
        onAttack = true;

        yield return new WaitForSeconds(attackInfos[1].preDelay);

        CombatSystem.Instance.InstantiateHitBox(attackInfos[1], gameObject.transform);

        yield return new WaitForSeconds(attackInfos[1].postDelay + attackInfos[1].duration);

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
