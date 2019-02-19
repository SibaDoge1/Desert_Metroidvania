using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Potal : InteractObject
{
    public Potal linkedPotal;
    public Stage ParentStage { get; private set; }
    // Start is called before the first frame update
    void Awake()
    {
        ParentStage = transform.parent.parent.GetComponent<Stage>();
        myKey = KeyCode.UpArrow;
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
