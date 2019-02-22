using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideObject : MonoBehaviour
{
    public MyKeyCode code;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected  virtual void Update()
    {
        if (MyInput.GetKeyDown(code))
        {
            Destroy(gameObject);
        }
    }
}
