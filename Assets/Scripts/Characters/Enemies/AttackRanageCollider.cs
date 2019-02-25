using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRanageCollider : MonoBehaviour
{
    Enemy parentEnemy;

    public bool attackable;
    public float cooltime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        parentEnemy = transform.parent.GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cooltime > 0)
            cooltime -= Time.deltaTime;
    }

    protected virtual void OnTriggerEnter2D(Collider2D c)
    {
        if (c.tag == "Player")
        {
            attackable = true;
        }

    }

    protected virtual void OnTriggerExit2D(Collider2D c)
    {

        if (c.tag == "Player")
        {
            attackable = false;
        }
    }
}
