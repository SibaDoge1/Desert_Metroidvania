using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_StoneGolem : Enemy
{
    protected override void Awake()
    {
        base.Awake();

        AttackInfo[] tempInfos = new AttackInfo[5];

        //상단 공격
        tempInfos[0].attackRange = new Vector2(4f, 8f);
        tempInfos[0].hitBoxPostion = new Vector2(2f, 3.5f);
        tempInfos[0].damage = 1;
        tempInfos[0].duration = 0.4f;
        tempInfos[0].preDelay = 0.8f;
        tempInfos[0].postDelay = 3f;

        tempInfos[0].monsterattackInfo.attackValue = 5;
        tempInfos[0].monsterattackInfo.attackIndex = 0;

        //하단 공격
        tempInfos[1].attackRange = new Vector2(6f, 3f);
        tempInfos[1].hitBoxPostion = new Vector2(3f, 1f);
        tempInfos[1].damage = 2;
        tempInfos[1].duration = 0.4f;
        tempInfos[1].preDelay = 0.8f;
        tempInfos[1].postDelay = 4f;

        tempInfos[1].monsterattackInfo.attackValue = 5;
        tempInfos[1].monsterattackInfo.attackIndex = 1;

        //초장거리 공격
        tempInfos[2].attackRange = new Vector2(2f, 2f);
        tempInfos[2].hitBoxPostion = new Vector2(1f, 3f);
        tempInfos[2].damage = 1;
        tempInfos[2].duration = 5f;
        tempInfos[2].preDelay = 1.5f;
        tempInfos[2].postDelay = 3f;

        tempInfos[2].monsterattackInfo.attackValue = 1;     //자기도 맞음 고쳐야함
        tempInfos[2].monsterattackInfo.attackIndex = 2;

        tempInfos[2].projectileInfo.projectileSpd = 8f;
        tempInfos[2].projectileInfo.attackSprite = null;
        tempInfos[2].projectileInfo.proType = ProjectileType.TOPLAYER;

        //중거리 공격 내려찍기
        tempInfos[3].attackRange = new Vector2(4f, 8f);
        tempInfos[3].hitBoxPostion = new Vector2(2f, 3.5f);
        tempInfos[3].damage = 1;
        tempInfos[3].duration = 0.3f;
        tempInfos[3].preDelay = 1.5f;
        tempInfos[3].postDelay = 6f;

        tempInfos[3].monsterattackInfo.attackValue = 1;    
        tempInfos[3].monsterattackInfo.attackIndex = 3;

        //전체 공격 내려찍기
        tempInfos[4].attackRange = new Vector2(4f, 8f);
        tempInfos[4].hitBoxPostion = new Vector2(2f, 3.5f);
        tempInfos[4].damage = 1;
        tempInfos[4].duration = 0.3f;
        tempInfos[4].preDelay = 2.5f;
        tempInfos[4].postDelay = 7f;

        tempInfos[4].monsterattackInfo.attackValue = 1;
        tempInfos[4].monsterattackInfo.attackIndex = 4;


        foreach (var tempInfo in tempInfos)
        {
            attackInfos.Add(tempInfo);
        }
    }

    protected override IEnumerator Patrol()
    {
        while (true)
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
        while (true)
        {
            Vector2 playerPos = PlayManager.Instance.Player.transform.position; //var는 웬만하면 안쓰는게 좋음
            float vectorToPlayer = playerPos.x - transform.position.x;

            direction = vectorToPlayer > 0 ? Direction.right : Direction.left;

            yield return new WaitForSeconds(0.5f);
        }
    }

    protected override IEnumerator Attack(float atk, float atkSpd, AttackInfo attackInfo)
    {
        Debug.Log("Start");

        patternChangable = false;
        AttackInfo tempInfo;

        Vector2 playerPos = PlayManager.Instance.Player.transform.position;
        float vectorToPlayer = playerPos.x - transform.position.x;
        direction = vectorToPlayer > 0 ? Direction.right : Direction.left;

        switch (attackInfo.monsterattackInfo.attackIndex)
        {
            case 0:
                tempInfo = attackInfos[0];
                tempInfo.damage += atk;
                tempInfo.duration *= atkSpd;
                tempInfo.preDelay *= atkSpd;
                tempInfo.postDelay *= atkSpd;

                yield return new WaitForSeconds(tempInfo.preDelay);

                CombatSystem.Instance.InstantiateHitBox(tempInfo, gameObject.transform);

                yield return new WaitForSeconds(tempInfo.postDelay + tempInfo.duration);
                break;
            case 1:
                tempInfo = attackInfos[1];
                tempInfo.damage += atk;
                tempInfo.duration *= atkSpd;
                tempInfo.preDelay *= atkSpd;
                tempInfo.postDelay *= atkSpd;
                
                yield return new WaitForSeconds(tempInfo.preDelay);

                CombatSystem.Instance.InstantiateHitBox(tempInfo, gameObject.transform);

                yield return new WaitForSeconds(tempInfo.postDelay + tempInfo.duration);
                break;
            case 2:
                tempInfo = attackInfos[2];
                tempInfo.damage += atk;
                tempInfo.duration *= atkSpd;
                tempInfo.preDelay *= atkSpd;
                tempInfo.postDelay *= atkSpd;

                yield return new WaitForSeconds(tempInfo.preDelay);

                CombatSystem.Instance.InstantiateProjectile(tempInfo, gameObject.transform);

                yield return new WaitForSeconds(tempInfo.postDelay);
                break;
        }

        patternChangable = true;

        yield return null;
    }
}
