using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Character
{
    private bool isMovable;
    public bool IsMovable { get { return isMovable; } set { isMovable = value; } }
    public BoxCollider2D MyCollider { get; private set; }
    private bool isGround;
    public bool IsGround { get { return isGround; } }
    private bool isJumpAniPlaying;
    private bool isLadder;
    public bool IsLadder { get { return isLadder; } set { isLadder = value; } }
    private bool isLadderAction;
    public bool IsLadderAction { get { return isLadderAction; } set { isLadderAction = value; } }
    public bool isElevator;
    private GameObject groundObject;
    private Vector3 previousPos;


    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
        isMovable = true;
        MyCollider = gameObject.GetComponent<BoxCollider2D>();
        //hpUI = GameObject.Find("Canvas").transform.Find("StatInfo").Find("HP").Find("Text").GetComponent<Text>();
        dashCoolUI = GameObject.Find("Canvas").transform.Find("StatInfo").Find("DashCool").Find("Text").GetComponent<Text>();
        jumpCount = 0;
        previousPos = transform.position;

    }

    protected override void Update()
    {
        base.Update();

        /*if (MyInput.GetKeyDown(KeyCode.UpArrow)) JumpAccept();
        //if (MyInput.GetKeyUp(KeyCode.UpArrow)) JumpStop();
        if (MyInput.GetKey(KeyCode.UpArrow)) Jump();  여기까지는 옛 점프(누른 시간 비례 점프) */
        //if (MyInput.GetKeyDown(KeyCode.Q)) circleWeapon(WeaponList.sword);
        //if (MyInput.GetKeyDown(KeyCode.W)) circleWeapon(WeaponList.shield);
        //if (MyInput.GetKeyDown(KeyCode.E)) circleWeapon(WeaponList.fist);

        if (isGround) jumpCount = 0;
        if (isDashable > 0f) isDashable = Mathf.Clamp(isDashable - Time.deltaTime, 0, dashCoolTime);
        if (Map.Instance.CheckOutSide(transform.position)) OnDieCallBack();
        if (MyInput.GetKeyDown(MyKeyCode.Attack)) Action();
        if (IsMovable)
        {
            if (MyInput.GetKeyDown(MyKeyCode.Dash) && isDashable <= 0f) Dash();
            if (MyInput.GetKeyDown(MyKeyCode.Jump)) Jump();
            if (MyInput.GetKeyUp(MyKeyCode.Right)) anim.SetBool("isRunning", false);
            if (MyInput.GetKeyUp(MyKeyCode.Left)) anim.SetBool("isRunning", false);
        }
        if (isGround && isJumping)
        {
            isJumping = false;
            anim.SetBool("isJumping", false);
        }

        DisplayInfo();
        ladderActionAnim();

        CheckFalling();
        previousPos = transform.position;
    }

    protected override void FixedUpdate() //물리연산용
    {
        base.FixedUpdate();
        CheckGround();
        if (IsMovable)
        {
            if (MyInput.GetKey(MyKeyCode.Right))
            {
                anim.SetBool("isRunning", true);
                Move(Direction.right);
            }
            if (MyInput.GetKey(MyKeyCode.Left))
            {
                sprite.GetComponent<SpriteRenderer>().flipX = true;
                anim.SetBool("isRunning", true);
                Move(Direction.left);
            }
        }
    }

    #region Dash/DashAttack

    float isDashable = 0f;
    bool isDashing = false;

    public void Dash()
    {
        isDashing = true;
        if (MyInput.GetKey(MyKeyCode.Right))
        {

            StartCoroutine(PlayerDash(Direction.right));
            sprite.GetComponent<SpriteRenderer>().flipX = false;
           
        }
        else if (MyInput.GetKey(MyKeyCode.Left))
        {
            StartCoroutine(PlayerDash(Direction.left));
            sprite.GetComponent<SpriteRenderer>().flipX = true;

        }
        else
        {
            StartCoroutine(PlayerDash(direction));
        }
        anim.SetBool("isRunning", false);
        anim.SetBool("isDash", true);
        anim.Play("dash");
    }

    private const float dashInvincibleTime = 0.2f;
    private const float dashTime = 0.25f;
    private const float dashCoolTime = 0.25f;
    IEnumerator PlayerDash(Direction dir)
    {
        float timer = 0;
        IsSuper = dashInvincibleTime;

        while(timer < dashTime)
        {
            spd = Mathf.Lerp(Spd * 8f, Spd, timer / dashTime);
            Move(dir);

            timer += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate(); //물리적인 이동같은건 fixedUpdate로
        }

        if (MyInput.GetKey(MyKeyCode.Right)) anim.SetBool("isRunning", true);
        if (MyInput.GetKey(MyKeyCode.Left)) anim.SetBool("isRunning", true);
        anim.SetBool("isDash", false);


        isDashing = false;
        isDashable = dashCoolTime;
    }

    IEnumerator DashAttacking()
    {

        IsMovable = false;
        anim.SetBool("isDashAttack", true);

        yield return new WaitForSeconds(0.3f);

        IsMovable = true;
        anim.SetBool("isDashAttack", false);
        anim.SetBool("isRunning", false);


    }

    #endregion

    private bool isJumping;
    private int jumpCount;
    public int JumpCount { set { jumpCount = value; } }
    private int maxJumpCount = 1;
    public int MaxJumpCount { set { maxJumpCount = value; } }
   
  public void ladderActionAnim()
    {
        if (isLadder)
        {
            anim.SetBool("isRunning", false);
            anim.SetBool("isJumping", false);
            anim.SetBool("isLadder", true);
            if (isLadderAction)
            {
                anim.SetBool("isLadderAction", true);
            }
            else
            {
                anim.SetBool("isLadderAction", false);
            }

        }
        else
        {
            anim.SetBool("isLadderAction", false);
     
            anim.SetBool("isLadder", false);
        }
    }

    #region Jump
    public override void Jump()
    {
        if (jumpCount < maxJumpCount && isGround)
        {
            isJumping = true;
            StopCoroutine("JumpRoutine");
            StartCoroutine("JumpRoutine");
            anim.SetBool("isJumping", true);
            anim.Play("jump_Start");
        }/*
        else if (jumpCount < maxJumpCount+1 && EquipManager.Instance.equipedWeapon.gameObject.name == "Sword")
        {
            StopCoroutine("JumpRoutine");
            StartCoroutine("JumpRoutine");
        }*/
        //점프 구현
    }

    IEnumerator JumpRoutine()
    {
        rigid.velocity = Vector2.zero;
        rigid.AddForce(new Vector2(0, jumpConst * jumpPower));
        jumpCount++;
        isGround = false;
        groundObject = null;
        stopGroundCheck = true;
        yield return new WaitForSeconds(0.1f);
        stopGroundCheck = false;
    }
    
    /*
    //float jumpCount = 0f;

    protected void JumpAccept() // 기획서 상 점프는 일정함, 바뀔수도 있으니 일단 보존
    {
        if (jumpCount > 0 && rigid.velocity.y == 0f)
            jumpCount = 0;
    }

    int jumpCount = 0;

protected void JumpStop()
    {
        jumpCount = 1f;
    }
    */
    #endregion


    private void circleWeapon(WeaponList _weapon) //무기교체
    {
        EquipManager.Instance.changeWeapon(_weapon);
    }

    private void Action() //장착된 무기의 action을 실행
    {
        if (!EquipManager.Instance.equipedWeapon.onAttack)
        {
            if (isDashing)
            {
                EquipManager.Instance.equipedWeapon.DashAttack(atkBuff, attackSpd);
                StartCoroutine(DashAttacking());

                return;
            }
            else
            {
                EquipManager.Instance.equipedWeapon.Action(atkBuff, attackSpd);
            }
        }


    }

    protected override void OnDieCallBack() //죽을 때 부르는 함수
    {
        //애니메이션재생
        gameObject.SetActive(false);
        PlayManager.Instance.Defeat();
    }

    protected override void CheckBuffAndDebuff()
    {
        base.CheckBuffAndDebuff();
        /*
        if (!isGround) //공중에서 횡이동 속도 0.5배
        {
            currentSpd *= 0.5f;
        }
        else
        {
            if (EquipManager.Instance.equipedWeapon.gameObject.name == "Sword" && MyInput.GetKey(MyInput.jump))
            //양손검 상태 플레이어가 Space 입력 시, 대쉬
            {
                currentSpd *= 1.3f;
            }

        }*/
    }

    private Text hpUI;
    private Text dashCoolUI;
    private void DisplayInfo()
    {
        //hpUI.text = hp.ToString();
        //dashCoolUI.text = Math.Round(isDashable, 2).ToString();
    }

    #region GroundCheck
    private bool stopGroundCheck = false;

    /// <summary>
    ///  땅에 있는지 체크
    /// </summary>
    protected virtual void CheckGround()
    {
        /*
        if (isGround || stopGroundCheck) return;

        Vector3 pos = new Vector3(transform.position.x, transform.position.y - transform.localScale.y / 2f - 0.02f, transform.position.z);
        Vector2 scale = new Vector2(transform.localScale.x, 0.005f);
        RaycastHit2D hit = Physics2D.BoxCast(pos, scale, 0, Vector2.down, 0.05f);
        if (hit.transform != null && !hit.collider.isTrigger)
        {
            isGround = true;
        }*/

    }

    protected virtual void OnCollisionExit2D(Collision2D col)
    {
        Debug.Log("air");
        isGround = false;
        groundObject = null;
        //if (MyCollider.bounds.min.y >= col.collider.bounds.max.y && !stopGroundCheck)
    }
    protected virtual void OnCollisionStay2D(Collision2D col)
    {
        if (MyCollider.bounds.min.y >= col.collider.bounds.max.y && !stopGroundCheck)
        {
            isGround = true;
            groundObject = col.gameObject;
            anim.SetBool("isFalling", false);

        }
    }
    #endregion


    public void CheckFalling()
    {
        Debug.Log(stopGroundCheck);
        if (previousPos.y > transform.position.y && !isGround && !isElevator)
        {
            anim.SetBool("isFalling", true);
        }
    }

    public void Reset()
    {
        GameObject clone = Instantiate(gameObject, transform.position, transform.rotation);
        clone.transform.SetParent(transform.parent);
        PlayManager.Instance.Player = clone.GetComponent<Player>();
        EquipManager.SetInstance(clone.transform.Find("Equip").GetComponent<EquipManager>());
        clone.SetActive(true);
        Destroy(gameObject);
    }
    
    private GameObject myPalete;
    public void MakePalette()
    {
        gameObject.SetActive(false);
        myPalete = Instantiate(gameObject, transform.position, transform.rotation); //나의 클론을 RespawnableObjects에 저장해둠
        myPalete.SetActive(false);
        myPalete.transform.SetParent(transform.GetComponentInParent<Stage>().transform.Find("RespawnPalete"));
        myPalete.GetComponent<Player>().myPalete = myPalete;
        gameObject.SetActive(true);
    }

    #region setter
    public void SetTrigger(bool value)
    {
        MyCollider.isTrigger = value;
    }
    
    /// <summary>
    /// 중력값을 수정
    /// </summary>
    /// <param name="value"></param>
    /// <param name="setDefault"> 트루면 기본값으로 변경</param>
    public void SetGravity(float value, bool setDefault)
    {
        if (setDefault)
            rigid.gravityScale = gravityDefault;
        else
            rigid.gravityScale = value;
    }

    public void SetVelocity(Vector2 value)
    {
        rigid.velocity = value;
    }
    public void SetStopGroundCheck(bool value)
    {
        stopGroundCheck = value;
    }
    public void SetIsGround(bool value)
    {
        isGround = value;
    }
    #endregion
}
