using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewPoint : InteractObject
{
    [SerializeField]
    private Stage linkedStage;

    protected override void Action()
    {
        linkedStage.GetMapInfo();
    }
}
