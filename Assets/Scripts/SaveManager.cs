﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Newtonsoft.Json;
using System.Runtime.InteropServices;

[Serializable]
public class SaveData
{
    public SaveData()
    {
        curStage = null;
        hp = 0;
        BossKillInfo = new Dictionary<int, bool>();
        MapInfo = new Dictionary<int, bool>();
        potalLockInfo = new Dictionary<int, bool>();
        skillUnlockInfos = new bool[4];
        triggerInfo = new Dictionary<int, bool>();
        defaultMaxHp = 1;
        isSetted = false;
        isClear = false;
    }

    public string curStage;
    public float posX;
    public float posY;
    public Dictionary<int, bool> MapInfo;
    public Dictionary<int, bool> BossKillInfo;
    public Dictionary<int, bool> potalLockInfo;
    public Dictionary<int, bool> triggerInfo;
    public bool[] skillUnlockInfos; //순서 : 대시, 점프스킬, 대시스킬, 평타3
    public int hp;
    public int defaultMaxHp;
    public bool isClear;
    public bool isSetted;
}

public static class SaveManager
{

    private static string Ext = ".ini";
    private static string FileName = "save";
    private static string Path = Application.dataPath;
    [SerializeField]
    private static SaveData saveData;

    public static void FirstSet()
    {
        if (saveData != null)
        {
            return;
        }
        saveData = new SaveData();
    }

    #region Set/Add/get
    public static void AddBossKillInfo(int boss)
    {
        if (!saveData.BossKillInfo.ContainsKey(boss))
            saveData.BossKillInfo.Add(boss, false);
    }

    public static void AddMapInfo(int stage)
    {
        if (!saveData.MapInfo.ContainsKey(stage))
            saveData.MapInfo.Add(stage, false);
    }

    public static void AddPotalLockInfo(int idx)
    {
        if (!saveData.potalLockInfo.ContainsKey(idx))
            saveData.potalLockInfo.Add(idx, false);
    }

    public static void SetBossKillInfo(int boss, bool isKilled)
    {
        if (!saveData.BossKillInfo.ContainsKey(boss))
            saveData.BossKillInfo.Add(boss, isKilled);
        else
            saveData.BossKillInfo[boss] = isKilled;
    }

    public static void SetMapInfo(int stage, bool isObtained)
    {
        if (!saveData.MapInfo.ContainsKey(stage))
            saveData.MapInfo.Add(stage, isObtained);
        else
            saveData.MapInfo[stage] = isObtained;
    }

    public static void SetPotalLockInfo(int idx, bool isUnLocked)
    {
        if (!saveData.potalLockInfo.ContainsKey(idx))
            saveData.potalLockInfo.Add(idx, isUnLocked);
        else
            saveData.potalLockInfo[idx] = isUnLocked;
    }

    public static void SetSkillUnlockInfo(int idx, bool isUnLocked)
    {
        if (saveData.skillUnlockInfos.Length < idx)
            return;
        saveData.skillUnlockInfos[idx] = isUnLocked;
    }

    public static void SetTriggerInfo(int idx, bool isUnLocked)
    {
        if (!saveData.triggerInfo.ContainsKey(idx))
            return;
        saveData.triggerInfo[idx] = isUnLocked;
    }

    public static void SetIsClear(bool value)
    {
        saveData.isClear = value;
    }

    public static bool GetBossKillInfo(int boss)
    {
        if (!saveData.BossKillInfo.ContainsKey(boss))
            saveData.BossKillInfo.Add(boss, false);
        return saveData.BossKillInfo[boss];
    }

    public static bool GetMapInfo(int stage)
    {
        if (!saveData.MapInfo.ContainsKey(stage))
        {
            saveData.MapInfo.Add(stage, false);
            return false;
        }
        return saveData.MapInfo[stage];
    }

