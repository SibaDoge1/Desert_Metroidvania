using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{

    void Awake()
    {
        Atk = 1;
        preDelay = 1;
        postDelay = 1;
        inDelay = 1;
    }
    public override void Action() // 최종적으로 이걸로 공격행동 시작
    {
        Debug.Log('h');
    }

    void OnTriggerEnter(Collider2D col)
    {

    }

}
