using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{    
    protected override void Awake() //attackInfo
    {
        AttackInfo[] tempInfos = new AttackInfo[1];

        tempInfos[0].attackRange = new Vector2(1f, 1f);
        tempInfos[0].hitBoxPostion = new Vector2(1f, 0f);
        tempInfos[0].damage = 1;
        tempInfos[0].duration = 0.15f;

        foreach(var tempInfo in tempInfos)
        {
            attackInfos.Add(tempInfo);
        }
    }

    public override void Action() // 최종적으로 이걸로 공격함
    {
        if (!onAttack)
            StartCoroutine(Action_Attack());
    }
    
    IEnumerator Action_Attack() //임시로 만든 거
    {
        onAttack = true;

        CombatSystem.Instance.InstantiateHitBox(attackInfos[0], gameObject.transform);

        yield return new WaitForSeconds(0.25f);

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
