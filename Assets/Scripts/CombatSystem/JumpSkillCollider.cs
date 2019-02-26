using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpSkillCollider : DamagingCollider
{

    public bool isTriggered = false;


    protected override void OnTriggerEnter2D(Collider2D c)
    {
        base.OnTriggerEnter2D(c);

        if (c.tag == "Enemy")
            isTriggered = true;

        if (c.tag == "Tile" || c.tag == "Boundary")
            isTriggered = true;
    }
}
