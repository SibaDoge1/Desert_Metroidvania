using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : InteractObject
{
    protected override void Action()
    {
        NoticeUI.Instance.MakeNotice("세이브를 합니다...", 3f);
        if (SaveManager.SaveToFile())
            NoticeUI.Instance.MakeNotice("세이브 성공!", 3f);
    }
}
