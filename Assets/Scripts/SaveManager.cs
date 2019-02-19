using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Newtonsoft.Json;

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
    }

    public string curStage;
    public Dictionary<int, bool> MapInfo;
    public IDictionary<int, bool> BossKillInfo;
    public Dictionary<int, bool> potalLockInfo;
    public int hp;
    public bool isSetted;
}

public static class SaveManager
{
    private static string Ext = ".txt";
    private static string FileName = "save";
    private static string Path = Application.dataPath;
    [SerializeField]
    public static SaveData saveData { get; private set; }

    public static void FirstSet()
    {
        if (saveData != null)
        {
            return;
        }
        saveData = new SaveData();
        Debug.Log("savedata created :" + JsonUtility.ToJson(saveData));
    }

    #region Set/Add
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
    #endregion

    public static bool SaveAll()
    {
        saveData.curStage = Map.Instance.CurStage.name;
        saveData.hp = PlayManager.Instance.Player.Hp;
        return JsonSave(saveData, FileName, Path);
    }

    public static void LoadAll()
    {
        JsonLoad(FileName, Path);
        FirstSet();
    }

    public static bool JsonSave(SaveData data, string filename, string path)
    {
        string json = JsonConvert.SerializeObject(data);
        FileStream file = new FileStream(path + "/" + filename + Ext, FileMode.Create);
        if(file == null)
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
        Debug.Log("LoadComplete");
        return true;
    }
}
