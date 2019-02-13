using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct AttackInfo
{
    public Vector2 attackRange;
    public Vector2 hitBoxPostion;
    public float damage;
    public float duration;
    public float preDelay;
    public float postDelay;
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
            Debug.LogError("Singleton Error! : " + this.name);
            Destroy(gameObject);
        }
        _damagingCollider = Resources.Load("Prefabs/DamagingCollider") as GameObject;
    }

    GameObject _damagingCollider;

    public void InstantiateHitBox(AttackInfo attackInfo, Transform transform_attacker)
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
            damagingCollider.ChangeSprite();
        }

        damagingCollider.gameObject.transform.parent = transform_attacker;
        damagingCollider.DestroyCollider(attackInfo.duration);
    }
    
}
