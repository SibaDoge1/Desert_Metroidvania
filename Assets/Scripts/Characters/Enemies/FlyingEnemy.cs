using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : RespawnableEnemy
{
    float damage;

    protected override void Awake()
    {
        base.Awake();

        damage = 1f;

        rigid.gravityScale = 0f;
        enemyType = EnemyType.FLY;
    }

    protected override IEnumerator Patrol()
    {
        while (true)
        {
            dir_Flying = Vector2.zero;
            yield return new WaitForSeconds(1f);

            dir_Flying = new Vector2(Random.Range(-10f, 10f), Random.Range(-10f, 10f));
            dir_Flying.Normalize();
            yield return new WaitForSeconds(5f);
        }
    }

    protected override IEnumerator Trace()
    {
        while (true)
        {
            var playerPos = PlayManager.Instance.Player.transform.position;
            var vectorToPlayer = playerPos - transform.position;
            vectorToPlayer.Normalize();

            dir_Flying = vectorToPlayer;

            yield return new WaitForSeconds(0.5f);
        }
    }

    bool isAttacking = false;

    public override void OnTriggerEnterAttack()
    {
        base.OnTriggerEnterAttack();
    }

    public override void OnTriggerExitAttack()
    {
        base.OnTriggerExitAttack();
    }

    protected override IEnumerator Attack(float atk, float atkSpd, AttackInfo attackInfo)
    {
        while (true)
        {
            isAttacking = true;
            patternChangable = false;

            yield return new WaitForSeconds(0.5f);

            float timer = 0f;

            var playerPos = PlayManager.Instance.Player.transform.position;
            var vectorToPlayer = playerPos - transform.position;
            vectorToPlayer.Normalize();

            dir_Flying = vectorToPlayer;

            while (true)
            {
                if (timer > 2f)
                {
                    break;
                }

                Move(dir_Flying);

                timer += Time.deltaTime;
                yield return null;
            }

            patternChangable = true;
            isAttacking = false;

            yield return null;
        }
    }

    private void OnCollisionEnter2D(Collision2D c)
    {
        if(enemyState != EnemyState.DEAD)
        {
            if (c.gameObject.tag == "Player")
            {
                c.gameObject.GetComponent<Character>().GetDamage(damage, transform);
            }
        }
    }

    protected override void OnDieCallBack()
    {
        if (enemyState != EnemyState.DEAD)
        {
            StopAllCoroutines();
            StartCoroutine("flyingEnemyDead");
        }
    }

    IEnumerator flyingEnemyDead()
    {
        anim.Play("dead");
        enemyState = EnemyState.DEAD;
        rigid.velocity = Vector2.zero;
        rigid.gravityScale = 1;
        gameObject.layer = 9;

        Destroy(transform.Find("Collider").gameObject);
        Destroy(transform.Find("SearchingCollider").gameObject);
        Destroy(transform.Find("AttackRangeCollider").gameObject);

        yield return new WaitForSeconds(1f);

        Destroy(gameObject);

    }

    protected override void CheckBuffAndDebuff()
    {
        base.CheckBuffAndDebuff();

        if (isAttacking)
            spd *= 3f;
    }
}
