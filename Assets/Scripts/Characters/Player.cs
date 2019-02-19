using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Character
{
    private bool isMovable;
    public bool IsMovable { get { return isMovable; } set { isMovable = value; } }
    private BoxCollider2D myCollider;
    private GameObject sprite;
    private bool isGround;
    public bool IsGround { get { return isGround; } }
    private bool isJumpAniPlaying;
    private bool isLadder;
    public bool IsLadder { get { return isLadder; } set { isLadder = value; } }
    private bool isLadderAction;
    public bool IsLadderAction { get { return isLadderAction; } set { isLadderAction = value; } }
    private GameObject groundObject;


    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
        isMovable = true;
        myCollider = gameObject.GetComponent<BoxCollider2D>();
        sprite = transform.Find("Sprite").gameObject;
        hpUI = GameObject.Find("Canvas").transform.Find("StatInfo").Find("HP").Find("Text").GetComponent<Text>();
        dashCoolUI = GameObject.Find("Canvas").transform.Find("StatInfo").Find("DashCool").Find("Text").GetComponent<Text>();
        jumpCount = 0;
    }

    protected override void Update()
    {
        base.Update();

        /*if (Input.GetKeyDown(KeyCode.UpArrow)) JumpAccept();
        //if (Input.GetKeyUp(KeyCode.UpArrow)) JumpStop();
        if (Input.GetKey(KeyCode.UpArrow)) Jump();  여기까지는 옛 점프(누른 시간 비례 점프) */
        //if (Input.GetKeyDown(KeyCode.Q)) circleWeapon(WeaponList.sword);
        //if (Input.GetKeyDown(KeyCode.W)) circleWeapon(WeaponList.shield);
        //if (Input.GetKeyDown(KeyCode.E)) circleWeapon(WeaponList.fist);
        if (isGround) jumpCount = 0;
        if (isDashable > 0f) isDashable = Mathf.Clamp(isDashable - Time.deltaTime, 0, dashCoolTime);
        if (Map.Instance.CurStage.checkOutSide(transform.position)) OnDieCallBack();
        if (Input.GetKeyDown(KeyCode.A)) Action();
        if (IsMovable)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && isDashable <= 0f) Dash();
            if (Input.GetKeyDown(KeyCode.Space)) Jump();
            if (Input.GetKeyUp(KeyCode.RightArrow)) sprite.GetComponent<Animator>().SetBool("isRunning", false);
            if (Input.GetKeyUp(KeyCode.LeftArrow)) sprite.GetComponent<Animator>().SetBool("isRunning", false);
        }
        if (isGround && isJumping)
        {
            isJumping = false;
            sprite.GetComponent<Animator>().SetBool("isJumping", false);
        }
        DisplayInfo();
        ladderActionAnim();
    }

    protected override void FixedUpdate() //물리연산용
    {
        base.FixedUpdate();
        CheckGround();
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
    }

    #region Dash/DashAttack

    float isDashable = 0f;
    bool isDashing = false;

    public void Dash()
    {
        isDashing = true;
        if (Input.GetKey(KeyCode.RightArrow))
        {

            StartCoroutine(PlayerDash(Direction.right));
            sprite.GetComponent<SpriteRenderer>().flipX = false;
           
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            StartCoroutine(PlayerDash(Direction.left));
            sprite.GetComponent<SpriteRenderer>().flipX = true;

        }
        else
        {
            StartCoroutine(PlayerDash(direction));
        }
        sprite.GetComponent<Animator>().SetBool("isRunning", false);
        sprite.GetComponent<Animator>().SetBool("isDash", true);
        sprite.GetComponent<Animator>().Play("dash");
    }

    private const float dashInvincibleTime = 0.2f;
    private const float dashTime = 0.25f;
    private const float dashCoolTime = 0.25f;
    IEnumerator PlayerDash(Direction dir)
    {
        IsMovable = false;
        float timer = 0;
        IsSuper = dashInvincibleTime;

        while(timer < dashTime)
        {
            currentSpd = Mathf.Lerp(Spd * 8f, Spd, timer / dashTime);
            Move(dir);

            timer += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate(); //물리적인 이동같은건 fixedUpdate로
        }

        if (Input.GetKey(KeyCode.RightArrow)) sprite.GetComponent<Animator>().SetBool("isRunning", true);
        if (Input.GetKey(KeyCode.LeftArrow)) sprite.GetComponent<Animator>().SetBool("isRunning", true);
        sprite.GetComponent<Animator>().SetBool("isDash", false);


        isDashing = false;
        isDashable = dashCoolTime;
        IsMovable = true;
    }

    IEnumerator DashAttacking()
    {

        IsMovable = false;
        sprite.GetComponent<Animator>().SetBool("isDashAttack", true);

        yield return new WaitForSeconds(0.3f);

        IsMovable = true;
        sprite.GetComponent<Animator>().SetBool("isDashAttack", false);
        sprite.GetComponent<Animator>().SetBool("isRunning", false);


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
            sprite.GetComponent<Animator>().SetBool("isRunning", false);
            sprite.GetComponent<Animator>().SetBool("isJumping", false);
            sprite.GetComponent<Animator>().SetBool("isLadder", true);
            if (isLadderAction)
            {
                sprite.GetComponent<Animator>().SetBool("isLadderAction", true);
            }
            else
            {
                sprite.GetComponent<Animator>().SetBool("isLadderAction", false);
            }

        }
        else
        {
            sprite.GetComponent<Animator>().SetBool("isLadderAction", false);
     
            sprite.GetComponent<Animator>().SetBool("isLadder", false);
        }
    }

    #region Jump
    public override void Jump()
    {
        if (jumpCount < maxJumpCount)
        {
            isJumping = true;
            StopCoroutine("JumpRoutine");
            StartCoroutine("JumpRoutine");
            sprite.GetComponent<Animator>().SetBool("isJumping", true);
            sprite.GetComponent<Animator>().Play("jump_Start");
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
        
        if (isDashing)
        {
            EquipManager.Instance.equipedWeapon.DashAttack(atkBuff, attackSpd);
            StartCoroutine(DashAttacking());


            return;
        }
        EquipManager.Instance.equipedWeapon.Action(atkBuff, attackSpd);
    }

    protected override void OnDieCallBack() //죽을 때 부르는 함수
    {
        PlayManager.Instance.Defeat();
        Destroy(gameObject);
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
            if (EquipManager.Instance.equipedWeapon.gameObject.name == "Sword" && Input.GetKey(KeyCode.Space))
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
        hpUI.text = hp.ToString();
        dashCoolUI.text = Math.Round(isDashable, 2).ToString();
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

    protected virtual void OnCollisionStay2D(Collision2D col)
    {
        if (myCollider.bounds.min.y >= col.collider.bounds.max.y && !stopGroundCheck)
        {
            isGround = true;
            groundObject = col.gameObject;
        }
    }

    protected virtual void OnCollisionExit2D(Collision2D col)
    {
        if (myCollider.bounds.min.y >= col.collider.bounds.max.y)
        {
            Debug.Log("air");
            isGround = false;
            groundObject = null;
        }
    }
    #endregion

    #region setter
    public void SetTrigger(bool value)
    {
        myCollider.isTrigger = value;
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
    #endregion
}
