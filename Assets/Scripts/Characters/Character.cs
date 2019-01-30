using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction { left, right, zero }

public abstract class Character : MonoBehaviour
{
    const float speedConst = 1;
    protected Rigidbody2D rigid;

    #region Status //이거 더블클릭 하셈
    [Header("Status")]
    [SerializeField]
    protected float hp = 1; //기본값
    [SerializeField]
    protected float spd = 1f;
    [SerializeField]
    protected int def = 0;
    [SerializeField]
    protected float maxHp = 10;

    [SerializeField]
    protected float currentHp = 1; //현재값(기본값에서 버프 / 디버프 등으로 바뀐 상태, 즉, 실제 게임 상에 적용되는 값)
    [SerializeField]
    protected float currentSpd = 1f;
    [SerializeField]
    protected int currentDef = 0;
    [SerializeField]
    protected float currentMaxHp = 10;
    //자신의 버프, 디버프 상태에만 영향을 받으므로 프로퍼티는 만들지 않았음

    public float Hp { get { return hp; } set { hp = value; } }
    public float Spd { get { return spd; } set { spd = value; } }
    public int Def { get { return def; } set { Def = value; } }
    #endregion

    bool isGround;

    void Awake()
    {
        rigid = transform.GetComponent<Rigidbody2D>();
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

    float jumpCount = 0f;

    protected void JumpAccept()
    {
        if (jumpCount > 0f && rigid.velocity.y == 0f)
            jumpCount = 0f;
    }

    protected void Jump()       //점프 높이를 점프 조작키 누른 시간과 비례하도록
    {
        if (jumpCount < 0.25f)
        {
            jumpCount += Time.deltaTime;
            rigid.velocity = new Vector2(rigid.velocity.x, 5f);
        }
        //점프 구현
    }

    protected void JumpStop()
    {
        jumpCount = 1f;
    }

    public virtual void GetDamage(float damage) // 공격받을시 실행, 초기 데미지를 전달
    {
        //방어력 등등 데미지 연산 후 최종데미지, 0이하로 줄어들면 사망
    }

    public virtual void OnDieCallBack() //죽을 때 부르는 함수
    {

    }

    protected void CheckBuffAndDebuff() //기본값과 버프 디버프 상태를 종합하여 실제 게임에 적용되는 현재 스텟값을 정함
    {
        currentHp = Hp;
        currentDef = Def;
        currentMaxHp = maxHp;
        currentSpd = Spd;

        if (rigid.velocity.y != 0f) //공중에서 횡이동 속도 0.5배
        {
            currentSpd *= 0.5f;
        }
    }
}
