using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponList
{
    sword, shield, fist
}

public class Player : Character
{

    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow)) Move(Direction.right);
        //if (Input.GetKeyUp(KeyCode.RightArrow)) Move(Direction.zero);
        if (Input.GetKey(KeyCode.LeftArrow)) Move(Direction.left);
        //if (Input.GetKeyUp(KeyCode.RightArrow)) Move(Direction.zero);
        if (Input.GetKey(KeyCode.X)) Jump();
        if (Input.GetKeyDown(KeyCode.A)) circleWeapon();
        if (Input.GetKeyDown(KeyCode.Z)) Action();
    }

    public void circleWeapon() //무기교체
    {
        EquipManager.Instance.changeWeapon(WeaponList.shield);
    }

    public void Action() //장착된 무기의 action을 실행
    {
        EquipManager.Instance.equipedWeapon.Action();
    }
}
