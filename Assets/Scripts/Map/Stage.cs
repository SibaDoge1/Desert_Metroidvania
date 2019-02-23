using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    private Transform camBoundary;
    private Transform dieBoundary;

    [SerializeField]
    public int myID { get; private set; }
    [SerializeField]
    private Vector2 camSize;
    [SerializeField]
    private Vector2 camPos;
    [SerializeField]
    private Vector2 availableSize;
    [SerializeField]
    private Vector2 availablePos;
    [SerializeField]
    private bool isMapInfoObtained;

    public Vector2 CamSize { get { return camSize; } }
    public Vector2 CamPos { get { return camPos; } }

    /// <summary>
    /// 리스폰을 위한 일종의 캐시저장소임
    /// </summary>
    [SerializeField]
    private List<GameObject> RespawnableObjects;

    void Awake()
    {
        camBoundary = transform.Find("Boundary");
        dieBoundary = transform.Find("DieBoundary");
        camSize.x = camBoundary.localScale.x / 2f;
        camSize.y = camBoundary.localScale.y / 2f;
        availableSize.x = dieBoundary.localScale.x / 2f;
        availableSize.y = dieBoundary.localScale.y / 2f;
        camPos.x = camBoundary.position.x;
        camPos.y = camBoundary.position.y;
        availablePos.x = dieBoundary.position.x;
        availablePos.y = dieBoundary.position.y;
        RespawnableObjects = new List<GameObject>();
        MakePalete();
    }

    void Start()
    {
        if (Map.Instance.CurStage != this) gameObject.SetActive(false);
        myID = Map.Instance.stages.IndexOf(this);
        isMapInfoObtained = SaveManager.GetMapInfo(myID);
    }
    
    public void DeActive()
    {
        gameObject.SetActive(false);
    }
    
    public void Active()
    {
        ResetStage();
        gameObject.SetActive(true);
    }

    public void ResetStage()
    {
        RespawnObjects();
    }

    public void MakePalete()
    {

        foreach (Transform trans in transform.Find("Objects").GetComponentInChildren<Transform>())
        {
            if (trans.GetComponent<InGameObj>() != null && trans.GetComponent<InGameObj>().isRespawnable == true)
            {
                GameObject objectPalete = Instantiate(trans.gameObject, trans.position, trans.rotation); //맵 상의 리스폰이 필요한 오브젝트를 찾고, 이를 RespawnableObjects에 저장해둠
                objectPalete.SetActive(false);
                objectPalete.transform.SetParent(transform.Find("RespawnPalete"));
                RespawnableObjects.Add(objectPalete);
            }
        }
    }

    /// <summary>
    /// RespawnableObjects에서 오브젝트들을 꺼내와서 생성함
    /// </summary>
    public void RespawnObjects()
    {
        foreach (Transform trans in transform.Find("Objects").GetComponentInChildren<Transform>()) //원래 있던 오브젝트는 삭제
        {
            if (trans.GetComponent<Respawnable>() != null)
            {
                Destroy(trans.gameObject);
            }
        }
        for (int i = 0; i < RespawnableObjects.Count; i++)
        {
            GameObject spawnedObject = Instantiate(RespawnableObjects[i], RespawnableObjects[i].transform.position, RespawnableObjects[i].transform.rotation);
            spawnedObject.transform.SetParent(transform.Find("Objects"));
            spawnedObject.SetActive(true);
        }
    }
    
    public void GetMapInfo()
    {
        isMapInfoObtained = true;
        SaveManager.SetMapInfo(myID, true);
    }

    public bool CheckOutSide(Vector2 position)
    {
        return position.x <= availablePos.x - availableSize.x
            || position.y <= availablePos.y - availableSize.y
            || position.x >= availablePos.x + availableSize.x
            || position.y >= availablePos.y + availableSize.y;
    }
}
