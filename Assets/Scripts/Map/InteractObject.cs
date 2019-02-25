using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractObject : MonoBehaviour
{
    protected bool isAtObject = false;
    public bool IsAtObject { get { return isAtObject; } }
    /// <summary>
    /// 트루면 터치만 돼도 작동, false면 키를 눌러야 작동
    /// </summary>
    public bool isTouch = false;
    protected MyKeyCode myKey = MyKeyCode.Interact;

    protected virtual void Update()
    {
        if (MyInput.GetKeyDown(myKey) && isAtObject && !isTouch)
        {
            Action();
        }
    }

    protected abstract void Action();

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.CompareTag("Player") && isTouch)
        {
            Action();
        }
    }

    protected virtual void OnTriggerStay2D(Collider2D col)
    {
        if (col.transform.CompareTag("Player"))
        {
            isAtObject = true;
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D col)
    {
        if (col.transform.CompareTag("Player"))
        {
            isAtObject = false;
        }
    }
}
