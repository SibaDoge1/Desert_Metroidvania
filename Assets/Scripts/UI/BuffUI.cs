﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffUI : MonoBehaviour
{
    private static BuffUI instance;
    public static BuffUI Instance
    {
        get { return instance; }
    }
    private GameObject buffEntity;
    private Transform content;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;

        }
        else
        {
            Debug.LogWarning("Singleton Error! : " + this.name);
            Destroy(this);
        }
        buffEntity = Resources.Load<GameObject>("Prefabs/UI/BuffEntity");
        content = transform.Find("Viewport").Find("Content");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MakeEntity(StatType type, float value, float time)
    {
        GameObject obj = Instantiate(buffEntity, transform.position, Quaternion.identity, content);
        obj.transform.SetParent(content);
        obj.GetComponent<BuffEntity>().Construct(type, value, time);
    }
}
