using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractGuide : GuideObject
{
    public InteractObject linkedInteract;
    protected override void Trigger()
    {
        if (linkedInteract.IsAtObject)
        {
            gameObject.SetActive(false);
        }
    }
}
