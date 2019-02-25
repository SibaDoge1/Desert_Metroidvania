using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectType{PowerAura, ShinyItem, CircularLightWall, LightWall}
//public enum RangeEffectType {CARD,ENEMY,DIR}
public enum UIEffect {CARD,REPORT }
public class EffectDelegate : MonoBehaviour
{
    private static EffectDelegate instance = null;
    public static EffectDelegate Instance
    {
        get { return instance; }
    }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            UnityEngine.Debug.LogError("SingleTone Error : " + this.name);
            Destroy(this);
        }
    }

	public GameObject[] effectPrefabs;
    //public GameObject[] rangeLayer;
    public GameObject[] UIEffect;
    public GameObject textEffectPrefab;

    public GameObject MadeEffect(UIEffect eType,Transform parent)
    {
        return Instantiate(UIEffect[(int)eType], parent);
    }
    public GameObject MadeEffect(EffectType eType, Transform parent){
        return Instantiate(effectPrefabs [(int)eType], parent.position, effectPrefabs[(int)eType].transform.rotation, parent);
	}
	public GameObject MadeEffect(EffectType eType, InGameObj parent){
        return Instantiate(effectPrefabs [(int)eType], parent.transform);
	}
	public GameObject MadeEffect(EffectType eType, Vector3 worldPosition){
        return Instantiate(effectPrefabs [(int)eType], worldPosition, Quaternion.identity);
	}

    public void DestroyEffect(GameObject go)
    {
        Destroy(go);
    }
    /*

	public GameObject MadeEffect(int damage, Transform parent){
        GameObject go = Instantiate(textEffectPrefab, parent);
        go.GetComponent<EffectText>().Init(damage.ToString(), damage >= 0 ? TextColorType.Green : TextColorType.Red);
        return go;
    }
	public GameObject MadeEffect(int damage, InGameObj parent){
        GameObject go =  Instantiate(textEffectPrefab, parent.transform);
        go.GetComponent<EffectText>().Init(damage.ToString(), damage >= 0 ? TextColorType.Green : TextColorType.Red);
        return go;
    }
	public GameObject MadeEffect(int damage, Vector3 worldPosition){
        if (damage == 0)
            return null;

        GameObject go= Instantiate(
            textEffectPrefab, worldPosition, Quaternion.identity);
        go.GetComponent<EffectText>()
            .Init(damage.ToString(), damage >= 0 ? TextColorType.Green : TextColorType.Red);
        return go;
    }
	public GameObject MadeEffect(int damage, Tile targetTile){
        GameObject go = Instantiate(
            textEffectPrefab, targetTile.transform.position, Quaternion.identity);
        go.GetComponent<EffectText>()
            .Init(damage.ToString(), damage >= 0 ? TextColorType.Green : TextColorType.Red);
        return go;
    }

    /// <summary>
    /// Range Effect Delete할때 사용
    /// </summary>
    public void DestroyEffect(List<GameObject> go)
    {
        if(go != null)
        {
            for (int i = 0; i < go.Count; i++)
            {
                DestroyImmediate(go[i]);
            }
        }
    }
    */
}
