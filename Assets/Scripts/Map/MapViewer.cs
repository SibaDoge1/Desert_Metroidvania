using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapViewer : MonoBehaviour
{
    private bool isViewing;
    private GameObject indicator;

    // Start is called before the first frame update
    void Awake()
    {
        indicator = PlayManager.Instance.Player.transform.Find("MapIndicator").gameObject;
    }

    public void Toggle()
    {

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
        isViewing = true;
        gameObject.SetActive(true);
        indicator.SetActive(true);
    }

    public void Off()
    {
        isViewing = false;
        gameObject.SetActive(false);
        indicator.SetActive(false);
    }
}
