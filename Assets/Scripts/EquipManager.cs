using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipManager : MonoBehaviour
{
    private static EquipManager _instance;
    private Weapon EquipedWeapon;
    public Weapon equipedWeapon { get { return EquipedWeapon; } set { EquipedWeapon = value; } }

    void Awake()
    {
        if (_instance == null) _instance = this;
        equipedWeapon = transform.Find("Weapon").GetChild(0).GetComponent<Weapon>();
    }

    public static EquipManager Instance
    {
        get
        {
            return _instance;
        }
    }

    public void changeWeapon(WeaponList weaponName)
    {

    }
}
