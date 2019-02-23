using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RespawnableEnemy : Enemy, Respawnable
{
    private GameObject myPalete;

    protected override void Awake()
    {
        base.Awake();
    }

    public virtual void Reset()
    {/*
        hp = defaultHp;
        transform.position = defaultPos;
        enemyState = defaultState;
        */
        GameObject clone = Instantiate(gameObject, transform.position, transform.rotation);
        clone.transform.SetParent(transform.parent);
        clone.SetActive(true);
        Destroy(gameObject);
    }

    public void FirstSet()
    {
        gameObject.SetActive(false);
        myPalete = Instantiate(gameObject, transform.position, transform.rotation); //나의 클론을 RespawnableObjects에 저장해둠
        myPalete.SetActive(false);
        myPalete.transform.SetParent(transform.GetComponentInParent<Stage>().transform.Find("RespawnPalete"));
        myPalete.GetComponent<RespawnableEnemy>().myPalete = myPalete;
        gameObject.SetActive(true);
    }
}
