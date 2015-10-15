using UnityEngine;
using System.Collections;

public class ConfigMgr{
    public static ConfigMgr Instance = new ConfigMgr();

    private string m_path;

    public void Load()
    {
        m_path = "Config/";

        LoadConfig();
    }

    private void LoadConfig(){

    }

}
