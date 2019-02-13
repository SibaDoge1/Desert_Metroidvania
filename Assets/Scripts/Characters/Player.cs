using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : Character
{
    private static Player instance = null;
    public static Player Instance
    {
        get { return instance; }
    }
    private bool isMovable;
    public bool IsMovable { get { return isMovable; } set { isMovable = value; } }
    private BoxCollider2D myCollider;
    private GameObject sprite;
    private bool isGround;
    public bool IsGround { get { return isGround; } }

    protected override void Awake()
    {
        base.Awake();
        if (instance == null) instance = this;
        else if (instance != this)
        {
            Debug.LogError("Singleton Error! : " + this.name);
            Destroy(gameObject);
        }
    }

    void Start()
    {
        isGround = true;
        isMovable = true;
        myCollider = gameObject.GetComponent<BoxCollider2D>();
        sprite = transform.Find("Sprite").gameObject;
        isJumping = false;
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
        if (isDashable > 0f) isDashable -= Time.deltaTime;
        if (IsMovable)
        {
            if (Input.GetKeyDown(KeyCode.A)) Action();
            if (Input.GetKeyDown(KeyCode.Space)) // FixedUpdate에서 사용하면 키가 씹힘
                isJumping = true;
            if (Input.GetKeyUp(KeyCode.RightArrow)) sprite.GetComponent<Animator>().SetBool("isRunning", false);
            if (Input.GetKeyUp(KeyCode.LeftArrow)) sprite.GetComponent<Animator>().SetBool("isRunning", false);
        }
    }

    protected override void FixedUpdate() //물리연산용
    {
        base.FixedUpdate();
        CheckGround();
        if (IsMovable)
        {
            if (Input.GetKey(KeyCode.LeftShift) && isDashable <= 0f)
            {
                if (Input.GetKey(KeyCode.RightArrow))
                    StartCoroutine(PlayerDash(Direction.right));

                else if (Input.GetKey(KeyCode.LeftArrow))
                    StartCoroutine(PlayerDash(Direction.left));

                else StartCoroutine(PlayerDash(direction));
            }
            else if (isDashAttacking)
            {
                EquipManager.Instance.equipedWeapon.DashAttack();
                isDashAttacking = false;
                StartCoroutine(DashAttacking());
            }
            else
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
        if (isJumping)
        {
            Jump();
            isJumping = false;
        }
    }

    #region Dash/DashAttack

    float isDashable = 0f;
    bool isDashAttacking = false;

    IEnumerator PlayerDash(Direction dir)
    {
        float timer = 0f;

        IsMovable = false;
        IsSuper = 0.2f;

        while(true)
        {
            if (timer >= 0.2f)
                break;

            if (Input.GetKey(KeyCode.A) && !isDashAttacking)
            {
                isDashAttacking = true;
            }

            currentSpd = Mathf.Lerp(Spd * 8f, Spd, timer * 5f);
            Move(dir);

            timer += Time.deltaTime;
            yield return null;
        }

        isDashable = 0.25f;

        IsMovable = true;
    }
    IEnumerator DashAttacking()
    {
        IsMovable = false;

        yield return new WaitForSeconds(0.15f);

        IsMovable = true;
    }

    #endregion

    private bool isJumping;
    private int jumpCount;
    public int JumpCount { set { jumpCount = value; } }
    private int maxJumpCount = 1;
    public int MaxJumpCount { set { maxJumpCount = value; } }

    #region Jump
    protected override void Jump()
    {
        if (jumpCount < maxJumpCount)
        {
            StopCoroutine("JumpRoutine");
            StartCoroutine("JumpRoutine");
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
        EquipManager.Instance.equipedWeapon.Action();
    }

    protected override void OnDieCallBack() //죽을 때 부르는 함수
    {
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

    protected virtual void OnDrawGizmos()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y - transform.localScale.y / 2f - 0.01f, transform.position.z);
        Vector2 scale = new Vector2(transform.localScale.x, 0.005f);
        RaycastHit2D hit = Physics2D.BoxCast(pos, scale, 0, Vector2.down, 0.05f);
        Gizmos.color = Color.red;
        if (hit.transform != null)
        {
            Gizmos.DrawRay(pos, Vector2.down * hit.distance);
            Gizmos.DrawWireCube(pos + Vector3.down * hit.distance, scale);
        }
        else
            Gizmos.DrawRay(pos, Vector3.down * 5f);
    }

    private bool stopGroundCheck =false;
    /// <summary>
    ///  땅에 있는지 체크
    /// </summary>
    protected virtual void CheckGround()
    {
        if (isGround || stopGroundCheck) return;

        Vector3 pos = new Vector3(transform.position.x, transform.position.y - transform.localScale.y / 2f - 0.02f, transform.position.z);
        Vector2 scale = new Vector2(transform.localScale.x, 0.005f);
        RaycastHit2D hit = Physics2D.BoxCast(pos, scale, 0, Vector2.down, 0.05f);
        if (hit.transform != null && !hit.collider.isTrigger)
            isGround = true;
    }

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
