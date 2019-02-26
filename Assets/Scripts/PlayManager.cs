using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayManager : MonoBehaviour
{
    private static PlayManager instance = null;
    public static PlayManager Instance
    {
        get { return instance; }
    }
    private MapViewer viewer;
    private Pause pause;
    private bool isFirstSaved = false;
    private voidFunc[] OnUsingSkill;

    void Awake()
    {
        if (instance == null) instance = this;
        else
        {
            Debug.LogWarning("Singleton Error! : " + this.name);
            Destroy(this);
        }
        SaveManager.FirstLoad(GlobalData.isNewStart);
        viewer = GameObject.Find("Canvas").transform.Find("MapViewer").GetComponent<MapViewer>();
        player = GameObject.Find("Player").GetComponent<Player>();
        pause = GameObject.Find("Canvas").transform.Find("Pause").GetComponent<Pause>();
        player.MakePalette();
        OnUsingSkill = new voidFunc[5];
    }

    void Start()
    {
        SaveManager.ApplySave();
        FadeTool.Instance.FadeIn(1f, null);
    }

    void Update()
    {
        if (Input.GetKeyDown((KeyCode)MyKeyCode.Map))
        {
            viewer.Toggle();
        }
        if (Input.GetKeyDown((KeyCode)MyKeyCode.Esc))
        {
            pause.Toggle();
        }
        if (!isFirstSaved)
        {
            for (int i = 0; i < Map.Instance.stages.Count; i++)
            {
                if (Map.Instance.CurStage != Map.Instance.stages[i])
                    Map.Instance.stages[i].DeActive();
            }
            SaveManager.SaveToFile();
            isFirstSaved = true;
        }
    }
    public void Return()
    {
        FadeTool.Instance.FadeInOut(0.5f, 0.2f, ReturnToCheckPoint);
    }

    public void ReturnToCheckPoint()
    {
        player.Reset();
        player.gameObject.SetActive(true);
        SaveManager.ApplySave();
    }
    public void GoToTitle()
    {
        GlobalData.SetChangeScene("Scenes/MainMenu");
        Time.timeScale = 1f;
        FadeTool.Instance.FadeOut(1f, LoadScene);
    }

    public void LoadScene()
    {
        SceneManager.LoadScene("Scenes/LoadingScene");
    }

    public bool isTestMode = true;

    private Player player = null;
    public Player Player
    {
        get { return player; } set { player = value; }
    }

    public void DoSkill(Skill skill)
    {
        if(OnUsingSkill[(int)skill] != null)
            OnUsingSkill[(int)skill]();
    }
    public void AddSkillFunc(Skill skill, voidFunc func)
    {
        OnUsingSkill[(int)skill] += func;
    }
    public void DeleteSkillFunc(Skill skill, voidFunc func)
    {
        OnUsingSkill[(int)skill] -= func;
    }
}

