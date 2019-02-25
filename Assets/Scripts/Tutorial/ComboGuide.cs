using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboGuide : GuideObject
{
    public GameObject linkedObj;
    protected override void Trigger()
    {
        if (linkedObj.active == false)
        {
            StartCoroutine(Check());
        }
    }

    IEnumerator Check()
    {
        while (true)
        {
            yield return null;
            if (MyInput.GetKeyDown(code))
            {
                gameObject.SetActive(false);
            }
        }
    }
}
