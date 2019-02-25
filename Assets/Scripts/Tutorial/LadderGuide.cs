using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderGuide : GuideObject
{
    public Ladder linkedLadder;

    // Update is called once per frame
    protected override void Trigger()
    {
        if (linkedLadder.isUsingLadder)
        {
            Destroy(gameObject);
        }
    }
}
