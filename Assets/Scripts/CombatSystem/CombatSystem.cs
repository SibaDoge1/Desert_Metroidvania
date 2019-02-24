using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ProjectileInfo
{
    public Sprite attackSprite;     //어택 스프라이트
    public float projectileSpd;     //투사체 속도

    public ProjectileType proType;
}

public struct MonsterAttackInfo
{
    public int attackValue;       //가중치
    public int attackIndex;
}

public struct AttackInfo
{
    public Vector2 attackRange;     //공격 범위
    public Vector2 hitBoxPostion;   //공격자 기준(오른쪽) 공격 판정 생성 지점
    public float damage;            //데미지
    public float duration;          //지속 시간
    public float preDelay;          //선딜레이
    public float postDelay;         //후딜레이
    public string attackID; // 공격 종류
    public ProjectileInfo projectileInfo;
    public MonsterAttackInfo monsterattackInfo;
}

public class CombatSystem : MonoBehaviour
{
    private static CombatSystem instance = null;
    public static CombatSystem Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this)
        {
            Debug.LogWarning("Singleton Error! : " + this.name);
            Destroy(gameObject);
        }
        _damagingCollider = Resources.Load("Prefabs/Colliders/DamagingCollider") as GameObject;
        _projectile = Resources.Load("Prefabs/Colliders/ProjectileObject") as GameObject;
    }

    GameObject _damagingCollider;
    GameObject _projectile;

    public GameObject InstantiateHitBox(AttackInfo attackInfo, Transform transform_attacker)
        //attacker의 transform을 기준으로 DamagingCollider 생성
    {
        BoxCollider2D damagingCollider_Collider2D = Instantiate(_damagingCollider, transform_attacker.position, Quaternion.identity).GetComponent<BoxCollider2D>();
        DamagingCollider damagingCollider = damagingCollider_Collider2D.gameObject.GetComponent<DamagingCollider>();

        damagingCollider_Collider2D.size = attackInfo.attackRange;
        Vector3 tempV3 = damagingCollider_Collider2D.transform.position;
        tempV3.x += attackInfo.hitBoxPostion.x * (int)transform_attacker.GetComponentInParent<Character>().Direction;
        tempV3.y += attackInfo.hitBoxPostion.y;
        damagingCollider_Collider2D.transform.position = tempV3;

        damagingCollider.damage = attackInfo.damage;

        if (PlayManager.Instance.isTestMode)
        {
            damagingCollider.ChangeSprite(attackInfo.attackRange);
        }

        damagingCollider.gameObject.transform.SetParent(transform_attacker);
        damagingCollider.DestroyCollider(attackInfo.duration);

        return damagingCollider.gameObject;
    }

    public GameObject InstantiateHitBox(AttackInfo attackInfo, Transform transform_attacker, string path)
    //path의 DamagingCollider 상속 오브젝트를 생성
    {
        GameObject dCol = Resources.Load(path) as GameObject;

        BoxCollider2D damagingCollider_Collider2D = Instantiate(dCol, transform_attacker.position, Quaternion.identity).GetComponent<BoxCollider2D>();
        DamagingCollider damagingCollider = damagingCollider_Collider2D.gameObject.GetComponent<DamagingCollider>();

        damagingCollider_Collider2D.size = attackInfo.attackRange;
        Vector3 tempV3 = damagingCollider_Collider2D.transform.position;
        tempV3.x += attackInfo.hitBoxPostion.x * (int)transform_attacker.GetComponentInParent<Character>().Direction;
        tempV3.y += attackInfo.hitBoxPostion.y;
        damagingCollider_Collider2D.transform.position = tempV3;

        damagingCollider.damage = attackInfo.damage;

        if (PlayManager.Instance.isTestMode)
        {
            damagingCollider.ChangeSprite(attackInfo.attackRange);
        }

        damagingCollider.gameObject.transform.SetParent(transform_attacker);
        damagingCollider.DestroyCollider(attackInfo.duration);

        return damagingCollider.gameObject;
    }

    public void InstantiateProjectile(AttackInfo attackInfo, Transform transform_attacker)
    {   //투사체 생성
        BoxCollider2D damagingCollider_Collider2D = Instantiate(_projectile, transform_attacker.position, Quaternion.identity).GetComponent<BoxCollider2D>();
        Projectile damagingCollider = damagingCollider_Collider2D.gameObject.GetComponent<Projectile>();

        damagingCollider_Collider2D.size = attackInfo.attackRange;
        Vector3 tempV3 = damagingCollider_Collider2D.transform.position;
        tempV3.x += attackInfo.hitBoxPostion.x * (int)transform_attacker.GetComponentInParent<Character>().Direction;
        tempV3.y += attackInfo.hitBoxPostion.y;
        damagingCollider_Collider2D.transform.position = tempV3;

        damagingCollider.damage = attackInfo.damage;

        switch (attackInfo.projectileInfo.proType)
        {
            case ProjectileType.DIRECTION:      //바라보는 방향

                break;
            case ProjectileType.HOMING:         //유도

                break;
            case ProjectileType.TOPLAYER:       //플레이어 방향
                damagingCollider.ShootProjectile_ToPlayer(attackInfo.projectileInfo.projectileSpd);
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

        damagingCollider.DestroyCollider(attackInfo.duration);
    }

}
