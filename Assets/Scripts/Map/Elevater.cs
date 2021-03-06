﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevater : MonoBehaviour
{
    private bool isElevator;
    private Transform movingArea;
    private float maxY;
    private float minY;
    private BoxCollider2D _collider;
    private Player player;
    private Transform mask;
    // Start is called before the first frame update
    void Awake()
    {
        movingArea = transform.Find("MovingArea");
        maxY = movingArea.position.y + (movingArea.lossyScale.y / 2f);
        minY = movingArea.position.y - (movingArea.lossyScale.y / 2f);
        _collider = transform.Find("Collider").GetComponent<BoxCollider2D>();
        mask = transform.Find("Mask");
    }
    void OnEnable()
    {
        StartCoroutine(ElevateRoutine());
    }

    // Update is called once per frame
    void FixedUpdate()
    {/*
        if (isElevator)
        {
            player = PlayManager.Instance.Player;
            float playerPosY = collider.bounds.max.y + player.MyCollider.bounds.extents.y;
            player.transform.position = new Vector3(player.transform.position.x, playerPosY, player.transform.position.z);

        }
        */
    }

    IEnumerator ElevateRoutine()
    {
        while (true)
        {
            while (transform.position.y > minY)
            {
                Move(Vector2.down * 4f * Time.deltaTime);
                yield return new WaitForFixedUpdate();
            }
            yield return new WaitForSeconds(1f);
            while (transform.position.y < maxY)
            {
                Move(Vector2.up * 4f * Time.deltaTime);
                yield return new WaitForFixedUpdate();
            }
            yield return new WaitForSeconds(1f);
        }
    }

    public void Move(Vector2 vec)
    {
        transform.Translate(vec);
        Vector2 scale = mask.localScale;
        Vector2 pos = mask.localPosition;
        scale.y -= vec.y;
        pos.y -= 0.5f * vec.y;
        mask.localScale = scale;
        mask.localPosition = pos;
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.transform.CompareTag("Player"))
        {
            BoxCollider2D playerCol = col.transform.GetComponent<Player>().MyCollider;
            if (playerCol.bounds.min.y >= _collider.bounds.max.y)
            {
                ElevatorIn();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.transform.CompareTag("Player"))
        {
            ElevatorOut();
        }
    }

    private void ElevatorIn()
    {
        if (!isElevator)
        {/*
            isElevator = true;
            PlayManager.Instance.Player.isElevator = true;
            PlayManager.Instance.Player.SetVelocity(Vector2.zero);
            PlayManager.Instance.Player.SetGravity(0, false);
            PlayManager.Instance.Player.IsMovable = false;
            PlayManager.Instance.Player.SetStopGroundCheck(true);
            PlayManager.Instance.Player.anim.SetBool("isFalling", false);
            */
            isElevator = true;
            PlayManager.Instance.Player.isElevator = true;
            PlayManager.Instance.Player.transform.SetParent(transform);
        }
    }

    private void ElevatorOut()
    {
        if (!isElevator) return;
        /*
        isElevator = false;
        PlayManager.Instance.Player.isElevator = false;
        PlayManager.Instance.Player.IsMovable = true;
        PlayManager.Instance.Player.SetGravity(0, true);
        PlayManager.Instance.Player.SetStopGroundCheck(false);
        */
        isElevator = false;
        PlayManager.Instance.Player.isElevator = false;
        PlayManager.Instance.Player.transform.SetParent(transform.parent);
        PlayManager.Instance.Player.SetIsGround(false);
    }
    /*
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.transform.CompareTag("Player"))
        {
            if (col.transform.position.y - col.transform.localScale.y / 2f >= transform.position.y + transform.localScale.y / 2f)
            {
                PlayManager.Instance.Player.anim.SetBool("isFalling", true);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.transform.CompareTag("Player"))
        {
            isAtObject = false;
        }
    }
    */
}
