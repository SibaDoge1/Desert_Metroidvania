using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoticeUI : MonoBehaviour
{
    private static NoticeUI instance;
    public static NoticeUI Instance
    {
        get { return instance; }
    }
    private GameObject noticeEntity;
    private Transform content;
    private ScrollRect myScrollRect;
    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
        {
            instance = this;

        }
        else
        {
            Debug.LogWarning("Singleton Error! : " + this.name);
            Destroy(this);
        }
        noticeEntity = Resources.Load<GameObject>("Prefabs/UI/NoticeEntity");
        content = transform.Find("Viewport").Find("Content");
        myScrollRect = transform.GetComponent<ScrollRect>();
    }

    void Update()
    {
        myScrollRect.verticalNormalizedPosition = 1f;
    }
    /// <summary>
    /// time이 0보다 작으면 계속 존재
    /// </summary>
    /// <param name="str"></param>
    /// <param name="time"></param>
    public void MakeNotice(string str, float time)
    {
        if (str.Length == 0) return;
        GameObject obj = Instantiate(noticeEntity, transform.position, Quaternion.identity);
        obj.transform.SetParent(content);
        obj.GetComponent<Text>().text = str;
        obj.GetComponent<NoticeEntity>().Active(time);
    }

    public void DeleteNotice(GameObject notice)
    {
        for(int i = 0; i < content.childCount; i++)
        {
            if(content.GetChild(i) == notice)
            {
                Destroy(content.GetChild(i));
            }
        }
    }
}
