using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : RespawnableEnemy
{
    public Sprite bullet;

    protected override void Awake()
    {
        base.Awake();

        AttackInfo[] tempInfos = new AttackInfo[1];

        tempInfos[0].attackRange = new Vector2(1f, 0.5f);
        tempInfos[0].hitBoxPostion = new Vector2(1f, 0f);
        tempInfos[0].damage = 1;
        tempInfos[0].duration = 0.15f;
        tempInfos[0].preDelay = 0.1f;
        tempInfos[0].postDelay = 0.1f;

        tempInfos[0].projectileInfo.attackSprite = bullet;
        tempInfos[0].projectileInfo.projectileSpd = 5f;
        tempInfos[0].projectileInfo.proType = ProjectileType.TOPLAYER;

        foreach (var tempInfo in tempInfos)
        {
            attackInfos.Add(tempInfo);
        }
    }

    protected override IEnumerator Patrol()
    {
        while (true)
        {
            direction = Direction.zero;
            yield return new WaitForSeconds(1f);

            direction = direction == Direction.left ? Direction.right : Direction.left;
            yield return new WaitForSeconds(5f);
        }
    }

    protected override IEnumerator Trace()
    {
        while (true)
        {
            direction = Direction.zero;
            yield return new WaitForSeconds(0.5f);
        }
    }

    protected override IEnumerator Attack(float atk, float atkSpd, AttackInfo attackInfo)
    {
        while (true)
        {
            patternChangable = false;

            AttackInfo tempInfo = attackInfos[0];
            tempInfo.damage += atk;
            tempInfo.duration *= atkSpd;
            tempInfo.preDelay *= atkSpd;
            tempInfo.postDelay *= atkSpd;
            var playerPos = PlayManager.Instance.Player.transform.position;
            var vectorToPlayer = playerPos - transform.position;

            direction = vectorToPlayer.x >= 0 ? Direction.right : Direction.left;
            yield return new WaitForSeconds(tempInfo.preDelay);

            CombatSystem.Instance.InstantiateProjectile(tempInfo, gameObject.transform);

            yield return new WaitForSeconds(tempInfo.postDelay + tempInfo.duration);

            patternChangable = true;

            yield return null;

        }
    }

    private void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.tag == "Player")
        {
            c.gameObject.GetComponent<Character>().GetDamage(attackInfos[0].damage, transform);
        }
    }
}
