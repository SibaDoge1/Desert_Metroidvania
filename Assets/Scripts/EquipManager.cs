using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipManager : MonoBehaviour
{
    private Weapon EquipedWeapon;
    public Weapon equipedWeapon { get { return EquipedWeapon; } set { EquipedWeapon = value; } }

    void Awake()
    {
        equipedWeapon = transform.Find("Weapon").transform.GetChild(0).GetComponent<Weapon>();     //왠지 안돼서 플레이어 컴포넌트에 붙임

    }
    public void changeWeapon(WeaponList weaponName)
    {
        equipedWeapon.gameObject.SetActive(false);
        equipedWeapon = transform.Find("Weapon").transform.GetChild((int)weaponName % (int)WeaponList.end).GetComponent<Weapon>();
        equipedWeapon.gameObject.SetActive(true);
        Debug.Log((int)weaponName % (int)WeaponList.end + " : " + equipedWeapon.gameObject.name);
    }

    public void AddItem(GameObject item)
    {
        item.transform.SetParent(transform);
    }
}

