using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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
    private bool isMovable;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null) instance = this;
        else if(instance != this)
        {
            Debug.LogError("Singleton Error! : " + this.name);
            Destroy(gameObject);
        }
        isMovable = true;
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
        if (isMovable)
        {
            Vector2 pos = player.transform.position;
            MoveCam(player.transform.position);
        }
        //transform.position = Vector3.Lerp(transform.position, new Vector3(pos.x, pos.y, transform.position.z), Time.deltaTime*5f);
    }

    public void MoveCam(Vector2 to)
    {
        curStage = Map.Instance.CurStage;
        Vector2 pos;
        pos.x = Mathf.Clamp(to.x, curStage.CamPos.x - curStage.CamSize.x + camWorldScale.x, curStage.CamPos.x + curStage.CamSize.x - camWorldScale.x);
        pos.y = Mathf.Clamp(to.y, curStage.CamPos.y - curStage.CamSize.y + camWorldScale.y, curStage.CamPos.y + curStage.CamSize.y - camWorldScale.y);
        transform.position = new Vector3(pos.x, pos.y, transform.position.z);
    }

    public void ShakeCam(float _amount, float _duration)
    {
        StartCoroutine(Shake(_amount, _duration));
    }

    IEnumerator Shake(float _amount, float _duration)
    {
        isMovable = false;
        Vector3 originPos = transform.position;
        float timer = 0;

        while (timer <= _duration)
        {
            MoveCam(Random.insideUnitCircle * _amount + (Vector2)originPos);
            timer += Time.deltaTime;
            yield return null;
        }
        isMovable = true;
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