    public static bool GetPotalLockInfo(int idx)
    {
        if (!saveData.potalLockInfo.ContainsKey(idx))
            saveData.potalLockInfo.Add(idx, false);
        return saveData.potalLockInfo[idx];
    }

    public static bool GetSkillUnlockInfo(int idx)
    {
        if (saveData.skillUnlockInfos.Length < idx)
            return false;
        return saveData.skillUnlockInfos[idx];
    }

    public static bool GetTriggerInfo(int boss)
    {
        if (!saveData.triggerInfo.ContainsKey(boss))
            saveData.triggerInfo.Add(boss, false);
        return saveData.triggerInfo[boss];
    }

    public static bool GetIsClear()
    {
        return saveData.isClear;
    }
    #endregion

    public static void SetSaveData()
    {
        if (saveData == null)
        {
            saveData = new SaveData();
        }
        saveData.curStage = PlayManager.Instance.Player.transform.parent.parent.GetComponent<Stage>().name;
        saveData.hp = PlayManager.Instance.Player.Hp;
        saveData.defaultMaxHp = PlayManager.Instance.Player.DefaultMaxHp;
        saveData.posX = PlayManager.Instance.Player.transform.position.x;
        saveData.posY = PlayManager.Instance.Player.transform.position.y;
        saveData.isSetted = true;
        Debug.Log("savedata Setted :" + JsonUtility.ToJson(saveData));
    }

    public static bool SaveToFile()
    {
        SetSaveData();
        return JsonSave(saveData, FileName, Path);
    }

    public static void FirstLoad(bool isNewStart)
    {
        if (!isNewStart && JsonLoad(FileName, Path))
        {
            return;
        }
        saveData = new SaveData();
        Debug.Log("savedata created");
    }

    public static void ApplySave()
    {
        if (saveData == null || !saveData.isSetted)
        {
            FirstSet();
            return;
        }

        Map.Instance.ChangeStage(Map.Instance.CurStage, saveData.curStage, new Vector2(saveData.posX, saveData.posY));
        for (int i = 0; i<Map.Instance.stages.Count; i++)
        {
            if(GetMapInfo(i) == true )
                Map.Instance.stages[i].UnlockMapInfo();
        }
        for (int i = 0; i < Map.Instance.bosses.Count; i++)
        {
            if (GetBossKillInfo(i) == true)
                Map.Instance.bosses[i].DeActive();
        }
        for (int i = 0; i < Map.Instance.PotalWithLocks.Count; i++)
        {
            if (GetPotalLockInfo(i) == true)
                Map.Instance.PotalWithLocks[i].Unlock();
        }
        for (int i = 0; i < Map.Instance.EventTriggers.Count; i++)
        {
            if (GetTriggerInfo(i) == true)
                Map.Instance.EventTriggers[i].Unlock();
        }
        PlayManager.Instance.Player.Hp = saveData.hp;
        PlayManager.Instance.Player.DefaultMaxHp = saveData.defaultMaxHp;
    }

    public static bool JsonSave(SaveData data, string filename, string path)
    {
        string json = JsonConvert.SerializeObject(data);
        FileStream file = new FileStream(path + "/" + filename + Ext, FileMode.Create);
        if (file == null)
        {
            Debug.LogError("file Create Error!");
            return false;
        }
        StreamWriter writer = new StreamWriter(file);
        writer.Write(json);
        
        writer.Close();
        Debug.Log("SaveComplete" + json);
        return true;
    }

    public static bool JsonLoad(string filename, string path)
    {
        FileStream file = new FileStream(path + "/" + filename + Ext, FileMode.OpenOrCreate);
        StreamReader reader = new StreamReader(file);
        string json = reader.ReadToEnd();
        Debug.Log("load data " + json);
        if (file.Length == 0)
        {
            Debug.Log("make new savefile");
            return false;
        }
        saveData = JsonConvert.DeserializeObject<SaveData>(json);
        file.Close();
        return true;
    }
}
