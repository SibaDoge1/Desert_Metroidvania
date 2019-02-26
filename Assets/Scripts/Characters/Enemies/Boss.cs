using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Boss : Enemy, Respawnable //이거 상속으로 보스 만들어주셈
{
    [SerializeField]
    public int myID { get; private set; }
    [SerializeField]
    private bool isKilled;
    public string noticeStr; //죽였을 때 나오는 다이알로그 내용
    private GameObject myPalete;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
        myID = Map.Instance.bosses.IndexOf(this);
        isKilled = SaveManager.GetBossKillInfo(myID);
        if (isKilled)
        {
            DeActive();
        }
    }

    protected override IEnumerator Attack(float atk, float atkSpd, AttackInfo attackInfo)
    {
        throw new System.NotImplementedException();
    }

    protected override IEnumerator Patrol()
    {
        throw new System.NotImplementedException();
    }

    protected override IEnumerator Trace()
    {
        throw new System.NotImplementedException();
    }

    protected override void OnDieCallBack() //죽을 때
    {
        SaveManager.SetBossKillInfo(myID, true);
        NoticeUI.Instance.MakeNotice(noticeStr, 6f);
        gameObject.SetActive(false);
    }

    public virtual void Reset()
    {/*
        hp = defaultHp;
        transform.position = defaultPos;
        enemyState = defaultState;
        */
        GameObject clone = Instantiate(gameObject, transform.position, transform.rotation);
        clone.transform.SetParent(transform.parent);
        clone.SetActive(true);
        Destroy(gameObject);
    }

    public void FirstSet()
    {
        gameObject.SetActive(false);
        myPalete = Instantiate(gameObject, transform.position, transform.rotation); //나의 클론을 RespawnableObjects에 저장해둠
        myPalete.SetActive(false);
        myPalete.transform.SetParent(transform.GetComponentInParent<Stage>().transform.Find("RespawnPalete"));
        myPalete.GetComponent<Boss>().myPalete = myPalete;
        gameObject.SetActive(true);
    }

    public void DeActive()
    {
        gameObject.SetActive(false);
    }
}
