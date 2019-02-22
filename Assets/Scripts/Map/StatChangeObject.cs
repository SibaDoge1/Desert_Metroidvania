﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum StatType
{
    HP, Damage, SPD, DEF, AttackSPD, MaxHP
}
public class StatChangeObject : InteractObject
{

    public StatType type;
    public int value = 0; // 기본값
    public float buffTime = 0;
    private GameObject prefab;
    public int remainCount = 1; //음수면
    public bool isInfinite;

    // Start is called before the first frame update
    void Awake()
    {
        prefab = Resources.Load("Prefabs/StatChanger") as GameObject;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void Action()
    {
        if (remainCount > 0 || isInfinite)
        {
            if (value == 0) return;
            Debug.Log("stat change");
            GameObject obj;
            obj = Instantiate(prefab, transform.position, transform.rotation);
            obj.GetComponent<StatChanger>().Construct(PlayManager.Instance.Player, type, value, buffTime);
            remainCount--;
            if(value > 0)
            {
                NoticeUI.Instance.MakeNotice("버프를 받습니다: " + type.ToString() + value + " 증가", 3f);
            }
            if (value < 0)
            {
                NoticeUI.Instance.MakeNotice("너프를 받습니다: " + type.ToString() + value + " 감소", 3f);
            }
        }
    }
}
