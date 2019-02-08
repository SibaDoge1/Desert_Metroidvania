using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potal : MonoBehaviour
{
    public Potal linkedPotal;
    public Stage ParentStage { get; private set; }
    private bool isAtPortal = false;
    // Start is called before the first frame update
    void Awake()
    {
        ParentStage = transform.parent.parent.GetComponent<Stage>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && isAtPortal)
        {
            StartCoroutine("Fade");
        }
    }

    public void ChangeStage()
    {
        if (linkedPotal == null)
        {
            Debug.Log("There is no linked portal, " + gameObject.name);
            return;
        }
        linkedPotal.ParentStage.Active();
        Player.Instance.transform.position = new Vector3(linkedPotal.transform.position.x, linkedPotal.transform.position.y, Player.Instance.transform.position.z);
        Player.Instance.transform.parent = linkedPotal.ParentStage.transform.Find("Objects");
        Map.Instance.changeStage(ParentStage, linkedPotal.ParentStage);
        ParentStage.DeActive();
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.transform.CompareTag("Player"))
        {
            isAtPortal = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.transform.CompareTag("Player"))
        {
            isAtPortal = false;
        }
    }

    IEnumerator Fade()
    {
        yield return new WaitForSeconds(0.1f);
        ChangeStage();
    }
}
