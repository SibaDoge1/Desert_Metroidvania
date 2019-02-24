using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpEntity : MonoBehaviour
{
    private Image image;
    // Start is called before the first frame update
    void Awake()
    {
        image = transform.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeColor(Color col)
    {
        image.color = col;
    }
}
