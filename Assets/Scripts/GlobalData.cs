using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalData
{
    public static bool isNewStart;
    public static bool isClear;
    private static string changeSceneTo;

    public static void SetChangeScene(string str)
    {
        changeSceneTo = str;
    }

    public static string GetChangeScene()
    {
        string temp = changeSceneTo;
        changeSceneTo = null;
        return temp;
    }
}
