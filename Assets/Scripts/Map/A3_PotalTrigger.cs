using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A3_PotalTrigger : InteractObject
{
    public GameObject layer;
    public float openTime;

    protected override void Action()
    {
        StartCoroutine(OpenRoutine());
    }
    IEnumerator OpenRoutine()
    {
        float timer = 0f;
        Vector3 defaultScale = layer.transform.localScale;
        Vector3 defaultPos = layer.transform.localPosition;
        Vector3 scale;
        Vector3 pos;
        while (timer < openTime)
        {
            scale = layer.transform.localScale;
            pos = layer.transform.localPosition;
            scale.x = Mathf.Lerp(defaultScale.x, 0.9f, timer / openTime);
            pos.x = Mathf.Lerp(defaultPos.x, -4f, timer / openTime);
            layer.transform.localScale = scale;
            layer.transform.localPosition = pos;
            timer += Time.deltaTime;
            yield return null;
        }
    }
}
