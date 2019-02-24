using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewPoint : InteractObject
{
    public Stage linkedStage;

    protected override void Action()
    {
        //linkedStage.GetMapInfo();
        transform.parent.parent.GetComponent<Stage>().UnlockMapInfo();
    }
}
