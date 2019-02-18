using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : Enemy
{
    protected override void Awake()
    {
        base.Awake();

        rigid.gravityScale = 0f;
        enemyType = EnemyType.FLY;
    }

    protected override IEnumerator Patrol()
    {
        while (true)
        {
            dir_Flying = Vector2.zero;
            yield return new WaitForSeconds(1f);

            dir_Flying = new Vector2(Random.value, Random.value);
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

    protected override IEnumerator Attack(float atk, float atkSpd)
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

    private void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.tag == "Player")
        {
            c.gameObject.GetComponent<Character>().GetDamage(attackInfos[0].damage);
        }
    }
}
