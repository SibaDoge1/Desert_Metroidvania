using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    private static Map instance = null;
    public static Map Instance
    {
        get { return instance; }
    }

    private Transform boundary;

    [SerializeField]
    private float mapX;
    [SerializeField]
    private float mapY;

    public float MapX { get { return mapX; } }
    public float MapY { get { return mapY; } }

    void Awake()
    {
        if (instance == null) instance = this;
        else
        {
            Debug.LogError("Singleton Error! : " + this.name);
            Destroy(gameObject);
        }
        boundary = transform.Find("Boundary");
        mapX = boundary.localScale.x/2f;
        mapY = boundary.localScale.y/2f;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
