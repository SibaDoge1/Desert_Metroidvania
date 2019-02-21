using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideObject : MonoBehaviour
{

    public enum keyEnum
    {
        right = KeyCode.RightArrow,
        left = KeyCode.LeftArrow,
        up = KeyCode.UpArrow,
        down = KeyCode.DownArrow,
        shift = KeyCode.LeftShift,
        space = KeyCode.Space,
        A = KeyCode.A,
        S = KeyCode.S
    };
    public keyEnum code;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown((KeyCode)code))
        {
            Destroy(gameObject);
        }
    }
}
