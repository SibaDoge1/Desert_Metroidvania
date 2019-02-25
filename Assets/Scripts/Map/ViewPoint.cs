using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewPoint : InteractObject
{
    public Stage linkedStage;

    protected override void Action()
    {
        //linkedStage.GetMapInfo();
        if (transform.parent.GetComponentInParent<Stage>().isMapInfoObtained == false)
        {
            NoticeUI.Instance.MakeNotice("이 지역의 지도를 획득했다", 3f);
            transform.parent.GetComponentInParent<Stage>().UnlockMapInfo();
        }
    }
}
