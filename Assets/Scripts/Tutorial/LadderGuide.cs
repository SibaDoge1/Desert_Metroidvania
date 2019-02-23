using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderGuide : GuideObject
{
    public Ladder linkedLadder;

    // Update is called once per frame
    protected override void Update()
    {
        if (MyInput.GetKey(code) && linkedLadder.isUsingLadder)
        {
            Destroy(gameObject);
        }
    }
}
