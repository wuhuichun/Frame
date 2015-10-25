using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AnswerUnit : MonoBehaviour {
    private Text TxtAnsower;
    private Toggle TogSelect;

    private AnsowerCfg m_Data;


    void Awake()
    {
        TxtAnsower = transform.FindChild("Label").GetComponent<Text>();
        TogSelect = transform.GetComponent<Toggle>();
    }


    public void UpdateUI(AnsowerCfg data)
    {
        this.m_Data = data;

        RefreshUI();
    }

    private void RefreshUI()
    {
        if (m_Data == null)
        {
            return;
        }

        this.TxtAnsower.text = m_Data.AnsowerText;
        this.TogSelect.group = transform.parent.GetComponent<ToggleGroup>();
        this.TogSelect.isOn = (m_Data.AnsowerValue == 1);
    }

}
