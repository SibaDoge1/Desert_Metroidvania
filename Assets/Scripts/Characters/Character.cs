using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction { left = -1, right = 1, zero = 0 }

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

    public Direction direction = Direction.right;

    public float Hp { get { return hp; } set { hp = value; } }
    public float Spd { get { return spd; } set { spd = value; } }
    public int Def { get { return def; } set { Def = value; } }
    #endregion


    void Awake()
    {
        rigid = transform.GetComponent<Rigidbody2D>();
        rigid.gravityScale = 3f;
    }
    
    protected virtual void Update()
    {
        if (IsSuper > 0f)
            IsSuper -= Time.deltaTime;

        CheckBuffAndDebuff();
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

    
    //float jumpCount = 0f;

    protected void JumpAccept() // 기획서 상 점프는 일정함, 바뀔수도 있으니 일단 보존
    {
        if (jumpCount > 0 && rigid.velocity.y == 0f)
            jumpCount = 0;
    }

    int jumpCount = 0;

    protected void Jump()       //주석 처리 : 점프 높이를 점프 조작키 누른 시간과 비례하도록
    {
        if (jumpCount == 0)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, 15f);
            jumpCount++;
        }
        else if (jumpCount == 1 && EquipManager.Instance.equipedWeapon.gameObject.name == "Sword")
            //양손 검 이단 점프
        {
            rigid.velocity = new Vector2(rigid.velocity.x, 15f);
            jumpCount++;
        }
        /*
        if (jumpCount < 0.25f)
        {
            jumpCount += Time.deltaTime;
        }
        */
        //점프 구현
    }

    /*
    protected void JumpStop()
    {
        jumpCount = 1f;
    }
    */

    protected virtual void Jump()
    {
        rigid.velocity = Vector2.zero;
        rigid.AddForce(new Vector2(0, jumpConst * jumpPower));
        //점프 구현
    }


    public virtual void GetDamage(float damage) // 공격받을시 실행, 초기 데미지를 전달
    {
        //임시
        Hp -= damage;
        IsSuper = 1f;

        Debug.Log("Damaged : " + gameObject.name);

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

        if (rigid.velocity.y != 0f) //공중에서 횡이동 속도 0.5배
        {
            currentSpd *= 0.5f;
        }
        else
        {
            if (gameObject.name == "Player" && EquipManager.Instance.equipedWeapon.gameObject.name == "Sword" && Input.GetKey(KeyCode.Space))
                //양손검 상태 플레이어가 Space 입력 시, 대쉬
            {
                currentSpd *= 1.3f;
            }

        }
    }
}
