using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private LoadingManager loadingPanel;
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
        loadingPanel = GameObject.Find("Canvas").transform.Find("LoadingPanel").GetComponent<LoadingManager>();

    }
    
    void Start()
    {
    }

    public void OnStartButtonDown()
    {
        //SceneManager.LoadScene("Levels/LoadingScene");
        GlobalData.isNewStart = true;
        loadingPanel.LoadScene();
    }

    public void OnContinueButtonDown()
    {
        GlobalData.isNewStart = false;
        loadingPanel.LoadScene();
    }

    public void OnExitButtonDown()
    {
        Application.Quit();
    }

}
