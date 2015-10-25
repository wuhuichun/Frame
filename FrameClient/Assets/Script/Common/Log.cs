using UnityEngine;
using System.Collections;

public class Log  {

    public static void Info(string _str)
    {
        Debug.Log("[Info] " + _str);
    }

    public static void Warn(string _str)
    {
        Debug.LogWarning("[Warn] " + _str);
    }

    public static void Error(string _str)
    {
        Debug.LogError("[Error] " + _str);
    }

}
