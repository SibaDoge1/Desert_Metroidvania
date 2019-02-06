using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponList
{
    sword = 0, shield, fist
}

public class Player : Character
{

    protected override void Update()
    {
        base.Update();

        CheckBuffAndDebuff();

        if (Input.GetKey(KeyCode.RightArrow))
        {
            Move(Direction.right);
            direction = Direction.right;
            GetComponent<SpriteRenderer>().flipX = false;
        }
        //if (Input.GetKeyUp(KeyCode.RightArrow)) Move(Direction.zero);
        if (Input.GetKey(KeyCode.LeftArrow)) 
        {
            Move(Direction.left);
            direction = Direction.left;
            GetComponent<SpriteRenderer>().flipX = true;
        }
        //if (Input.GetKeyUp(KeyCode.RightArrow)) Move(Direction.zero);
        if (Input.GetKeyDown(KeyCode.UpArrow)){
            JumpAccept();
            Jump();
        }
        /*if (Input.GetKeyDown(KeyCode.UpArrow)) JumpAccept();
        //if (Input.GetKeyUp(KeyCode.UpArrow)) JumpStop();
        if (Input.GetKey(KeyCode.UpArrow)) Jump();  여기까지는 옛 점프(누른 시간 비례 점프) */
        if (Input.GetKeyDown(KeyCode.Alpha1)) circleWeapon(WeaponList.sword);
        if (Input.GetKeyDown(KeyCode.Alpha2)) circleWeapon(WeaponList.shield);
        if (Input.GetKeyDown(KeyCode.Alpha3)) circleWeapon(WeaponList.fist);
        if (Input.GetKeyDown(KeyCode.S)) Action();
    }

    public void circleWeapon(WeaponList _weapon) //무기교체
    {
        EquipManager.Instance.changeWeapon(_weapon);
    }

    public void Action() //장착된 무기의 action을 실행
    {
        EquipManager.Instance.equipedWeapon.Action();
    }

    public override void OnDieCallBack() //죽을 때 부르는 함수
    {
        Destroy(gameObject);
    }
}
