using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StableFallHelper : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.transform.CompareTag("Player"))
        {
            Debug.Log("a");
            if (col.transform.position.y - col.transform.localScale.y / 2f >= transform.position.y + transform.localScale.y / 2f)
            {
                Debug.Log("b");
                PlayManager.Instance.Player.anim.SetBool("isFalling", false);
            }
        }
    }
}
