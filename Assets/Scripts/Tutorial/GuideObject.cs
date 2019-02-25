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
        if (MyInput.GetKey(code))
        {
            Vector3 targetScreenPos = Camera.main.WorldToViewportPoint(transform.position);
            if (targetScreenPos.x >= 0f && targetScreenPos.x <= 1f && targetScreenPos.y >= 0f && targetScreenPos.y <= 1f)
            {
                Trigger();
            }
        }
    }

    protected virtual void Trigger()
    {
        Destroy(gameObject);
    }
}
