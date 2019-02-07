using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ChargingMode
{
    public float chargingTime;
    public float duration;
    private float postDelay;
    public int attackFrequency;
    void isShotOnCenter(GameObject character){
        //방어력 10 감소 character.defence -= 10;
        //기획서 안채워짐 : 더욱 강력한 데미
    }
}

public class Gauntlet : Weapon
{


    ChargingMode[] charging = new ChargingMode[4];

    charging[0].chargingTime = 0.0f;
    charging[0].duration = 0.1f;
    charging[0].postDelay = 0.1f;
    charging[0].attackFrequency = 1;

    charging[1].chargingTime = 0.5f;
    charging[1].duration = 0.1f;
    charging[1].postDelay = 0.3f;
    charging[1].attackFrequency = 1;

    charging[2].chargingTime = 1.5f;
    charging[2].duration = 0.1f;
    charging[2].postDelay = 0.3f;
    charging[2].attackFrequency = 2;

    charging[3].chargingTime = 3f;
    charging[3].duration = 0.1f;
    charging[3].postDelay = 0.5f;
    charging[3].attackFrequency = 2;
    //명중시



    protected override void Awake() //attackInfo
    {
        AttackInfo[] tempInfos = new AttackInfo[3];


        tempInfos[2].chargingTime = new Vector2(1f, 1f);
        tempInfos[2].hitBoxPostion = new Vector2(1f, 0f);
        tempInfos[2].damage = 1;
        tempInfos[2].duration = 0.15f;


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
