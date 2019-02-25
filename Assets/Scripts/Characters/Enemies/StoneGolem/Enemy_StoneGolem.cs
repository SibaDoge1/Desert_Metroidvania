using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_StoneGolem : Boss
{
    GameObject meteorLine;
    GameObject meteor;

    protected override void Awake()
    {
        base.Awake();

        meteor = Resources.Load("Prefabs/Monster/StoneGolem/StoneGolem_Meteor") as GameObject;
        meteorLine = Resources.Load("Prefabs/Monster/StoneGolem/StoneGolem_Line") as GameObject;

        AttackInfo[] tempInfos = new AttackInfo[6];

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
        tempInfos[2].cooltime = 12f;            //임시 적용

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
        tempInfos[3].cooltime = 18f;

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

        //중거리 공격 충격파
        tempInfos[5].attackRange = new Vector2(2f, 6f);
        tempInfos[5].hitBoxPostion = new Vector2(2f, 2.5f);
        tempInfos[5].damage = 1;
        tempInfos[5].duration = 1.5f;
        tempInfos[5].preDelay = 1.5f;
        tempInfos[5].postDelay = 0f;

        tempInfos[5].projectileInfo.projectileSpd = 3f;
        tempInfos[5].projectileInfo.attackSprite = null;
        tempInfos[5].projectileInfo.proType = ProjectileType.DIRECTION;

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
            case 3:
                tempInfo = attackInfos[3];
                tempInfo.damage += atk;
                tempInfo.duration *= atkSpd;
                tempInfo.preDelay *= atkSpd;
                tempInfo.postDelay *= atkSpd;

                yield return new WaitForSeconds(tempInfo.preDelay);

                CombatSystem.Instance.InstantiateHitBox(tempInfo, gameObject.transform);
                StartCoroutine(ShootShockWave());

                yield return new WaitForSeconds(tempInfo.postDelay + tempInfo.duration);
                break;
            case 4:
                tempInfo = attackInfos[4];
                tempInfo.damage += atk;
                tempInfo.duration *= atkSpd;
                tempInfo.preDelay *= atkSpd;
                tempInfo.postDelay *= atkSpd;

                yield return new WaitForSeconds(tempInfo.preDelay);

                for (int i = 0; i < 6; i++)
                {
                    CombatSystem.Instance.InstantiateHitBox(tempInfo, gameObject.transform);
                    direction = direction == Direction.right ? Direction.left : Direction.right;
                    StartCoroutine(Attack_Meteor());
                    yield return new WaitForSeconds(tempInfo.duration);
                }

                yield return new WaitForSeconds(tempInfo.postDelay);
                break;
        }

        patternChangable = true;

        yield return null;
    }

    IEnumerator ShootShockWave()
    {
        AttackInfo tempInfo = attackInfos[5];
        Direction dir = direction;
        Vector3 pos = transform.position;

        for (int i = 0; i < 6; i++)
        {
            InstantiateShockWave(tempInfo, dir, pos);

            yield return new WaitForSeconds(tempInfo.preDelay);
        }
    }

    IEnumerator Attack_Meteor()
    {
        Vector2 meteorVec;
        meteorVec.x = Random.Range(transform.position.x + 20f, transform.position.x - 20f);
        meteorVec.y = 5f;
        GameObject tempLine = Instantiate(meteorLine, meteorVec, Quaternion.identity);

        Vector2 meteorVec2;
        meteorVec2.x = Random.Range(transform.position.x + 20f, transform.position.x - 20f);
        meteorVec2.y = 5f;
        GameObject tempLine2 = Instantiate(meteorLine, meteorVec2, Quaternion.identity);

        yield return new WaitForSeconds(0.5f);

        Destroy(tempLine);
        Destroy(tempLine2);
        GameObject tempMeteor = Instantiate(meteor, meteorVec, Quaternion.identity);
        tempMeteor.GetComponent<DamagingCollider>().parentTransform = transform;
        GameObject tempMeteor2 = Instantiate(meteor, meteorVec2, Quaternion.identity);
        tempMeteor2.GetComponent<DamagingCollider>().parentTransform = transform;
    }

    private void InstantiateShockWave(AttackInfo attackInfo, Direction dir, Vector3 pos)
    {   //눼 맞워오 누더기골램이애오
        GameObject _projectile = Resources.Load("Prefabs/Colliders/ProjectileObject") as GameObject;

        BoxCollider2D damagingCollider_Collider2D = Instantiate(_projectile, pos, Quaternion.identity).GetComponent<BoxCollider2D>();
        Projectile damagingCollider = damagingCollider_Collider2D.gameObject.GetComponent<Projectile>();

        damagingCollider_Collider2D.size = attackInfo.attackRange;
        Vector3 tempV3 = damagingCollider_Collider2D.transform.position;
        tempV3.x += attackInfo.hitBoxPostion.x * (int)dir;
        tempV3.y += attackInfo.hitBoxPostion.y;
        damagingCollider_Collider2D.transform.position = tempV3;

        damagingCollider.damage = attackInfo.damage;

        switch (attackInfo.projectileInfo.proType)
        {
            case ProjectileType.DIRECTION:      //바라보는 방향
                damagingCollider.ShootProjectile_Direction(attackInfo.projectileInfo.projectileSpd, dir);
                break;
        }

        if (PlayManager.Instance.isTestMode)
        {
            damagingCollider.ChangeSprite(attackInfo.attackRange);
        }

        if (attackInfo.projectileInfo.attackSprite != null)
        {
            damagingCollider.ChangeProjectileSprite(attackInfo.projectileInfo.attackSprite);
        }

        damagingCollider.parentTransform = transform;
        damagingCollider.DestroyCollider(attackInfo.duration);
    }
}
