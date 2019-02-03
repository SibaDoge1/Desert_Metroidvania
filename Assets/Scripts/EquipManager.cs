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
        equipedWeapon = transform.Find("Equip").transform.Find("Weapon").transform.GetChild(0).GetComponent<Weapon>();     //왠지 안돼서 플레이어 컴포넌트에 붙임
        Debug.Log(equipedWeapon.gameObject.name);
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
