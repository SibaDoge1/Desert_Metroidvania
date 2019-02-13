using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : Enemy
{
    protected override void Awake()
    {
        base.Awake();

        AttackInfo[] tempInfos = new AttackInfo[1];

        tempInfos[0].attackRange = new Vector2(1f, 1f);
        tempInfos[0].hitBoxPostion = new Vector2(1f, 0f);
        tempInfos[0].damage = 1;
        tempInfos[0].duration = 0.15f;
        tempInfos[0].preDelay = 0.1f;
        tempInfos[0].postDelay = 0.1f;

        foreach (var tempInfo in tempInfos)
        {
            attackInfos.Add(tempInfo);
        }
    }

    protected override IEnumerator Patrol()
    {
        while(true)
        {
            direction = Direction.zero;
            yield return new WaitForSeconds(1f);

            direction = direction == Direction.left ? Direction.right : Direction.left;
            yield return new WaitForSeconds(5f);
        }
    }

    protected override IEnumerator Trace()
    {
        while(true)
        {
            var playerPos = PlayManager.Instance.Player.transform.position;
            var vectorToPlayer = playerPos - transform.position;

            direction = vectorToPlayer.x >= 0 ? Direction.right : Direction.left;

            yield return new WaitForSeconds(0.5f);
        }
    }

    protected override IEnumerator Attack()
    {
        while(true)
        {
            var playerPos = PlayManager.Instance.Player.transform.position;
            var vectorToPlayer = playerPos - transform.position;

            direction = vectorToPlayer.x >= 0 ? Direction.right : Direction.left;

            yield return new WaitForSeconds(attackInfos[0].preDelay);

            CombatSystem.Instance.InstantiateHitBox(attackInfos[0], gameObject.transform);

            yield return new WaitForSeconds(attackInfos[0].postDelay + attackInfos[0].duration);
        }
    }

    private void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.tag == "Player")
        {
            c.gameObject.GetComponent<Character>().GetDamage(attackInfos[0].damage);
        }
    }
}
