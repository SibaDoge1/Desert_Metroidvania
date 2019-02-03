using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponList
{
    sword, shield, fist
}

public class Player : Character
{
    private bool isMovable;
    public bool IsMovable { get{ return isMovable ;} set { isMovable = value; } }
    private BoxCollider2D myCollider;
    private bool isJumping;
    private int jumpCount;
    public int JumpCount { set { jumpCount = value; } }
    private int maxJumpCount = 1;
    private GameObject sprite;

    void Start()
    {
        isMovable = true;
        myCollider = gameObject.GetComponent<BoxCollider2D>();
        sprite = transform.Find("Sprite").gameObject;
        isJumping = false;
        jumpCount = 0;
    }

    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Alpha1)) circleWeapon(WeaponList.sword);
        if (Input.GetKeyDown(KeyCode.Alpha2)) circleWeapon(WeaponList.shield);
        if (Input.GetKeyDown(KeyCode.Alpha3)) circleWeapon(WeaponList.fist);
        if (Input.GetKeyDown(KeyCode.S)) Action();
        if (isGround) jumpCount = 0;
        if (IsMovable)
        {
            if (Input.GetKeyDown(KeyCode.X)) // FixedUpdate에서 사용하면 키가 씹힘
                isJumping = true;
            if (Input.GetKeyUp(KeyCode.RightArrow)) sprite.GetComponent<Animator>().SetBool("isRunning", false);
            if (Input.GetKeyUp(KeyCode.LeftArrow)) sprite.GetComponent<Animator>().SetBool("isRunning", false);
        }
    }

    protected override void FixedUpdate() //물리연산용
    {
        base.FixedUpdate();
        if (IsMovable)
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                sprite.GetComponent<SpriteRenderer>().flipX = false;
                sprite.GetComponent<Animator>().SetBool("isRunning", true);
                Move(Direction.right);
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                sprite.GetComponent<SpriteRenderer>().flipX = true;
                sprite.GetComponent<Animator>().SetBool("isRunning", true);
                Move(Direction.left);
            }
        }
        if (isJumping)
        {
            Jump();
            isJumping = false;
        }
    }

    protected override void Jump()
    {
        if (jumpCount < maxJumpCount)
        {
            SetVelocity(Vector2.zero);
            rigid.AddForce(new Vector2(0, jumpConst * jumpPower));
            jumpCount++;
            isGround = false;
        }
        //점프 구현
    }

    private void circleWeapon(WeaponList _weapon) //무기교체
    {
        EquipManager.Instance.changeWeapon(_weapon);
    }

    private void Action() //장착된 무기의 action을 실행
    {
        EquipManager.Instance.equipedWeapon.Action();
    }

    protected override void OnDieCallBack() //죽을 때 부르는 함수
    {
        Destroy(gameObject);
    }

    protected override void CheckBuffAndDebuff()
    {
        base.CheckBuffAndDebuff();

        if (!isGround) //공중에서 횡이동 속도 0.5배
        {
           currentSpd *= 0.5f;
        }
    }

    public void SetTrigger(bool value)
    {
        myCollider.isTrigger = value;
    }

    public void SetGravity(float value)
    {
        rigid.gravityScale = value;
    }
    public void SetVelocity(Vector2 value)
    {
        rigid.velocity = value;
    }




}
