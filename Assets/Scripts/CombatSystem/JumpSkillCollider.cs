using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpSkillCollider : DamagingCollider
{

    public bool isTriggered = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void OnTriggerEnter2D(Collider2D c)
    {
        base.OnTriggerEnter2D(c);

        if (c.tag == "Enemy" || c.tag == "Tile")
            isTriggered = true;
    }
}
