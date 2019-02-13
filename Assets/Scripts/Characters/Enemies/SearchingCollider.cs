using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchingCollider : MonoBehaviour
{
    Enemy parentEnemy;

    // Start is called before the first frame update
    void Start()
    {
        parentEnemy = transform.parent.GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.tag == "Player")
            parentEnemy.OnTriggerEnterSearch();
    }

    private void OnTriggerExit2D(Collider2D c)
    {
        if (c.tag == "Player")
            parentEnemy.OnTriggerExitSearch();
    }
}
