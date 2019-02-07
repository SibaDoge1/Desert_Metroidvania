﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingCollider : MonoBehaviour
{
    public float damage;
    public Sprite colliderSprite;

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.transform.parent.tag == "Enemy"/* && c.transform.parent.GetComponent<Character>().IsSuper <= 0f*/)    //무적 시간? 있나?
            c.transform.parent.GetComponent<Character>().GetDamage(damage);
    }

    public void ChangeSprite()
    {
        GetComponent<SpriteRenderer>().sprite = colliderSprite;
    }

    public void DestroyCollider(float duration)
    {
        StartCoroutine(DestroyTimer(duration));
    }

    IEnumerator DestroyTimer(float duration)
    {
        yield return new WaitForSeconds(duration);

        Destroy(gameObject);
    }
}