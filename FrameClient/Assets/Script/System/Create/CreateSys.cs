using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class CreateSys{

    public static readonly CreateSys Instance = new CreateSys();

    private List<int> m_questionId_lst = null;
    private List<RoleCfg> m_ChoiceRole_lst = null;

    public int m_questionCount = 4;                                         // 问题个数
    private readonly int m_maxRole = 3;                                     // 最大角色数量
    private eSex m_selectSex = eSex.Male;                                   // 选择性别
    private eRoleType m_selectType = eRoleType.Wen;                        // 选择类型
    private eTrait m_selectTrait = eTrait.Utility;                          // 选择性格
    private eState m_selectState = eState.Si;                               // 选择州
    private int m_selectIndex = 0;                                          // 玩家选择的角色序号

    public Action SelectIndexCall;

    public eSex SelectSex
    {
        get { return m_selectSex; }
        set { m_selectSex = value; }
     
    }

    public eRoleType SelectType
    {
        set { m_selectType = value; }
        get { return m_selectType; }
    }

    public eTrait SelectTrait
    {
        set { m_selectTrait = value; }
        get { return m_selectTrait; }
    }

    public eState SelectState
    {
        set { m_selectState = value; }
        get { return m_selectState; }  
    }

    public int SelectIndex
    {
        get { return m_selectIndex; }
        set
        {
            if (m_selectIndex != value)
            {
                m_selectIndex = value;
                if (SelectIndexCall != null)
                {
                    SelectIndexCall();
                }
            }
        }
    }

//    public void SetSelectIndex( int){

//}

    public List<int> QuestionList
    {
        get { return m_questionId_lst; }
    }

    public List<RoleCfg> GetChoiceHero()
    {
        if(m_ChoiceRole_lst != null){

            return m_ChoiceRole_lst;
        }

        m_ChoiceRole_lst = new List<RoleCfg>();

        Debug.Log("GetChoiceHero, ConfigMgr.StateCfg_lst.Count:" + ConfigMgr.StateCfg_lst.Count);
        // 筛选满足条件的
        for (int i = 0; i < ConfigMgr.StateCfg_lst.Count; i++)
        {
            RoleCfg cfg = ConfigMgr.RoleCfg_lst[i];

            Debug.Log("==>> 1");
            if ((eSex)cfg.Sex != this.SelectSex)
                continue;

            Debug.Log("==>> 2");
            if ((eRoleType)cfg.Type != this.SelectType)
                continue;

            Debug.Log("==>> 3");
            if ((eTrait)cfg.Trait != this.SelectTrait)
                continue;

            Debug.Log("==>> 4");
            if ((eState)cfg.State != this.SelectState)
                continue;

            m_ChoiceRole_lst.Add(cfg);
        }

        // 数量不够就选则
        if (m_ChoiceRole_lst.Count > this.m_maxRole)
        {
            // 数量多了随机删除
            int count = m_ChoiceRole_lst.Count - this.m_maxRole;
            for (int i = 0; i < count; i++)
            {
                int deleteIndex = Game.Instance.Rand.Next(0, m_ChoiceRole_lst.Count - 1);
                m_ChoiceRole_lst.RemoveAt(deleteIndex);
            }
        }

        // 出入 自定义
        InserCustomRole();

        return m_ChoiceRole_lst;
    }

    private void InserCustomRole()
    {
        // 插入自定义英雄
        RoleCfg custom = new RoleCfg();
        custom.ID = -1;
        custom.Name = "自定义";
        custom.Alias = "无";
        custom.Desc = "无";

        custom.Sex = (int)CreateSys.Instance.SelectSex;
        custom.Type = (int)CreateSys.Instance.SelectType;
        custom.Trait = (int)CreateSys.Instance.SelectTrait;
        custom.State = (int)CreateSys.Instance.SelectState;

        custom.Star = 0;
        custom.Leading = 20;
        custom.Strength = 20;
        custom.Wit = 20;
        custom.Charm = 20;
        custom.politics = 20;

        m_ChoiceRole_lst.Add(custom);
    }

    public void Init()
    {
        // 插入需要提问的问题列表
        m_questionId_lst = new List<int>(4);
        m_questionId_lst.Add(1);
        m_questionId_lst.Add(2);
        m_questionId_lst.Add(3);
        m_questionId_lst.Add(4);
    }

    public QuestionCfg GetCreateQuestionByIndex(int _index)
    {
        if (_index >= m_questionId_lst.Count)
        {
            //Log.Error("_index >= m_questionId_lst.Count");
            return null;
        }

        int questionId = m_questionId_lst[_index];

        return ConfigMgr.Instance.GetQuestionCfg(questionId);
    }
    
    
	
    public RoleCfg GetSelectRole()
    {
        Debug.Log("GetSelectRole(), this.SelectIndex:" + this.SelectIndex);
        return this.GetChoiceHero()[this.SelectIndex];
    }
}
