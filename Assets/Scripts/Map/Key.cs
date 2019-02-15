using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : InteractObject
{
    protected override void Action()
    {
        EquipManager.Instance.AddItem(gameObject);
    }
}
