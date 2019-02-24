using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalData
{
    public static bool isNewStart;
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
