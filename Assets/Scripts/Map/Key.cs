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
            NoticeUI.Instance.MakeNotice(noticeStr, 3f);
            isObtained = true;
        }
    }
}
