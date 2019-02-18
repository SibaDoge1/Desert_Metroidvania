﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : InteractObject
{
    private bool isUsingLadder;

    [SerializeField]
    private const float climbSpeed = 0.125f;

    // Start is called before the first frame update
    void Start()
    {
        isUsingLadder = false;
    }

    protected override void Update()
    {
        if (isAtObject)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                LadderIn(true);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                if (PlayManager.Instance.Player.transform.position.y >= transform.position.y + transform.localScale.y / 2f)
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

    protected override void Action()
    {

    }
    private void UpLadder()
    {
        if (!isUsingLadder) return;
        if (PlayManager.Instance.Player.transform.position.y <= transform.position.y + transform.localScale.y / 2f + 0.1f)
        {
            PlayManager.Instance.Player.transform.Translate(Vector2.up * climbSpeed);
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
            PlayManager.Instance.Player.SetTrigger(true);
            PlayManager.Instance.Player.SetVelocity(Vector2.zero);
            PlayManager.Instance.Player.SetGravity(0, false);
            PlayManager.Instance.Player.IsMovable = false;
            if (isUp) PlayManager.Instance.Player.transform.Translate(new Vector2(transform.position.x - PlayManager.Instance.Player.transform.position.x, 0.1f));
            else PlayManager.Instance.Player.transform.Translate(new Vector2(transform.position.x - PlayManager.Instance.Player.transform.position.x, -0.2f));
            isUsingLadder = true;
        }
    }

    private void DownLadder()
    {
        if (!isUsingLadder) return;

        if (PlayManager.Instance.Player.transform.position.y < transform.position.y - transform.localScale.y / 2f)
        {
            LadderOut();
        }
        else
        {
            PlayManager.Instance.Player.transform.Translate(Vector2.down * climbSpeed);
        }
    }

    private void LadderOut()
    {
        if (!isUsingLadder) return;
        isUsingLadder = false;
        PlayManager.Instance.Player.IsMovable = true;
        PlayManager.Instance.Player.SetTrigger(false);
        PlayManager.Instance.Player.SetGravity(0, true);
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
        
        PlayManager.Instance.Player.transform.Translate(vec * (transform.localScale.x/2f + 0.1f));
        PlayManager.Instance.Player.JumpCount = 0;
        LadderOut();
    }
}
