using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRanageCollider : MonoBehaviour
{
    Enemy parentEnemy;

    public bool attackable;
    public bool isMaxAttackRanage;      //이게 가장 넓은 공격 범위인지 체크

    // Start is called before the first frame update
    void Start()
    {
        parentEnemy = transform.parent.GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        attackable = true;

        if (isMaxAttackRanage && c.tag == "Player")
            transform.parent.GetComponent<Enemy>().OnTriggerEnterAttack();
    }

    private void OnTriggerExit2D(Collider2D c)
    {
        attackable = false;

        if (isMaxAttackRanage && c.tag == "Player")
            transform.parent.GetComponent<Enemy>().OnTriggerExitAttack();
    }
}
