using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapViewer : MonoBehaviour
{
    private bool isViewing;
    private GameObject indicator;
    private Coroutine routine;

    // Start is called before the first frame update
    void Awake()
    {
        indicator = PlayManager.Instance.Player.transform.Find("MapIndicator").gameObject;
    }

    public void Toggle()
    {
        SoundDelegate.Instance.PlayEffectSound(EffectSoundType.Button);

        if (!isViewing)
        {
            On();
        }
        else
        {
            Off();
        }
    }

    public void On()
    {
        gameObject.SetActive(true);
        isViewing = true;
        indicator.SetActive(true);
        for(int i =0; i<Map.Instance.stages.Count; i++)
        {
            if(SaveManager.GetMapInfo(Map.Instance.stages[i].myID) == true)
            {
                Map.Instance.stages[i].gameObject.SetActive(true);
            }
            else
                Map.Instance.stages[i].gameObject.SetActive(false);
        }
        Time.timeScale = 0f;
        //routine = StartCoroutine(MapViewerRoutine());
    }

    public void Off()
    {
        isViewing = false;
        Time.timeScale = 1f;
        //StopCoroutine(routine);
        for (int i = 0; i < Map.Instance.stages.Count; i++)
        {
            if (Map.Instance.CurStage == Map.Instance.stages[i])
            {
                Map.Instance.stages[i].gameObject.SetActive(true);
            }
            else
                Map.Instance.stages[i].gameObject.SetActive(false);
        }
        indicator.SetActive(false);
        gameObject.SetActive(false);
    }

    IEnumerator MapViewerRoutine()
    {
        yield return null;
        while (Time.timeScale == 0f)
        {
            if (Input.GetKeyDown((KeyCode)MyKeyCode.Map))
            {
                Off();
            }
            yield return null;
        }

        // 씬이동코드
    }
}
