using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearObj : InteractObject
{
    public PotalWithLock potal;
    private SpriteRenderer image;
    private bool isActive = false;

    public void Active(GameObject sprite)
    {
        gameObject.SetActive(true);
        sprite.GetComponent<Animator>().Play("Die");
        image = sprite.GetComponent<SpriteRenderer>();
        sprite.transform.SetParent(transform);
        StartCoroutine(BossClear());
    }

    protected override void Action()
    {
        if (!isActive) return;
        if (potal != null)
        {
            potal.Unlock();
        }
        SaveManager.SaveToFile();
        PlayManager.Instance.GoToTitle();

    }

    IEnumerator BossClear()
    {
        float remainTime = 1f;
        while(image.color.a > 0)
        {
            Color col = image.color;
            col.a = remainTime / 1f;
            image.color = col;
            remainTime -= Time.deltaTime;
            yield return null;
        }
        isActive = true;
    }
}
