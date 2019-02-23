using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProjectileType
{
    HOMING, DIRECTION, TOPLAYER
}

public class Projectile : DamagingCollider
{
    Vector2 dir;
    float spd;

    public void ChangeProjectileSprite(Sprite projSprite)
    {
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = projSprite;
    }
    
    public void ShootProjectile_ToPlayer(float _spd)
    {
        Vector3 playerPos = PlayManager.Instance.Player.transform.position;
        Vector3 projPos = transform.position;

        spd = _spd;
        dir.x = playerPos.x - projPos.x;
        dir.y = playerPos.y - projPos.y;

        StartCoroutine(MoveProjectile());
    }

    IEnumerator MoveProjectile()
    {
        while(true)
        {
            transform.Translate(dir * spd * Time.deltaTime);

            if (Map.Instance.CheckOutSide(transform.position))
            //벽 뚫는 거 방지
            {
                OnDestroyCallBack();
            }

            yield return null;
        }
    }
}
