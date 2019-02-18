using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : InteractObject
{
    protected override void Action()
    {
        SaveManager.SaveAll();
    }
}
