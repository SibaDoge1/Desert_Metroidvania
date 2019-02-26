using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Weapon
{

    float counterBurf;  //  카운터 버프 및 카운터 모션 시간
    float unbeatableTime;

    protected override void Awake() //attackInfo
    {
        AttackInfo[] tempInfos = new AttackInfo[2];

        tempInfos[1].defenceRange = new Vector2(1f, 1f);   //임시값
        tempInfos[1].defenceBoxPostion = new Vector2(1f, 0f);   //임시값
        tempInfos[1].duration = 0.2f;  //방패로 공격하는 시간
        counterBurf = 0.5f;
        unbeatableTime = 0.25f;
            

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

    IEnumerator Action_Attack() //임시로 만든 거
    {
        onAttack = true;

        CombatSystem.Instance.InstantiateHitBox(attackInfos[1], gameObject.transform);

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
