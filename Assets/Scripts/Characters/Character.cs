using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction { left, right, zero }

public abstract class Character : MonoBehaviour
{
    protected const float speedConst = 1;
    protected const float jumpConst = 100f;
    protected Rigidbody2D rigid;

    #region Status //이거 더블클릭 하셈
    [Header("Status")]
    [SerializeField]
    protected float hp = 1; // 기본값
    [SerializeField]
    protected float spd = 1f;
    [SerializeField]
    protected int def = 0;
    [SerializeField]
    protected float maxHp = 10;
    [SerializeField]
    protected float jumpPower = 2f;

    [SerializeField]
    protected float currentSpd = 1f;
    [SerializeField]
    protected int currentDef = 0;
    //자신의 버프, 디버프 상태에만 영향을 받으므로 프로퍼티는 만들지 않았음

    protected float isSuper = 0f; //무적
    public float IsSuper { get { return isSuper; } set { isSuper = value; } }

    public float Hp { get { return hp; } set { hp = value; } }
    public float Spd { get { return spd; } set { spd = value; } }
    public int Def { get { return def; } set { Def = value; } }
    #endregion

    protected bool isGround;
    public bool IsGround { get { return isGround; }}

    void Awake()
    {
        rigid = transform.GetComponent<Rigidbody2D>();
    }
    
    protected virtual void Update()
    {
        if (IsSuper > 0f)
            IsSuper -= Time.deltaTime;

        CheckBuffAndDebuff();
        CheckGround();
    }

    protected virtual void FixedUpdate()
    {
    }

    protected void Move(Direction dir)
    {
        Vector2 vec;
        switch (dir)
        {
            case Direction.right:
                vec = Vector2.right; break;
            case Direction.left:
                vec = Vector2.left; break;
            default:
                vec = Vector2.zero; break;
        }
        transform.Translate(vec * currentSpd * speedConst * Time.deltaTime);
    }


    /*
    float jumpCount = 0f;

    protected void JumpAccept() // 기획서 상 점프는 일정함, 바뀔수도 있으니 일단 보존
    {
        if (jumpCount > 0f && rigid.velocity.y == 0f)
            jumpCount = 0f;
    }

    protected void Jump()       
    {
        if (jumpCount < 0.25f)
        {
            jumpCount += Time.deltaTime;
            rigid.velocity = new Vector2(rigid.velocity.x, 10f);
        }
        //점프 구현
    }

    protected void JumpStop()
    {
        jumpCount = 1f;
    }
    */

    protected virtual void Jump()
    {
        if (isGround)
        {
            rigid.AddForce(new Vector2(0, jumpConst * jumpPower));
            isGround = false;
        }
        //점프 구현
    }


    public virtual void GetDamage(float damage) // 공격받을시 실행, 초기 데미지를 전달
    {
        //임시
        Hp -= damage;
        IsSuper = 1f;

        if (Hp <= 0)
        {
            OnDieCallBack();
        }
        //방어력 등등 데미지 연산 후 최종데미지, 0이하로 줄어들면 사망
    }

    protected virtual void OnDieCallBack() //죽을 때 부르는 함수
    {
        //임시
        Destroy(gameObject);
    }

    protected virtual void CheckBuffAndDebuff() //기본값과 버프 디버프 상태를 종합하여 실제 게임에 적용되는 현재 스텟값을 정함
    {
        currentDef = Def;
        currentSpd = Spd;
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
			Gizmos.DrawRay(pos, Vector3.down *5f);
    }

    protected virtual void CheckGround()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y - transform.localScale.y / 2f - 0.02f, transform.position.z);
        Vector2 scale = new Vector2(transform.localScale.x, 0.005f);
        RaycastHit2D hit = Physics2D.BoxCast(pos, scale, 0, Vector2.down, 0.05f);
        if (hit.transform != null && !hit.collider.isTrigger)
            isGround = true;
        else
            isGround = false;
    }

}
