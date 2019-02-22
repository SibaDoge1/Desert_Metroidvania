using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField]
    public bool downEnabled;
    private const float clickThreshold = 1f;
    private bool isAtObject;
    private float clickCheckTimer;
    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        isAtObject = false;
        clickCheckTimer = 0;
        downEnabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (MyInput.GetKeyDown(MyKeyCode.Down) && isAtObject && downEnabled)
        {
            if (clickCheckTimer <= 0) StartCoroutine("ClickCheck");
            else Down();
        }
    }
    
    protected void Down()
    {
        Debug.Log("down");
        StopCoroutine("ClickCheck");
        clickCheckTimer = 0;
        player.transform.Translate(Vector2.down * (transform.localScale.y+ player.transform.localScale.y/2f));
    }

    IEnumerator ClickCheck()
    {
        clickCheckTimer = clickThreshold;
        while (clickCheckTimer > 0)
        {
            yield return new WaitForEndOfFrame();
            clickCheckTimer -= Time.deltaTime;
        }
        clickCheckTimer = 0;
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.transform.CompareTag("Player"))
        {
            if (col.transform.position.y - col.transform.localScale.y / 2f >= transform.position.y + transform.localScale.y / 2f)
            {
                isAtObject = true;
                player = col.gameObject.GetComponent<Player>();
            }
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.transform.CompareTag("Player"))
        {
            isAtObject = false;
            player = null;
        }
    }
}
