using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearObj : InteractObject
{
    public PotalWithLock potal;
    private SpriteRenderer bossImage;
    private SpriteRenderer image;
    private bool isActive = false;

    public void Active(GameObject sprite)
    {
        gameObject.SetActive(true);
        sprite.GetComponent<Animator>().Play("Die");
        bossImage = sprite.GetComponent<SpriteRenderer>();
        image = transform.Find("Sprite").GetComponent<SpriteRenderer>();
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
        float remainTime = 2f;
        while(remainTime > 0)
        {
            remainTime -= Time.deltaTime;
            yield return null;
        }
        image.gameObject.SetActive(true);
        isActive = true;
    }
}
