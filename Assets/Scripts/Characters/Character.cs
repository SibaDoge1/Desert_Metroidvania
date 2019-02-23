using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction { left = -1, right = 1, zero = 0 }

public abstract class Character : InGameObj
{
    protected const float speedConst = 1;
    protected const float jumpConst = 100f;
    protected const float gravityDefault = 4f;
    protected const float InvincibleTIme = 0.5f;
    protected Rigidbody2D rigid;
    protected GameObject sprite;


    #region Status //이거 더블클릭 하셈
    [Header("Status")]
    [SerializeField]
    protected float defaultSpd = 1f;
    [SerializeField]
    protected float defaultDef = 0;
    [SerializeField]
    protected float jumpPower = 6f;
    [SerializeField]
    protected int defaultMaxHp = 10;

    [SerializeField]
    protected int hp = 1; // 기본값
    [SerializeField]
    protected int maxHp = 10;
    [SerializeField]
    protected float spd = 1f;
    [SerializeField]
    protected float def = 0;
    [SerializeField]
    protected float attackSpd = 0; //1이 정상속도, 곱연산
    [SerializeField]
    protected float atkBuff = 0; //공격력 버프를 위해 쓰임
    [SerializeField]
    protected List<StatChanger> StatChangers;

    //자신의 버프, 디버프 상태에만 영향을 받으므로 프로퍼티는 만들지 않았음

    protected float isSuper = 0f; //무적
    public float IsSuper { get { return isSuper; } set { isSuper = value; } }

    protected Direction direction = Direction.right;
    public Direction Direction { get { return direction; } set { direction = value; } }

    public int Hp { get { return hp; } set { hp = value; } }
    public float Spd { get { return defaultSpd; } set { defaultSpd = value; } }
    public float Def { get { return defaultDef; } set { Def = value; } }
    public float AttackSpd { get { return attackSpd; } set { attackSpd = value; } }
    #endregion


    protected virtual void Awake()
    {
        rigid = transform.GetComponent<Rigidbody2D>();
        rigid.gravityScale = gravityDefault;
        StatChangers = new List<StatChanger>();
        if (transform.Find("Sprite") != null)
            sprite = transform.Find("Sprite").gameObject;
        if (rigid == null)
        Debug.Log("Noooooo");
    }

    protected virtual void Start()
    {
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
                vec = Vector2.right; direction = Direction.right;
                if (sprite != null)
                    sprite.GetComponent<SpriteRenderer>().flipX = false;
                break;
            case Direction.left:
                vec = Vector2.left; direction = Direction.left;
                if (sprite != null)
                    sprite.GetComponent<SpriteRenderer>().flipX = true;
                break;
            default:
                vec = Vector2.zero; break;
        }

        transform.Translate(vec * spd * speedConst * Time.deltaTime);

        if (Map.Instance.CheckOutSide(transform.position))
            //벽 뚫는 거 방지
        {
            vec.x = -vec.x;
            vec.y = -vec.y;

            transform.Translate(vec * spd * speedConst * Time.deltaTime);
        }
    }

    protected void Move(Vector2 vec)
    {
        Direction dir = vec.x >= 0 ? Direction.right : Direction.left;

        switch (dir)
        {
            case Direction.right:
                if (sprite != null)
                    sprite.GetComponent<SpriteRenderer>().flipX = false;
                break;
            case Direction.left:
                if (sprite != null)
                    sprite.GetComponent<SpriteRenderer>().flipX = true;
                break;
            default:
                break;
        }

        transform.Translate(vec * spd * speedConst * Time.deltaTime);

        if (Map.Instance.CheckOutSide(transform.position))
        //벽 뚫는 거 방지
        {
            vec.x = -vec.x;
            vec.y = -vec.y;

            transform.Translate(vec * spd * speedConst * Time.deltaTime);
        }
    }


    public virtual void Jump() //일반적인 캐릭터의 점프, 플레이어 점프는 플레이어에 있음
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
            Hp -= (int)damage;
            IsSuper = InvincibleTIme;

            Debug.Log("Damaged : " + gameObject.name);

            if (Hp <= 0)
            {
                OnDieCallBack();
            }
            //방어력 등등 데미지 연산 후 최종데미지, 0이하로 줄어들면 사망
        }

    }

    public virtual void GetHeal(int heal)
    {
        Hp += heal;
        Debug.Log("Healed : " + gameObject.name);
        if (Hp > maxHp)
            Hp = maxHp;
    }

    protected virtual void OnDieCallBack() //죽을 때 부르는 함수
    {
        //임시
        Destroy(gameObject);
    }

    public void SetStat(StatType type, float value)
    {
        switch (type)
        {
            case StatType.DEF: def += value; break;
            case StatType.SPD: spd += value; break;
            case StatType.AttackSPD: attackSpd += value; break;
            case StatType.Damage: atkBuff += value; break;
            case StatType.MaxHP: maxHp += (int)value;  break;
            case StatType.HP: break;
        }
    }

    protected virtual void CheckBuffAndDebuff() //기본값과 버프 디버프 상태를 종합하여 실제 게임에 적용되는 현재 스텟값을 정함
    {
        def = defaultDef;
        spd = defaultSpd;
        maxHp = defaultMaxHp;
        attackSpd = 1;
        atkBuff = 0;
        for (int i = 0; i< StatChangers.Count; i++)
        {
            if(!StatChangers[i].isInfinite && StatChangers[i].remainTime <= 0)
            {
                StatChangers[i].DestroyMe();
                continue;
            }
            StatChangers[i].Action();
        }

    }

    public virtual void AddStatChanger(StatChanger stat)
    {
        StatChangers.Add(stat);
    }
    public virtual void DeleteStatChanger(StatChanger stat) 
    {
        StatChangers.Remove(stat);
    }
}
