using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_mummy: RespawnableEnemy
{
    protected override void Awake()
    {
        base.Awake();

        AttackInfo[] tempInfos = new AttackInfo[1];

        tempInfos[0].attackRange = new Vector2(1f, 1f);
        tempInfos[0].hitBoxPostion = new Vector2(1f, 0f);
        tempInfos[0].damage = 1;
        tempInfos[0].duration = 0.2f;
        tempInfos[0].preDelay = 1f;
        tempInfos[0].postDelay = 0.8f;

        tempInfos[0].monsterattackInfo.attackValue = 1;
        tempInfos[0].monsterattackInfo.attackIndex = 0;

        foreach (var tempInfo in tempInfos)
        {
            attackInfos.Add(tempInfo);
        }
    }

    protected override IEnumerator Patrol()
    {
        while(true)
        {
            yield return new WaitForSeconds(5f);
            direction = Direction.zero;
            yield return new WaitForSeconds(1f);
            direction = Direction.left;
            yield return new WaitForSeconds(5f);
            direction = Direction.zero;
            yield return new WaitForSeconds(1f);
            direction = Direction.right;
        }
    }

    protected override IEnumerator Trace()
    {
        while(true)
        {
            Vector2 playerPos = PlayManager.Instance.Player.transform.position; //var는 웬만하면 안쓰는게 좋음
            float vectorToPlayer = playerPos.x - transform.position.x;

            direction = vectorToPlayer > 0 ? Direction.right : Direction.left;

            yield return new WaitForSeconds(0.5f);
        }
    }

    protected override IEnumerator Attack(float atk, float atkSpd, AttackInfo attackInfo)
    {
        Debug.Log("Scorpione_attack");

        anim.Play("attack");

        patternChangable = false;

        switch (attackInfo.monsterattackInfo.attackIndex)
        {
            case 0:
                AttackInfo tempInfo = attackInfos[0];
                tempInfo.damage += atk;
                tempInfo.duration *= atkSpd;
                tempInfo.preDelay *= atkSpd;
                tempInfo.postDelay *= atkSpd;
                Vector2 playerPos = PlayManager.Instance.Player.transform.position;
                float vectorToPlayer = playerPos.x - transform.position.x;

                direction = vectorToPlayer > 0 ? Direction.right : Direction.left;
                Move(direction);
                yield return new WaitForSeconds(tempInfo.preDelay);
                SoundDelegate.Instance.PlayEffectSound(EffectSoundType.MummyAttack);

                CombatSystem.Instance.InstantiateHitBox(tempInfo, gameObject.transform);

                yield return new WaitForSeconds(tempInfo.postDelay + tempInfo.duration);
                break;
        }

        patternChangable = true;

        yield return null;
    }

    protected override void OnDieCallBack()
    {
        if(enemyState != EnemyState.DEAD)
        {
            StopAllCoroutines();
            StartCoroutine("EnemyDead");
        }
    }

    IEnumerator EnemyDead()
    {
        anim.Play("dead");
        SoundDelegate.Instance.PlayEffectSound(EffectSoundType.MummyDie);
        enemyState = EnemyState.DEAD;
        rigid.velocity = Vector2.zero;
        gameObject.layer = 9;
        Destroy(transform.Find("Collider").gameObject);
        Destroy(transform.Find("SearchingCollider").gameObject);
        Destroy(transform.Find("AttackRangeCollider").gameObject);


        yield return new WaitForSeconds(1f);

        Destroy(gameObject);

    }

    private void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.tag == "Player")
        {
            c.gameObject.GetComponent<Character>().GetDamage(attackInfos[0].damage, transform);
        }
    }
}
