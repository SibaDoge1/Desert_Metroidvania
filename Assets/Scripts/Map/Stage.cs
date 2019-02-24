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
        FirstSetStage();
    }

    void Start()
    {
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

    public void FirstSetStage()
    {
        foreach (Transform trans in transform.Find("Objects").GetComponentsInChildren<Transform>())
        {
            if (trans.GetComponent<Respawnable>() != null)
            {
                trans.GetComponent<Respawnable>().FirstSet();
            }
        }
    }

    public void ResetStage()
    {
        foreach (Transform trans in transform.Find("Objects").GetComponentsInChildren<Transform>())
        {
            if (trans.GetComponent<Respawnable>() != null)
            {
                trans.GetComponent<Respawnable>().Reset();
            }
        }
    }

    /*
    public void MakePalete()
    {

        foreach (Transform trans in transform.Find("Objects").GetComponentInChildren<Transform>())
        {
            if (trans.GetComponent<RespawnEnemy>() != null)
            {
                GameObject objectPalete = Instantiate(trans.gameObject, trans.position, trans.rotation); //맵 상의 리스폰이 필요한 오브젝트를 찾고, 이를 RespawnableObjects에 저장해둠
                objectPalete.SetActive(false);
                objectPalete.transform.SetParent(transform.Find("RespawnPalete"));
                RespawnableObjects.Add(objectPalete);
            }
        }
    }
    */

    public void UnlockMapInfo()
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
