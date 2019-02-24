using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTrigger : InteractObject
{
    public enum EventType
    {
        GroundOpen, ChangeStage, Disappear
    }
    public GameObject Obj;
    public Key linkedKey;
    public float openTime;
    public float openThreshold;
    private bool isTriggered = false;
    private float stayTimer;
    public EventType eventType;
    public int myID { get; private set; }

    protected override void Action()
    {

    }

    void Start()
    {
        myID = Map.Instance.EventTriggers.IndexOf(this);
        isTriggered = SaveManager.GetTriggerInfo(myID);
    }

    protected override void Update()
    {
        if (stayTimer >= openThreshold && !isTriggered)
        {
            Trigger();
        }
        else if (isAtObject)
        {
            stayTimer += Time.deltaTime;
        }
    }

    public void Trigger()
    {
        if(linkedKey == null || linkedKey.transform.parent == EquipManager.Instance.transform)
        {
            switch (eventType)
            {
                case EventType.GroundOpen: StartCoroutine(GroundOpenRoutine(0.9f, -4f)); break;
                case EventType.ChangeStage: StartCoroutine(ElevateRoutine()); break;
                case EventType.Disappear: gameObject.SetActive(false); break;
            }
            isTriggered = true;
            SaveManager.SetTriggerInfo(myID, true);
        }
    }

    public void Unlock()
    {
        switch (eventType)
        {
            case EventType.GroundOpen: GroundOpen(0.9f, -4f); break;
            case EventType.ChangeStage: ElevateEvent(); break;
            case EventType.Disappear: gameObject.SetActive(false); break;
        }
        isTriggered = true;
    }

    public void ElevateEvent()
    {
        linkedKey.Obtain();
        gameObject.SetActive(false);
    }

    public void GroundOpen(float toScale, float toPosition)
    {
        Vector3 scale = Obj.transform.localScale;
        Vector3 pos = Obj.transform.localPosition;
        scale.x = toScale;
        pos.x = toPosition;
        Obj.transform.localScale = scale;
        Obj.transform.localPosition = pos;
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
        float timer = 0f;
        while (timer < 1.5f)
        {
            CameraManager.Instance.MoveCam(Random.insideUnitCircle * 0.2f + (Vector2)CameraManager.Instance.transform.position);
            timer += Time.deltaTime;
            yield return null;
        }
        gameObject.SetActive(false);
    }
}
