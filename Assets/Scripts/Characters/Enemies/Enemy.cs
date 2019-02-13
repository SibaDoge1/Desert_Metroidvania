using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    DEFAULT ,ATTACK, TRACE, PATROL
}

public abstract class Enemy : Character
{
    #region Status
    [Header("AttackStatus")]
    protected List<AttackInfo> attackInfos = new List<AttackInfo>();
    #endregion

    protected Coroutine patrol;
    protected Coroutine trace;
    protected Coroutine attack;

    protected EnemyState enemyState;        //적 상태 변경 시, 이걸 바꾸면 됨
    protected EnemyState enemyCurState;     //상태가 변경됐는지 확인하기 위한 Enum형 필드

    protected override void Awake()
    {
        base.Awake();

        enemyState = EnemyState.PATROL;
    }

    protected override void Update()
    {
        base.Update();

        CheckAI();
    }

    public virtual void ResetEnemy()
    {
        StopAllCoroutines();

        Destroy(gameObject);
    }

    protected override void OnDieCallBack() //죽을 때 부르는 함수
    {
        StopAllCoroutines();

        Destroy(gameObject);
    }

    public virtual void OnTriggerEnterSearch()
    {
        enemyState = EnemyState.TRACE;
    }
    public virtual void OnTriggerExitSearch()
    {
        enemyState = EnemyState.PATROL;
    }

    public virtual void OnTriggerEnterAttack()
    {
        enemyState = EnemyState.ATTACK;
    }
    public virtual void OnTriggerExitAttack()
    {
        enemyState = EnemyState.TRACE;
    }

    public virtual void CheckAI()
    {
        switch (enemyState)
        {
            case EnemyState.ATTACK:
                if (enemyCurState != EnemyState.ATTACK)
                {
                    attack = StartCoroutine(Attack());
                }
                break;

            case EnemyState.TRACE:
                if (enemyCurState != EnemyState.TRACE)
                {
                    trace = StartCoroutine(Trace());
                }
                break;

            case EnemyState.PATROL:
                if (enemyCurState != EnemyState.PATROL)
                {
                    patrol = StartCoroutine(Patrol());
                }
                break;
        }

        if (enemyCurState != enemyState)
        {
            switch (enemyCurState)
            {
                case EnemyState.ATTACK:
                    StopCoroutine(attack);
                    break;

                case EnemyState.TRACE:
                    StopCoroutine(trace);
                    break;

                case EnemyState.PATROL:
                    StopCoroutine(patrol);
                    break;

                case EnemyState.DEFAULT:
                    break;

                default:
                    break;
            }
            enemyCurState = enemyState;
        }

        if (enemyCurState != EnemyState.ATTACK)
            Move(direction);

    }

    protected abstract IEnumerator Patrol();
    protected abstract IEnumerator Trace();
    protected abstract IEnumerator Attack();

}
