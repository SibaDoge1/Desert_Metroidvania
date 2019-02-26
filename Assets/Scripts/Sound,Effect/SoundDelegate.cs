using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BGM
{
    None = -1,
    A1A2,
    B1B2,
    C1C2,
    C3,
    Title,
    TitleClear
}
public enum EffectSoundType
{
    Attack1,
    Attack2,
    Attack3,
    Hit,
    HitWall,
    HitGolem,
    MummyAttack,
    MummyDie,
    SlimeAttack,
    SlimeDie,
    GolemAttack,
    Button,
    Dash,
    Buff,
    Jump,
    GolemAttack2
}
public class SoundDelegate : MonoBehaviour {
    #region variables
    SoundObject bgm;

    private static float bgmSound = 1f;
    public static float BGMSound
    {
        get { return bgmSound; }
        set
        {
            bgmSound = value;
        }
    }

    private static float effectSound =1f;
    public static float EffectSound
    {
        get { return effectSound; }
        set { effectSound = value; }
    }

    private static SoundDelegate instance = null;
    public static SoundDelegate Instance
    {
        get { return instance; }
    }

    public GameObject[] bgmAudioClips;
    public GameObject[] effectAudioClips;
    public GameObject audioSource;
    #endregion

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
            Destroy(gameObject);
        }

        bgm = transform.Find("BGM").GetComponent<SoundObject>();
           

    }

    public void SetVolume()
    {
    }

    public void PlayBGM(BGM b)
    {
        if (bgm.bgm == b)
            return;
        Destroy(bgm.gameObject);
        GameObject madeObj = Instantiate(bgmAudioClips[(int)b], transform.position, Quaternion.identity, transform);
        bgm = madeObj.GetComponent<SoundObject>();
        bgm.bgm = b;
        // bgm.volume = bgmSound;
    }

    public void StopBGM()
    {
        bgm.Stop();
    }


    public GameObject PlayEffectSound(EffectSoundType ef)
    {
        GameObject madeObj = Instantiate(effectAudioClips[(int)ef], transform.position, Quaternion.identity);
        madeObj.GetComponent<SoundObject>().Play();
        return madeObj;
    }

    public GameObject PlayEffectSound(EffectSoundType ef, Vector3 position)
    {
        GameObject madeObj = Instantiate(effectAudioClips[(int)ef], position, Quaternion.identity);
        madeObj.GetComponent<SoundObject>().Play();
        return madeObj;
    }

    public GameObject PlayEffectSound(EffectSoundType ef, Transform obj)
    {
        GameObject madeObj = Instantiate(effectAudioClips[(int)ef], obj.position, Quaternion.identity, obj);
        madeObj.GetComponent<SoundObject>().Play();
        return madeObj;
    }
}
