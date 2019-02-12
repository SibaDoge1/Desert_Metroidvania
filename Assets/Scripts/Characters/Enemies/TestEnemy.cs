﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : Enemy
{
    protected override void Awake()
    {
        Hp = 3;
    }

    protected override void Action() //몬스터 공격 구현
    {

    }

    protected override void Think() //여기서 AI 구현? -> 맘대로 하셈
    {

    }

    protected override void OnDieCallBack() //죽을 때 부르는 함수
    {
        Destroy(gameObject);
    }

    public override void ResetEnemy()
    {
        StopAllCoroutines();

        Destroy(gameObject);
    }

}
