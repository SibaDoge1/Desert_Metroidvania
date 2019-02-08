using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private static CameraManager instance = null;
    public static CameraManager Instance
    {
        get { return instance; }
    }

    private Camera mainCamera;
    private GameObject player;
    private Vector2 camWorldScale;
    private Stage curStage;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null) instance = this;
        else if(instance != this)
        {
            Debug.LogError("Singleton Error! : " + this.name);
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        mainCamera = GetComponent<Camera>();
        player = GameObject.Find("Player");
        camWorldScale = (mainCamera.ScreenToWorldPoint(new Vector2(mainCamera.pixelWidth, mainCamera.pixelHeight)) - mainCamera.ScreenToWorldPoint(new Vector2(0, 0))) / 2f; //카메라의 월드기준 크기
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        curStage = Map.Instance.CurStage;
        Vector2 pos = player.transform.position;
        pos.x = Mathf.Clamp(pos.x, curStage.Pos.x - curStage.Size.x + camWorldScale.x, curStage.Pos.x + curStage.Size.x - camWorldScale.x);
        pos.y = Mathf.Clamp(pos.y, curStage.Pos.y - curStage.Size.y + camWorldScale.y, curStage.Pos.y + curStage.Size.y - camWorldScale.y);

        transform.position = new Vector3(pos.x, pos.y, transform.position.z);
        //transform.position = Vector3.Lerp(transform.position, new Vector3(pos.x, pos.y, transform.position.z), Time.deltaTime*5f);
    }

    /*
    public void ResetMapInfo()
    {
        string currentMap;

        currentMap =  PlayerPrefs.GetString("Map");

        switch(currentMap)
        {
            case "A1":
                break;
            case "A2":
                mapX = 100f;
                mapY = 5.5f;
                break;
            default:
                break;
        }
    }
    */
}
