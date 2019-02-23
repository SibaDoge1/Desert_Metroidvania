using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RespawnableEnemy : Enemy, Respawnable
{
    private Vector3 defaultPos;
    private int defaultHp;
    private EnemyState defaultState;
    private GameObject myPalete;

    protected override void Awake()
    {
        base.Awake();
        defaultHp = hp;
        defaultPos = transform.position;
        defaultState = enemyState;
        /*
        myPalete = Instantiate(gameObject, transform.position, transform.rotation); //나의 클론을 RespawnableObjects에 저장해둠
        myPalete.SetActive(false);
        myPalete.transform.SetParent(transform.parent.parent.Find("RespawnPalete"));
        */
    }
    public virtual void Reset()
    {
        hp = defaultHp;
        transform.position = defaultPos;
        enemyState = defaultState;
        /*
        myPalete.transform.SetParent(transform.parent);
        myPalete.SetActive(true);
        Destroy(gameObject);
        */
    }
}
