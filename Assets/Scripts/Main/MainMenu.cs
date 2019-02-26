using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public delegate void voidFunc();
    private Image flower;
    private bool isLoading;

    void Awake()
    {/*
        #region 안드로이드 설정
        Input.multiTouchEnabled = false;
        Application.targetFrameRate = 60;
        //Screen.SetResolution(1920,1080, true);
        //Screen.SetResolution(Screen.width, (Screen.width / 2) * 3, true);
        Screen.orientation = ScreenOrientation.Landscape;
        #endregion
        */
        flower = GameObject.Find("Flower").GetComponent<Image>();
        flower.gameObject.SetActive(false);
        SaveManager.FirstLoad(false);

    }
    
    void OnEnable()
    {
        SoundDelegate.Instance.PlayBGM(BGM.Title);
        if (SaveManager.GetIsClear() == true)
        {
            Debug.Log("clear");
            flower.gameObject.SetActive(true);
            SoundDelegate.Instance.PlayBGM(BGM.TitleClear);
        }
    }

    public void OnStartButtonDown()
    {
        if (isLoading) return;
        isLoading = true;
        SoundDelegate.Instance.PlayEffectSound(EffectSoundType.Button);
        GlobalData.isNewStart = true;
        GlobalData.SetChangeScene("Scenes/Stage");
        FadeTool.Instance.FadeOut(1f, LoadScene);
    }

    public void OnContinueButtonDown()
    {
        if (isLoading) return;
        isLoading = true;
        SoundDelegate.Instance.PlayEffectSound(EffectSoundType.Button);
        GlobalData.isNewStart = false;
        GlobalData.SetChangeScene("Scenes/Stage");
        FadeTool.Instance.FadeOut(1f, LoadScene);
    }

    public void OnExitButtonDown()
    {
        SoundDelegate.Instance.PlayEffectSound(EffectSoundType.Button);
        Application.Quit();
    }

    public void LoadScene()
    {
        isLoading = false;
        SceneManager.LoadScene("Scenes/LoadingScene");
    }

}
