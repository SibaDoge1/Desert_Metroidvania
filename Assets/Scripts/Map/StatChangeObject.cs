using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum StatType
{
    HP, Damage, SPD, DEF, AttackSPD, MaxHP, Trap, MaxHeal
}
public class StatChangeObject : InteractObject, Respawnable
{

    public StatType type;
    public float value = 1; // 기본값
    public float buffTime = 0;
    private GameObject prefab;
    public int remainCount = 1;
    public bool isInfinite;
    private int DefaultRemainCount = 1;

    // Start is called before the first frame update
    void Awake()
    {
        prefab = Resources.Load("Prefabs/StatChanger") as GameObject;
        DefaultRemainCount = remainCount;
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
            SoundDelegate.Instance.PlayEffectSound(EffectSoundType.Buff);
            if (value == 0) return;
            Debug.Log("stat change");
            GameObject obj;
            obj = Instantiate(prefab, transform.position, transform.rotation);
            obj.GetComponent<StatChanger>().Construct(PlayManager.Instance.Player, type, value, buffTime);
            remainCount--;
            if(remainCount <= 0 && !isInfinite)
            {
                gameObject.SetActive(false);
            }
            /*
            string typeStr;
            switch (type)
            {
                case StatType.AttackSPD: typeStr = "공격속도"; break;
                case StatType.HP: typeStr = "체력"; break;
                case StatType.MaxHP: typeStr = "최대체력"; break;
                case StatType.SPD: typeStr = "이동속도"; break;
                case StatType.Damage: typeStr = "공격력"; break;
                case StatType.DEF: typeStr = "방어력"; break;
                default: typeStr = ""; break;
            }
            if(value > 0)
            {
               NoticeUI.Instance.MakeNotice("버프를 받습니다\n" + typeStr + " " + value + " 증가", 3f);
            }
            if (value < 0)
            {
               NoticeUI.Instance.MakeNotice("너프를 받습니다\n" + typeStr + " " + value + " 감소", 3f);
            }
            */
        }
    }

    public void Reset()
    {
        remainCount = DefaultRemainCount;
    }

    public void FirstSet()
    {
    }
}
