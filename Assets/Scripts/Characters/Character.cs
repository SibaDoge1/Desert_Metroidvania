using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction { left, right, zero }

public abstract class Character : MonoBehaviour
{
    const float speedConst = 1;
    protected Rigidbody2D rigid;

    #region 스테이터스
    [Header("Status")]
    [SerializeField] //에디터상으로 수정가능하게 함
    private float Hp;
    [SerializeField]
    private int Spd;
    [SerializeField]
    private int Def;
    [SerializeField]
    private float maxHp;

    public float hp { get { return Hp; } set { Hp = value; } } //c#의 프로퍼티 기능, 접근제한자가 있는것들의 get set을 간편히 만듬
    public int spd { get { return Spd; } set { Spd = value; } }
    public int def { get { return Def; } set { Def = value; } }
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
        transform.Translate(vec * spd * speedConst * Time.deltaTime);
    }

    protected void Jump()
    {
        //점프 구현
    }

    public virtual void GetDamage(float damage) // 순수한 기본 데미지를 전달
    {
        //방어력 등등 데미지 연산 후 줄이기, 0이하로 줄어들면 사망
    }

    public virtual void OnDieCallBack() //죽을 때 부르는 함수
    {

    }
}
