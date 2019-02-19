using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatChanger : MonoBehaviour
{
    public int hp { get; private set; } = 0;// 기본값
    public float atk { get; private set; } = 0;
    public float spd { get; private set; } = 0;
    public float def { get; private set; } = 0;
    public float attackSpd { get; private set; } = 0;
    [SerializeField]
    public float deleteTime;
    public Character linkedChar;

    void Update()
    {
        deleteTime -= Time.deltaTime;
    }

    public void Action()
    {
        if (hp < 0)
        {
            linkedChar.GetDamage(-hp);
            destroy();
        }
        else if (hp > 0)
        {
            linkedChar.GetHeal(hp);
        }
    }

    public void construct(Character cha, int Hp, float Atk, float Spd, float Def, float AttackSpd, float Time)
    {
        hp = Hp;
        atk = Atk;
        spd = Spd;
        def = Def;
        attackSpd = AttackSpd;
        deleteTime = Time;
        linkedChar = cha;
        transform.parent = linkedChar.transform.Find("StatChangers");
        linkedChar.AddStatChanger(this);
    }

    public void destroy()
    {
        linkedChar.DeleteStatChanger(this);
        Destroy(gameObject);
    }
}
