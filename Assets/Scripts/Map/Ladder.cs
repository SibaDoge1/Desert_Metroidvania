using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    private bool isUsingLadder;
    private bool isAtLadder = false;
    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        isUsingLadder = false;
    }

    void Update()
    {
        if (isAtLadder)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                LadderIn(true);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                if (player.transform.position.y - player.transform.localScale.y / 2f >= transform.position.y + transform.localScale.y / 2f)
                {
                    LadderIn(false);
                }
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isUsingLadder)
        {
            if (Input.GetKey(KeyCode.RightArrow)) Move(Direction.right);
            if (Input.GetKey(KeyCode.LeftArrow)) Move(Direction.left);
            if (Input.GetKey(KeyCode.UpArrow)) UpLadder();
            if (Input.GetKey(KeyCode.DownArrow)) DownLadder();
        }
    }

    [SerializeField]
    private const float climbSpeed = 0.125f;
    private void UpLadder()
    {
        if (!isUsingLadder) return;
        if (player.transform.position.y <= transform.position.y + transform.localScale.y / 2f + 0.1f)
        {
            player.transform.Translate(Vector2.up * climbSpeed);
        }
        else
        {
            LadderOut();
        }
    }

    private void LadderIn(bool isUp)
    {
        if (!isUsingLadder)
        {
            player.SetTrigger(true);
            player.SetVelocity(Vector2.zero);
            player.SetGravity(0, false);
            if (isUp) player.transform.Translate(new Vector2(transform.position.x - player.transform.position.x, 0.1f));
            else player.transform.Translate(new Vector2(transform.position.x - player.transform.position.x, -0.2f));
            isUsingLadder = true;
        }
    }

    private void DownLadder()
    {
        if (!isUsingLadder) return;

        if (player.transform.position.y < transform.position.y - transform.localScale.y / 2f)
        {
            LadderOut();
        }
        else
        {
            player.transform.Translate(Vector2.down * climbSpeed);
        }
    }

    private void LadderOut()
    {
        if (!isUsingLadder) return;
        Debug.Log("ladder out");
        isUsingLadder = false;
        player.IsMovable = true;
        player.SetTrigger(false);
        player.SetGravity(0, true);
    }

    protected void Move(Direction dir)
    {
        Vector2 vec;
        switch (dir)
        {
            case Direction.right:
                vec = Vector2.right; break;
            case Direction.left:
                vec = Vector2.left; break;
            default:
                vec = Vector2.zero; break;
        }
        
        player.transform.Translate(vec * (transform.localScale.x/2f + 0.1f));
        player.JumpCount = 0;
        LadderOut();
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.transform.CompareTag("Player"))
        {
            isAtLadder = true;
            player = col.GetComponent<Player>();
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.transform.CompareTag("Player"))
        {
            isAtLadder = false;
        }
    }
}
