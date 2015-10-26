using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class QustionUI : MonoBehaviour {
    private Transform TfNodePos;
    private Button BtnNext;
    private Button BtnBack;
    private CreateRoleUI UICreateRole;

    private int m_stepIndex = 1;        // 当前步数
    private int m_stepMax = 5;          // 最大步数
    private List<QustionUnit> m_QuestionUnit_lst = new List<QustionUnit>();

    void Awake()
    {
        TfNodePos = transform.FindChild("NodePos").transform;
        BtnNext = transform.FindChild("BtnNext").GetComponent<Button>();
        BtnBack = transform.FindChild("BtnBack").GetComponent<Button>();
        UICreateRole = TfNodePos.FindChild("CreateRoleUI").GetComponent<CreateRoleUI>();
        UICreateRole.gameObject.SetActive(false);
    }
    
	// Use this for initialization
	void Start () {
        BtnNext.onClick.AddListener(OnBtnNextClick);
        BtnBack.onClick.AddListener(OnBtnBackClick);

        UpdateUI();
	}


    void UpdateUI()
    {
        QuestionCfg question = CreateSys.Instance.GetCreateQuestionByIndex(this.m_stepIndex - 1);
        if (question == null)
        {
            return;
        }

        QustionUnit script = GetQustionUnit(0);
        script.UpdateUI(question);
    }

    private void OnBtnNextClick()
    {
        Debug.Log("OnBtnNextClick, m_stepIndex:" + m_stepIndex);

        if (m_stepIndex < m_stepMax+1)
        { 
            m_stepIndex++;
        }

        if (m_stepIndex == m_stepMax)
        {
            // 创建列表
            Debug.Log("角色列表" + m_stepIndex);
            ChangeToRoleList();

            return;
        }


        if (m_stepIndex > m_stepMax)
        {
            Debug.Log("创建...");
        }
        // 切换到下一步代码:
        UpdateUI();
    }

    private void OnBtnBackClick()
    {
        Debug.Log("OnBtnBackClick, m_stepIndex:" + m_stepIndex);
        m_stepIndex--;
        if (m_stepIndex <= 0)
        {
            Application.LoadLevel("Login");
            return;
        }

        if (m_stepIndex == m_stepMax)
        {
            ChangeToQuestionList();
        }

        // 切换到上一步代码:
        UpdateUI();
    }

    private QustionUnit GetQustionUnit(int _index)
    {
        if (_index < m_QuestionUnit_lst.Count)
        {
            m_QuestionUnit_lst[_index].gameObject.SetActive(true);
            return m_QuestionUnit_lst[_index];
        }

        QustionUnit script = Common.Create<QustionUnit>(TfNodePos);
        m_QuestionUnit_lst.Add(script);

        return script;
    }

    private void ChangeToRoleList()
    {
        // 隐藏问题UI;
        QustionUnit script = GetQustionUnit(0);
        script.gameObject.SetActive(false);

        // 显示角色列表
        this.UICreateRole.gameObject.SetActive(true);
    }

    private void ChangeToQuestionList()
    {
        // 显示问题UI;
        QustionUnit script = GetQustionUnit(0);
        script.gameObject.SetActive(true);

        // 隐藏角色列表
        this.UICreateRole.gameObject.SetActive(false);
    }
}
