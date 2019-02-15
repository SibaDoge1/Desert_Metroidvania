﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractObject : MonoBehaviour
{
    protected bool isAtObject = false;
    protected Player player;
    protected KeyCode myKey = KeyCode.Tab;

    protected virtual void Update()
    {
        if (Input.GetKeyDown(myKey) && isAtObject)
        {
            Action();
        }
    }

    protected abstract void Action();

    protected virtual void OnTriggerStay2D(Collider2D col)
    {
        if (col.transform.CompareTag("Player"))
        {
            isAtObject = true;
            player = col.GetComponent<Player>();
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D col)
    {
        if (col.transform.CompareTag("Player"))
        {
            isAtObject = false;
            player = null;
        }
    }
}