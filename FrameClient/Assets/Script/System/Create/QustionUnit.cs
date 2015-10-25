using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class QustionUnit : MonoBehaviour {


    private Text TxtQuestion;
    private Transform TfAnsowerPos;



    private QuestionCfg m_Data;
    private List<AnsowerCfg> m_AnswerCfg_lst = new List<AnsowerCfg>();
    private List<AnswerUnit> m_AnswerUnit_lst = new List<AnswerUnit>();

    void Awake()
    {
        TxtQuestion = transform.FindChild("Text").GetComponent<Text>();
        TfAnsowerPos = transform.FindChild("AnswerList/Viewport/Content");
    }



    public void UpdateUI(QuestionCfg _data)
    {
        m_Data = _data;
        m_AnswerCfg_lst.Clear();

        AnsowerCfg ACfg1 = new AnsowerCfg();
        ACfg1.AnsowerText = m_Data.AnsowerText1;
        ACfg1.AnsowerValue = m_Data.Ansower1;
        m_AnswerCfg_lst.Add(ACfg1);

        if (!string.IsNullOrEmpty(m_Data.AnsowerText2))
        { 
            AnsowerCfg tempCfg = new AnsowerCfg();
            tempCfg.AnsowerText = m_Data.AnsowerText2;
            tempCfg.AnsowerValue = m_Data.Ansower2;
            m_AnswerCfg_lst.Add(tempCfg);
        }

        if (!string.IsNullOrEmpty(m_Data.AnsowerText3))
        { 
            AnsowerCfg tempCfg = new AnsowerCfg();
            tempCfg.AnsowerText = m_Data.AnsowerText3;
            tempCfg.AnsowerValue = m_Data.Ansower3;
            m_AnswerCfg_lst.Add(tempCfg);
        }

        if (!string.IsNullOrEmpty(m_Data.AnsowerText4))
        { 
            AnsowerCfg tempCfg = new AnsowerCfg();
            tempCfg.AnsowerText = m_Data.AnsowerText4;
            tempCfg.AnsowerValue = m_Data.Ansower4;
            m_AnswerCfg_lst.Add(tempCfg);
        }

        if (!string.IsNullOrEmpty(m_Data.AnsowerText5))
        { 
            AnsowerCfg tempCfg = new AnsowerCfg();
            tempCfg.AnsowerText = m_Data.AnsowerText5;
            tempCfg.AnsowerValue = m_Data.Ansower5;
            m_AnswerCfg_lst.Add(tempCfg);
        }

        if (!string.IsNullOrEmpty(m_Data.AnsowerText6))
        { 
            AnsowerCfg tempCfg = new AnsowerCfg();
            tempCfg.AnsowerText = m_Data.AnsowerText6;
            tempCfg.AnsowerValue = m_Data.Ansower6;
            m_AnswerCfg_lst.Add(tempCfg);
        }

        if (!string.IsNullOrEmpty(m_Data.AnsowerText7))
        { 
            AnsowerCfg tempCfg = new AnsowerCfg();
            tempCfg.AnsowerText = m_Data.AnsowerText7;
            tempCfg.AnsowerValue = m_Data.Ansower7;
            m_AnswerCfg_lst.Add(tempCfg);
        }

        if (!string.IsNullOrEmpty(m_Data.AnsowerText8))
        { 
            AnsowerCfg tempCfg = new AnsowerCfg();
            tempCfg.AnsowerText = m_Data.AnsowerText8;
            tempCfg.AnsowerValue = m_Data.Ansower8;
            m_AnswerCfg_lst.Add(tempCfg);
        }

        if (!string.IsNullOrEmpty(m_Data.AnsowerText9))
        { 
            AnsowerCfg tempCfg = new AnsowerCfg();
            tempCfg.AnsowerText = m_Data.AnsowerText9;
            tempCfg.AnsowerValue = m_Data.Ansower9;
            m_AnswerCfg_lst.Add(tempCfg);
        }

        if (!string.IsNullOrEmpty(m_Data.AnsowerText10))
        { 
            AnsowerCfg tempCfg = new AnsowerCfg();
            tempCfg.AnsowerText = m_Data.AnsowerText10;
            tempCfg.AnsowerValue = m_Data.Ansower10;
            m_AnswerCfg_lst.Add(tempCfg);
        }

        if (!string.IsNullOrEmpty(m_Data.AnsowerText11))
        { 
            AnsowerCfg tempCfg = new AnsowerCfg();
            tempCfg.AnsowerText = m_Data.AnsowerText11;
            tempCfg.AnsowerValue = m_Data.Ansower11;
            m_AnswerCfg_lst.Add(tempCfg);
        }

        if (!string.IsNullOrEmpty(m_Data.AnsowerText12))
        { 
            AnsowerCfg tempCfg = new AnsowerCfg();
            tempCfg.AnsowerText = m_Data.AnsowerText12;
            tempCfg.AnsowerValue = m_Data.Ansower12;
            m_AnswerCfg_lst.Add(tempCfg);
        }

        if (!string.IsNullOrEmpty(m_Data.AnsowerText13))
        { 
            AnsowerCfg tempCfg = new AnsowerCfg();
            tempCfg.AnsowerText = m_Data.AnsowerText13;
            tempCfg.AnsowerValue = m_Data.Ansower13;
            m_AnswerCfg_lst.Add(tempCfg);
        }

        if (!string.IsNullOrEmpty(m_Data.AnsowerText14))
        { 
            AnsowerCfg tempCfg = new AnsowerCfg();
            tempCfg.AnsowerText = m_Data.AnsowerText14;
            tempCfg.AnsowerValue = m_Data.Ansower14;
            m_AnswerCfg_lst.Add(tempCfg);
        }

        if (!string.IsNullOrEmpty(m_Data.AnsowerText16))
        { 
            AnsowerCfg tempCfg = new AnsowerCfg();
            tempCfg.AnsowerText = m_Data.AnsowerText16;
            tempCfg.AnsowerValue = m_Data.Ansower16;
            m_AnswerCfg_lst.Add(tempCfg);
        }

        if (!string.IsNullOrEmpty(m_Data.AnsowerText16))
        { 
            AnsowerCfg tempCfg = new AnsowerCfg();
            tempCfg.AnsowerText = m_Data.AnsowerText16;
            tempCfg.AnsowerValue = m_Data.Ansower16;
            m_AnswerCfg_lst.Add(tempCfg);
        }


        RefreshUI();
    }

    void RefreshUI()
    {
        if (m_Data == null)
        {
            return;
        }

        Log.Info("RefreshUI, m_Data.ID:" + m_Data.ID);

        TxtQuestion.text = this.m_Data.Capital;

        for (int i = 0; i < m_AnswerUnit_lst.Count; i++)
        {
            m_AnswerUnit_lst[i].gameObject.SetActive(false);
        }


        for (int i = 0; i < m_AnswerCfg_lst.Count; i++)
        {
            UpdataAnswer(i);
        }
    }

    private AnswerUnit GetAnsowerUnit(int _index)
    {
        if (_index < m_AnswerUnit_lst.Count)
        {
            m_AnswerUnit_lst[_index].gameObject.SetActive(true);
            return m_AnswerUnit_lst[_index];
        }

        GameObject go = Common.CreateGO(TfAnsowerPos, "AnswerUnit");
        AnswerUnit script = go.AddComponent<AnswerUnit>();

        m_AnswerUnit_lst.Add(script);

        return script;
    }

    private void UpdataAnswer(int _index)
    {
        AnswerUnit script = GetAnsowerUnit(_index);
        script.UpdateUI(m_AnswerCfg_lst[_index]);
    }

}
