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
        if (value > 0)
        {
            text.text = type.ToString() + "\n" + value + "+";
            fill.color = Color.green;
            text.color = Color.black;
        }
        else if (value < 0)
        {
            text.text = type.ToString() + "\n" + value + "-";
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
            remainTime -= Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }
}
