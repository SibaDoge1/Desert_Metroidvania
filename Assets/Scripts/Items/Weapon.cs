using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class Weapon : Item
{
    protected float Atk;
    protected float preDelay;
    protected float postDelay;
    protected float inDelay;

    public abstract void Action();
}
