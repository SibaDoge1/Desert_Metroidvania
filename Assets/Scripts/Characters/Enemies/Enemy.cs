using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Character
{
    #region Status
    [Header("AttackStatus")] 
    [SerializeField]
    protected float atk = 1; //default value
    [SerializeField]
    protected float atkDelay = 1;
    #endregion

    protected abstract void Action();
    protected abstract void Think();

}
