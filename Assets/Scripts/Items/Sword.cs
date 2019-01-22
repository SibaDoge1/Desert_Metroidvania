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
        Debug.Log("attack!");
    }

    void OnTriggerEnter2D(Collider2D col)
    {

    }

}
