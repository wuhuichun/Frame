using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CreateSys{

    public static readonly CreateSys Instance = new CreateSys();

    private List<int> m_questionId_lst;

    public int m_questionCount = 4;

    public List<int> QuestionList
    {
        get { return m_questionId_lst; }
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
            Log.Error("_index >= m_questionId_lst.Count");
            return null;
        }

        int questionId = m_questionId_lst[_index];

        return ConfigMgr.Instance.GetQuestionCfg(questionId);
    }
    
	
}
