using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public delegate void voidFunc();

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

    }
    
    void OnEnable()
    {
        SoundDelegate.Instance.PlayBGM(BGM.Title);
    }

    public void OnStartButtonDown()
    {;
        GlobalData.isNewStart = true;
        GlobalData.SetChangeScene("Scenes/Stage");
        FadeTool.Instance.FadeInOut(1f,0.5f, LoadScene);
    }

    public void OnContinueButtonDown()
    {
        GlobalData.isNewStart = false;
        GlobalData.SetChangeScene("Scenes/Stage");
        FadeTool.Instance.FadeInOut(1f, 0.5f, LoadScene);
    }

    public void OnExitButtonDown()
    {
        Application.Quit();
    }

    public void LoadScene()
    {

        SceneManager.LoadScene("Scenes/LoadingScene");
    }

}
