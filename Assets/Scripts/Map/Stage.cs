using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    private Transform boundary;

    [SerializeField]
    private Vector2 size;
    [SerializeField]
    private Vector2 pos;

    public Vector2 Size { get { return size; } }
    public Vector2 Pos { get { return pos; } }

    void Awake()
    {
        boundary = transform.Find("Boundary");
        size.x = boundary.localScale.x / 2f;
        size.y = boundary.localScale.y / 2f;
        pos.x = transform.position.x;
        pos.y = transform.position.y;
    }

    void Start()
    {
        if (Map.Instance.CurStage != this) gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    public void DeActive()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    public void Active()
    {
        gameObject.SetActive(true);
    }

    public void ResetStage()
    {

    }
}
