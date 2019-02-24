using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpEntity : MonoBehaviour
{
    private Image image;
    public Sprite sprite1;
    public Sprite sprite2;
    // Start is called before the first frame update
    void Awake()
    {
        image = transform.Find("Image").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeColor(bool isFull)
    {
        if (isFull)
            image.sprite = sprite1;
        else
            image.sprite = sprite2;
    }
}
