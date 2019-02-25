using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpUI : MonoBehaviour
{
    private Transform content;
    private GameObject hpEntity;
    // Start is called before the first frame update
    void Awake()
    {
        content = transform.Find("Viewport").Find("Content");
        hpEntity = Resources.Load<GameObject>("Prefabs/UI/hpEntity");
    }

    // Update is called once per frame
    void Update()
    {
        if(content.childCount != PlayManager.Instance.Player.MaxHp)
        {
            for (int i = 0; i < content.childCount; i++)
            {
                Destroy(content.GetChild(i).gameObject);
            }
            for (int i = 0; i < PlayManager.Instance.Player.MaxHp; i++)
            {
                MakeEntity();
            }
        }
        for(int i =0; i< PlayManager.Instance.Player.Hp; i++)
        {
            if (i < content.childCount)
            {
                content.GetChild(i).GetComponent<HpEntity>().ChangeColor(true);
            }
        }
        for (int i = PlayManager.Instance.Player.Hp; i < PlayManager.Instance.Player.MaxHp; i++)
        {
            if (i < content.childCount)
            {
                content.GetChild(i).GetComponent<HpEntity>().ChangeColor(false);
            }
        }
    }

    public void MakeEntity()
    {
        GameObject obj = Instantiate(hpEntity, transform.position, Quaternion.identity);
        obj.transform.SetParent(content);
    }

}
