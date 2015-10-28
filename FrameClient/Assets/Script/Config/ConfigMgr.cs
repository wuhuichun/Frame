using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using LitJson;
using System;


public class ConfigMgr{
    public static ConfigMgr Instance = new ConfigMgr();

    private string m_path;                                          // 配置文件路径 
    private bool m_isLoad = false;

    public static List<CityCfg> CityCfg_lst = new List<CityCfg>();                            // 城市
    public static List<StateCfg> StateCfg_lst= new List<StateCfg>();                          // 州
    public static List<ContryCfg> ContryCfg_lst = new List<ContryCfg>();                       // 势力
    public static List<QuestionCfg> QuestionCfg_lst = new List<QuestionCfg>();                // 问题
    public static List<RoleCfg> RoleCfg_lst = new List<RoleCfg>();

    // 初始化
    private void Init()
    {
        m_path = Application.streamingAssetsPath + "/Config/CJson/";
    }

    // 加载所有配置
    public void LoadAll()
    {
        if (m_isLoad)
        {
            return;
        }


        Init();

        LoadCfg<CityCfg>("City", CityCfg_lst);
        LoadCfg<StateCfg>("State", StateCfg_lst);
        LoadCfg<ContryCfg>("Contry", ContryCfg_lst);
        LoadCfg<QuestionCfg>("Question", QuestionCfg_lst);
        LoadCfg<RoleCfg>("Role", RoleCfg_lst);

        Log.Info("ConfigMgr.Load() Finished");
        m_isLoad = true;
    }

    // 加载配置, 存到List;
    private void LoadCfg<T>(string _fileName, List<T> _Lst)
    {
        string file = m_path + _fileName + ".json";
        
        if (!File.Exists(file))
        {
            Log.Error("配置文件不存在, url:" + file);
            return;
        }

        // 这里以后可能改成协程 WWW的形式
        StreamReader Sr = new StreamReader(file);
        string jsonStr = Sr.ReadToEnd();

        ParseCfg(jsonStr, _Lst);
    }

    private void ParseCfg<T>(string _jsonText, List<T> _Lst)
    {
        _Lst.Clear();

        JsonData jsonData = JsonMapper.ToObject(_jsonText);
        for (int i = 0; i < jsonData.Count; i++)
        {
            T cfg = JsonMapper.ToObject<T>(jsonData[i].ToJson());
            _Lst.Add(cfg);

            //Log.Info(cfg.ID.ToString() + ": " +cfg.Name);
        }    
    }

    // 加载配置, 存到Dictionary;
    private void LoadCfg<T>(string _fileName, Dictionary<string, T> _Dic)
    {
        string file = m_path + _fileName + ".json";

        if (!File.Exists(file))
        {
            Log.Error("配置文件不存在, url:" + file);
            return;
        }

        // 这里以后可能改成协程 WWW的形式
        StreamReader Sr = new StreamReader(file);
        string jsonStr = Sr.ReadToEnd();

        ParseCfg(jsonStr, _Dic);
    }

    private void ParseCfg<T>(string _jsonText, Dictionary<string, T> _Dic)
    {
        _Dic.Clear();

        JsonData jsonData = JsonMapper.ToObject(_jsonText);
        for (int i = 0; i < jsonData.Count; i++)
        {
            string key = jsonData[i]["ID"].ToString();
            T cfg = JsonMapper.ToObject<T>(jsonData[i].ToJson());
            _Dic[key] = cfg;
        }
    }

    // 根据序号 获取配置项
    public T GetCfg<T>(int _index, List<T> _Lst)
    {
        if (_index >= _Lst.Count)
        {
            return _Lst[_index];
        }

        Log.Error("配置缺失, _index:" + _index + " /List:" + _Lst.ToString());
        return default(T);
    }

    // 根据Key 获取配置项
    public T GetCfg<T>(string _key, Dictionary<string, T> _Dic)
    {
        if (_Dic.ContainsKey(_key))
        {
            return _Dic[_key];
        }

        Log.Error("配置缺失, _key:" + _key + " /Dictionary:" + _Dic.ToString());
        return default(T);
    }

    public QuestionCfg GetQuestionCfg(int id)
    {
        for (int i = 0; i < QuestionCfg_lst.Count; i++)
        {
            if (QuestionCfg_lst[i].ID == id)
            {
                return QuestionCfg_lst[i];
            }
        }

        return null;
    }

}
