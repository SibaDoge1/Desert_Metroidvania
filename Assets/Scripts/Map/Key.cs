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
            EquipManager.Instance.AddItem(gameObject);
            StartCoroutine(CamShakeRoutine());
            isObtained = true;
        }
    }

    public void Obtain()
    {
        if (!isObtained)
        {
            EquipManager.Instance.AddItem(gameObject);
            isObtained = true;
        }
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
