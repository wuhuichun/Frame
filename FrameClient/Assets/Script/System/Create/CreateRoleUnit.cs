using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CreateRoleUnit : MonoBehaviour {

    private Button BtnSelf;             // 自身按钮
    private Image SpBg;                 // 背景
    private Text LbName;                // 姓名

    private int m_index;
    private RoleCfg m_Data;

    public int Index
    {
        get { return m_index; }
        set { m_index = value; }
    }


    void Awake()
    {
        BtnSelf = gameObject.GetComponent<Button>();
        SpBg = transform.FindChild("SpBg").GetComponent<Image>();
        LbName = transform.FindChild("LbName").GetComponent<Text>();

    }

    void Start()
    {
        BtnSelf.onClick.AddListener(this.OnBtnSelfClick);
    }


    void OnEnable()
    {
        Debug.Log("==>> OnEnable");
        // 注册监听
        CreateSys.Instance.SelectIndexCall += OnSelectIndexUpdate;
    }
    void OnDisable()
    {
        Debug.Log("==>> OnDisable");
        CreateSys.Instance.SelectIndexCall -= OnSelectIndexUpdate;
    }
    public void OnSelectIndexUpdate()
    {
        RefreshSelectState();
    }

    public void UpdateUI(int _index, RoleCfg _Data)
    {
        Index = _index;
        m_Data = _Data;

        RefreshUI();
    }

    void RefreshUI()
    {
        if (m_Data == null)
            return;

        LbName.text = m_Data.Name;

        RefreshSelectState();
    }

    public void OnBtnSelfClick()
    {
        this.SpBg.color = Color.green;
        CreateSys.Instance.SelectIndex = this.Index;
    }

    private void RefreshSelectState()
    {
        if (CreateSys.Instance.SelectIndex == this.Index)
            this.SpBg.color = Color.green;
        else
            this.SpBg.color = Color.white;
    }
}
