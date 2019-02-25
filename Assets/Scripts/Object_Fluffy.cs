using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Fluffy : MonoBehaviour
{
    private float spd;
    private Vector3 Pos;
    private Vector3 dir;
    // Start is called before the first frame update
    void Start()
    {
        spd = 0.2f;
        Pos = transform.position;
        dir = Vector3.up;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > Pos.y + 0.1)
        {
            dir = Vector3.down;
        }
        else if(transform.position.y < Pos.y - 0.1)
        {
            dir = Vector3.up;
        }
        transform.Translate(dir * spd * Time.deltaTime);
    }
}
