using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingCollider : MonoBehaviour
{
    public float damage;
    public Sprite colliderSprite;
    public Transform parentTransform;
    public GameObject hitEffect;

    protected virtual void OnTriggerEnter2D(Collider2D c)
    {
        //if (c.transform.parent == null) return;

        if ((parentTransform.tag != "Enemy" && c.tag == "Enemy") || c.tag == "Player")
        {   
            //무적 시간? 있나?
            c.GetComponent<Character>().GetDamage(damage, transform);
            if (hitEffect != null)
            {
                Instantiate(hitEffect, transform.position, transform.rotation);
            }
        }
    }

    public void ChangeSprite(Vector3 size)
    {
        transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = colliderSprite;
        transform.GetChild(1).localScale = size;
    }

    public void DestroyCollider(float duration)
    {
        StartCoroutine(DestroyTimer(duration));
    }

    IEnumerator DestroyTimer(float duration)
    {
        yield return new WaitForSeconds(duration);

        OnDestroyCallBack();
    }

    public virtual void OnDestroyCallBack()
    {
        StopAllCoroutines();
        Destroy(gameObject);
    }
}
