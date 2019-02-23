using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    private Transform boundary;
    private Transform dieBoundary;

    [SerializeField]
    public int myID { get; private set; }
    [SerializeField]
    private Vector2 size;
    [SerializeField]
    private Vector2 pos;
    [SerializeField]
    private bool isMapInfoObtained;
    private Vector2 MaxSize;

    public Vector2 Size { get { return size; } }
    public Vector2 Pos { get { return pos; } }

    /// <summary>
    /// 리스폰을 위한 일종의 캐시저장소임
    /// </summary>
    [SerializeField]
    private List<GameObject> RespawnableObjects;

    void Awake()
    {
        boundary = transform.Find("Boundary");
        dieBoundary = transform.Find("DieBoundary");
        size.x = boundary.localScale.x / 2f;
        size.y = boundary.localScale.y / 2f;
        MaxSize.x = dieBoundary.localScale.x / 2f;
        MaxSize.y = dieBoundary.localScale.y / 2f;
        pos.x = transform.position.x;
        pos.y = transform.position.y;
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

    public bool checkOutSide(Vector2 pos)
    {
        return pos.x <= Map.Instance.CurStage.Pos.x - Map.Instance.CurStage.MaxSize.x
            || pos.y <= Map.Instance.CurStage.Pos.y - Map.Instance.CurStage.MaxSize.y
            || pos.x >= Map.Instance.CurStage.Pos.x + Map.Instance.CurStage.MaxSize.x
            || pos.y >= Map.Instance.CurStage.Pos.y + Map.Instance.CurStage.MaxSize.y;
    }
}
