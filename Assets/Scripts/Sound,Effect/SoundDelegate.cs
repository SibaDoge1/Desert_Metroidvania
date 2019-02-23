using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BGM
{
    FLOOR1,
    Floor1_BOSS,
    Title
}
public enum EffectSoundType
{
    GetHit,
    RoomClear,
    RoomMove,
    GameOver,
}
public enum ButtonSoundType
{
    Normal,
    Select,
    Exit,
    Confirm,
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
    public GameObject[] effectAudioObject;
    #endregion

    void Awake()
    {
        //Changes: destroy 부분 추가
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

        bgm = transform.Find("BGM").GetComponent<AudioSource>();
    }

    public void PlayBGM(BGM b)
    {
        bgm.clip = bgmAudioClips[(int)b];
        bgm.volume = bgmSound;
        bgm.Play();
    }

    public GameObject PlayEffectSound(EffectSoundType ef, Vector3 position)
    {
        return Instantiate(effectAudioObject[(int)ef], position, Quaternion.identity);
    }

    public GameObject PlayEffectSound(EffectSoundType ef, Transform obj)
    {
        return Instantiate(effectAudioObject[(int)ef], obj.position, Quaternion.identity, obj);
    }
}
