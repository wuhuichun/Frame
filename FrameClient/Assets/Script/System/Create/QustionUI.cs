using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class QustionUI : MonoBehaviour {
    private Transform TfNodePos;
    private Button BtnNext;
    private Button BtnBack;


    private int m_stepIndex = 1;        // 当前步数
    private int m_stepMax = 4;          // 最大步数

    void Awake()
    {
        TfNodePos = transform.FindChild("NodePos").transform;
        BtnNext = transform.FindChild("BtnNext").GetComponent<Button>();
        BtnBack = transform.FindChild("BtnBack").GetComponent<Button>();
    }
    
	// Use this for initialization
	void Start () {
        BtnNext.onClick.AddListener(OnBtnNextClick);
        BtnBack.onClick.AddListener(OnBtnBackClick);
	}

    private void OnBtnNextClick()
    {
        Debug.Log("OnBtnNextClick");
        m_stepIndex++;

        // 切换到下一步代码:


        if (m_stepIndex > m_stepMax)
        {
            // 创建列表    
        }
    }

    private void OnBtnBackClick()
    {
        Debug.Log("OnBtnBackClick");
        m_stepIndex--;

        // 切换到上一步代码:


        if (m_stepIndex <= 0)
        {
            Application.LoadLevel("Login");
        }
    }

}
