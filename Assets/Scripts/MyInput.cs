using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum MyKeyCode
{
    Attack = KeyCode.A,
    Right = KeyCode.RightArrow,
    Left = KeyCode.LeftArrow,
    Up = KeyCode.UpArrow,
    Down = KeyCode.DownArrow,
    Dash = KeyCode.LeftShift,
    Jump = KeyCode.Space,
    Map = KeyCode.M,
    Interact = KeyCode.Tab
};

public static class MyInput
{

    public static bool GetKey(MyKeyCode code)
    {
        if (!IsInputEnabled())
            return false;
        else
            return Input.GetKey((KeyCode)code);
    }

    public static bool GetKeyDown(MyKeyCode code)
    {
        if (!IsInputEnabled())
            return false;
        else
            return Input.GetKeyDown((KeyCode)code);
    }

    public static bool GetKeyUp(MyKeyCode code)
    {
        if (!IsInputEnabled())
            return false;
        else
            return Input.GetKeyUp((KeyCode)code);
    }

    private static bool IsInputEnabled()
    {
        return Time.timeScale == 1f;
    }
}
