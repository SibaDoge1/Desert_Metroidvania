using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class Weapon : Item
{
    [Header("AtackStatus")]
    [SerializeField]
    protected float atk = 1;  //default value
    [SerializeField]
    protected float preDelay = 0.5f;
    [SerializeField]
    protected float postDelay = 0.5f;
    [SerializeField]
    protected float inDelay = 0.5f;
    [SerializeField]
    protected float range = 0.5f;


    protected bool onAttack = false;

    public abstract void Action();
}
