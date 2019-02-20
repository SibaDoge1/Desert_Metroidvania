using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    private static Map instance = null;
    public static Map Instance
    {
        get { return instance; }
    }

    public Stage CurStage { get; set; }
    public List<Boss> bosses;
    public List<Stage> stages;
    public List<PotalWithLock> PotalWithLocks;
    private Image fade;

    void Awake()
    {
        if (instance == null) instance = this;
        else
        {
            Debug.LogError("Singleton Error! : " + this.name);
            Destroy(gameObject);
        }
    }

    void Start()
    {
        CurStage = PlayManager.Instance.Player.transform.parent.parent.GetComponent<Stage>();
        fade = GameObject.Find("Canvas").transform.Find("Fade").GetComponent<Image>();
        Debug.Log("Stage : " + CurStage.gameObject.name);
    }

    public void SetCurStage(string name)
    {
        CurStage = GameObject.Find(name).GetComponent<Stage>();
    }

    public void ChangeStageRoutine(Stage from, Stage to, Potal toPotal)
    {
        StartCoroutine(Fade(from, to, toPotal));
    }

    private void ChangeStage(Stage from, Stage to, Potal toPotal)
    {
        Debug.Log("Satge : " + CurStage.gameObject.name);
        to.Active();
        PlayManager.Instance.Player.transform.position = new Vector3(toPotal.transform.position.x, toPotal.transform.position.y, PlayManager.Instance.Player.transform.position.z);
        PlayManager.Instance.Player.transform.parent = to.transform.Find("Objects");
        CurStage = to;
        from.DeActive();
    }

    IEnumerator Fade(Stage from, Stage to, Potal toPotal)
    {
        Color col = fade.color;
        float time = 0f;

        while (col.a < 1f)
        {
            time += Time.deltaTime / 0.5f;
            col.a = Mathf.Lerp(0, 1, time);
            fade.color = col;
            yield return null;
        }
        ChangeStage(from, to, toPotal);
        time = 1f;
        while (col.a > 0f)
        {
            Debug.Log("d");
            time -= Time.deltaTime / 0.5f;
            col.a = Mathf.Lerp(0, 1, time);
            fade.color = col;
            yield return null;
        }
    }
}
