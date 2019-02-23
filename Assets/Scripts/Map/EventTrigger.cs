using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTrigger : InteractObject
{
    public enum EventType
    {
        GroundOpen, Elevate
    }
    public GameObject Obj;
    public float openTime;
    public float openThreshold;
    private bool isTriggered = false;
    private float stayTimer;
    public EventType eventType;

    protected override void Action()
    {
    }

    protected override void Update()
    {
        if (stayTimer >= openThreshold && !isTriggered)
        {
            switch (eventType)
            {
                case EventType.GroundOpen: StartCoroutine(GroundOpenRoutine(0.9f, -4f)); break;
                case EventType.Elevate: Obj.GetComponent<Potal>().ChangeStage(); break;
            }
            isTriggered = true;
        }
        else if (isAtObject)
        {
            stayTimer += Time.deltaTime;
        }
    }
    

    protected override void OnTriggerExit2D(Collider2D col)
    {
        base.OnTriggerExit2D(col);
        if (col.CompareTag("Player"))
        {
            stayTimer = 0;
        }
    }

    IEnumerator GroundOpenRoutine(float toScale, float toPosition)
    {
       // CameraManager.Instance.ShakeCam(0.2f, openTime);
        float timer = 0f;
        Vector3 defaultScale = Obj.transform.localScale;
        Vector3 defaultPos = Obj.transform.localPosition;
        Vector3 scale;
        Vector3 pos;
        while (timer < openTime)
        {
            CameraManager.Instance.MoveCam(Random.insideUnitCircle * 0.2f + (Vector2)CameraManager.Instance.transform.position);
            scale = Obj.transform.localScale;
            pos = Obj.transform.localPosition;
            scale.x = Mathf.Lerp(defaultScale.x, 0.9f, timer / openTime);
            pos.x = Mathf.Lerp(defaultPos.x, -4f, timer / openTime);
            Obj.transform.localScale = scale;
            Obj.transform.localPosition = pos;
            timer += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator ElevateRoutine()
    {
        Obj.GetComponent<Potal>().ChangeStage();
        yield return null;
    }
}
