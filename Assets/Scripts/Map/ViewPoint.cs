using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewPoint : InteractObject
{
    public Stage ParentStage { get; private set; }

    void Awake()
    {
        ParentStage = transform.parent.parent.GetComponent<Stage>();
    }

    protected override void Action()
    {

    }
}
