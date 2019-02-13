using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction { left = -1, right = 1, zero = 0 }

public abstract class Character : MonoBehaviour
{
    protected const float speedConst = 1;
    protected const float jumpConst = 100f;
    protected const float gravityDefault = 4f;
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
    protected float jumpPower = 6f;

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


    protected virtual void Awake()
    {
        rigid = transform.GetComponent<Rigidbody2D>();
        rigid.gravityScale = gravityDefault;
        if (rigid == null)
        Debug.Log("Noooooo");
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
                vec = Vector2.right; direction = Direction.right;  break;
            case Direction.left:
                vec = Vector2.left; direction = Direction.left; break;
            default:
                vec = Vector2.zero; break;
        }

        transform.Translate(vec * currentSpd * speedConst * Time.deltaTime);

        if (transform.position.x <= Map.Instance.CurStage.Pos.x - Map.Instance.CurStage.Size.x
        || transform.position.y <= Map.Instance.CurStage.Pos.y - Map.Instance.CurStage.Size.y
        || transform.position.x >= Map.Instance.CurStage.Pos.x + Map.Instance.CurStage.Size.x
        || transform.position.y >= Map.Instance.CurStage.Pos.y + Map.Instance.CurStage.Size.y)
            //벽 뚫는 거 방지
        {
            vec.x = -vec.x;
            vec.y = -vec.y;

            transform.Translate(vec * currentSpd * speedConst * Time.deltaTime);
        }
    }




    protected virtual void Jump() //일반적인 캐릭터의 점프, 플레이어 점프는 플레이어에 있음
    {
        rigid.velocity = Vector2.zero;
        rigid.AddForce(new Vector2(0, jumpConst * jumpPower));
        //점프 구현
    }


    public virtual void GetDamage(float damage) // 공격받을시 실행, 초기 데미지를 전달
    {
        if (isSuper <= 0f)
        {
            //임시
            Hp -= damage;
            IsSuper = 0.5f;

            Debug.Log("Damaged : " + gameObject.name);

            if (Hp <= 0)
            {
                OnDieCallBack();
            }
            //방어력 등등 데미지 연산 후 최종데미지, 0이하로 줄어들면 사망
        }

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
}
