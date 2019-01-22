using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Character
{
    protected float Atk;
    protected float preDelay;
    protected float postDelay;
    protected float inDelay;

    public abstract void Action();
}
