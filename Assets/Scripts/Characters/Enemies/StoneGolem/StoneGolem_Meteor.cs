using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneGolem_Meteor : DamagingCollider
{
    private void Awake()
    {
        damage = 1;
    }

    protected override void OnTriggerEnter2D(Collider2D c)
    {
        base.OnTriggerEnter2D(c);

        if (c.tag == "Player")
            OnDestroyCallBack();

        if (c.tag == "Tile" || c.tag == "Boundary")
            OnDestroyCallBack();
    }

    public override void OnDestroyCallBack()
    {
        base.OnDestroyCallBack();
    }
}
