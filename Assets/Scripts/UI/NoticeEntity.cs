using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoticeEntity : MonoBehaviour
{
    private float remainTime;

    public void Active(float time)
    {
        if(time > 0)
        {
            remainTime = time;
            StartCoroutine(DestroyRoutine());
        }
    }

    IEnumerator DestroyRoutine()
    {
        while (remainTime > 0)
        {
            remainTime -= Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }
}
