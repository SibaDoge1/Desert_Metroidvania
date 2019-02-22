using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatChanger : MonoBehaviour
{
    public StatType statType;
    public float value; // 기본값
    public bool isInfinite = false;
    public float remainTime;
    public Character linkedChar;

    void Update()
    {
        remainTime -= Time.deltaTime;
    }

    public void Action()
    {
        if (statType == StatType.HP)
        {
            if (value < 0)
            {
                linkedChar.GetDamage(-(int)value);
                DestroyMe();
            }
            else if (value > 0)
            {
                linkedChar.GetHeal((int)value);
                DestroyMe();
            }
        }
        else
        {
            linkedChar.SetStat(statType, value);
        }
        
    }

    /// <summary>
    /// Time변수가 0보다 작으면 계속 유지댐
    /// </summary>
    /// <param name="cha"></param>
    /// <param name="type"></param>
    /// <param name="_value"></param>
    /// <param name="Time"></param>
    public void Construct(Character cha, StatType type,float _value, float Time)
    {
        statType = type;
        remainTime = Time;
        value = _value;
        linkedChar = cha;
        if(Time < 0)
        {
            isInfinite = true;
        }
        transform.SetParent(linkedChar.transform.Find("StatChangers"));
        linkedChar.AddStatChanger(this);
    }

    public void DestroyMe()
    {
        linkedChar.DeleteStatChanger(this);
        Destroy(gameObject);
    }
}
