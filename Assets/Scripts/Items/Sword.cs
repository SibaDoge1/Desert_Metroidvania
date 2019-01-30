using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    void Awake()
    {

    }
    public override void Action() // 최종적으로 이걸로 공격함
    {

        StartCoroutine(Action_Attack());
    }

    IEnumerator Action_Attack() //임시로 만든 거
    {
        float timer = 0f;

        onAttack = true;

        while (true)
        {
            if (timer >= 0.5f)
            {
                break;
            }

            transform.Translate(new Vector2(1f * Time.deltaTime, 0f));

            timer += Time.deltaTime;

            yield return null;
        }

        transform.Translate(new Vector2(-0.5f, 0f));
        Debug.Log(onAttack.ToString());
        onAttack = false;
    }

    void OnTriggerStay2D(Collider2D col)   //임시로 만든 거
    {

        if (onAttack == true && col.transform.parent.tag == "Enemy" && col.transform.parent.GetComponent<Character>().IsSuper <= 0f)
        {
            Debug.Log("attack!");

            col.transform.parent.GetComponent<Character>().GetDamage(atk);

        }
    }

}
