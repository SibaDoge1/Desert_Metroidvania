using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponList
{
    sword = 0, shield, fist, end
}

public abstract class Weapon : MonoBehaviour
{
    [Header("AtackStatus")]

    [SerializeField]
    protected List<AttackInfo> attackInfos = new List<AttackInfo>();

    protected bool onAttack = false;
    
    public abstract void Action();
    protected abstract void Awake(); //attackInfo + 에네미에도 해줘야함
}
