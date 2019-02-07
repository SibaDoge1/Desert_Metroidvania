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

    Camera mainCamera;
    GameObject player;

    [SerializeField]
    float mapX = 0f;
    [SerializeField]
    float mapY = 0f;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null) instance = this;

    }

    private void Start()
    {
        mainCamera = GetComponent<Camera>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        var pos = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
        transform.position = pos;

        CheckCameraBoundary();
    }

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

    private void CheckCameraBoundary()
    {
        var pos = transform.position;

        pos.x = Mathf.Clamp(pos.x, -mapX + 9f, mapX - 9f);
        pos.y = Mathf.Clamp(pos.y, -mapY + 5.5f, mapY - 5.5f);

        transform.position = pos;
    }
    
}
