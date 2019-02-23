using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Potal : InteractObject
{
    public Potal linkedPotal;
    public Stage ParentStage { get; private set; }
    public Vector2 spawnPoint { get; private set; }
    // Start is called before the first frame update
    void Awake()
    {
        ParentStage = transform.parent.parent.GetComponent<Stage>();
        spawnPoint = transform.Find("SpawnPoint").transform.position;
        myKey = MyKeyCode.Up;
    }

    protected override void Action()
    {
        ChangeStage();
    }

    public void ChangeStage()
    {
        if (linkedPotal == null)
        {
            Debug.Log("There is no linked portal, " + gameObject.name);
            return;
        }
        Map.Instance.ChangeStageRoutine(ParentStage, linkedPotal.ParentStage, linkedPotal);
    }
}
