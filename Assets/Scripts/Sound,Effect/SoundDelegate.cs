using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BGM
{
    A1A2,
    B1B2,
    C1C2,
    C3,
    Title
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
    GolemAttack
}
public class SoundDelegate : MonoBehaviour {
    #region variables
    AudioSource bgm;

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

    public AudioClip[] bgmAudioClips;
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

        bgm = transform.Find("BGM").GetComponent<AudioSource>();
    }

    public void PlayBGM(BGM b)
    {
        bgm.clip = bgmAudioClips[(int)b];
       // bgm.volume = bgmSound;
        bgm.Play();
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
