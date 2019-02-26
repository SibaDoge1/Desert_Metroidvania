using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : InteractObject
{
    public string noticeStr; //획득 시 나오는 다이알로그
    private bool isObtained = false;

    protected override void Action()
    {
        if (!isObtained)
        {
            isObtained = true;
            GameObject obj = Instantiate(gameObject, transform.position, transform.rotation, transform.parent);
            obj.transform.Find("Image").transform.localPosition = new Vector2(-0.4f, -0.2f);
            obj.transform.Find("Image").transform.rotation = Quaternion.Euler(new Vector3(0, 0,45));
            obj.GetComponent<Key>().StartShake();
            PlayManager.Instance.Player.equip.AddItem(gameObject);
            gameObject.SetActive(false);
        }
    }

    public void Obtain()
    {
        if (!isObtained)
        {
            isObtained = true;
            GameObject obj = Instantiate(gameObject, transform.position, transform.rotation, transform.parent);
            obj.GetComponent<SpriteRenderer>().flipX = true;
            gameObject.SetActive(false);
            PlayManager.Instance.Player.equip.AddItem(gameObject);
        }
    }

    public void StartShake()
    {
        StartCoroutine(CamShakeRoutine());
    }

    IEnumerator CamShakeRoutine()
    {
        float timer = 0f;
        while (timer < 2f)
        {
            CameraManager.Instance.MoveCam(Random.insideUnitCircle * 0.2f + (Vector2)CameraManager.Instance.transform.position);
            timer += Time.deltaTime;
            yield return null;
        }
    }
}
