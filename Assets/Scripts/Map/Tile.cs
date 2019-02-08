using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField]
    private bool downEnabled;
    private const float clickThreshold = 1f;
    private bool isAtTile;
    private float clickCheckTimer;
    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        isAtTile = false;
        clickCheckTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) && isAtTile && downEnabled)
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
                isAtTile = true;
                player = col.gameObject.GetComponent<Player>();
            }
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.transform.CompareTag("Player"))
        {
            isAtTile = false;
            player = null;
        }
    }
}
