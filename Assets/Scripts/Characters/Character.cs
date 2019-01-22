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
    protected float hp = 1; //default value
    [SerializeField]
    protected int spd = 1;
    [SerializeField]
    protected int def = 0;
    [SerializeField]
    protected float maxHp = 10;

    public float Hp { get { return hp; } set { hp = value; } }
    public int Spd { get { return spd; } set { spd = value; } }
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
        transform.Translate(vec * spd * speedConst * Time.deltaTime);
    }

    protected void Jump()
    {
        rigid.velocity = new Vector2(0, 5);
        //점프 구현
    }

    public virtual void GetDamage(float damage) // 공격받을시 실행, 초기 데미지를 전달
    {
        //방어력 등등 데미지 연산 후 최종데미지, 0이하로 줄어들면 사망
    }

    public virtual void OnDieCallBack() //죽을 때 부르는 함수
    {

    }
}
