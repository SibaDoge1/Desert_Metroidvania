using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffEntity : MonoBehaviour
{
    private Text text;
    private Image fill;
    private Slider slider;
    private float remainTime;
    private float buffTime;
    private bool isInfinite = false;

    // Start is called before the first frame update
    void Awake()
    {
        slider = transform.Find("Slider").GetComponent<Slider>();
        fill = transform.Find("Slider").Find("Fill Area").Find("Fill").GetComponent<Image>();
        text = transform.Find("Text").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Construct(StatType type, float value, float time)
    {
        buffTime = time;
        if (time < 0)
        {
            buffTime = 1f;
            isInfinite = true;
        }
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
        if (value > 0)
        {
            text.text = typeStr + "\n" + value + "+";
            fill.color = Color.green;
            text.color = Color.black;
        }
        else if (value < 0)
        {
            text.text = typeStr + "\n" + value + "-";
            fill.color = Color.red;
            text.color = Color.white;
        }
        else
            return;
        StartCoroutine(updateRoutine());
    }

    IEnumerator updateRoutine()
    {
        remainTime = buffTime;
        while (remainTime > 0)
        {
            slider.value = remainTime / buffTime;
            if(!isInfinite)
                remainTime -= Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }
}
