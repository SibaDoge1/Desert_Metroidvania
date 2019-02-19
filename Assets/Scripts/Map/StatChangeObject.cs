﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatChangeObject : InteractObject
{
    [SerializeField]
    private int hp = 0; // 기본값
    [SerializeField]
    private float atk = 0;
    [SerializeField]
    private float spd = 0;
    [SerializeField]
    private float def = 0;
    [SerializeField]
    private float attackSpd = 0;
    [SerializeField]
    private float buffTime = 0;
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
        Debug.Log("act");
        if (remainCount > 0 || isInfinite)
        {
            GameObject obj;
            obj = Instantiate(prefab, transform.position, transform.rotation);
            obj.GetComponent<StatChanger>().construct(PlayManager.Instance.Player, hp, atk, spd, def, attackSpd, buffTime);
            remainCount--;
        }
    }
}
