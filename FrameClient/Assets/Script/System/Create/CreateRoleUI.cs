using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CreateRoleUI : MonoBehaviour {

    private Transform TfRoleList;
    private Transform TfRoleNode;

    private Text LbTitle;                               // 标题
    private Text LbName;                                // 姓名
    private Text LbAlias;                               // 字
    private Text LbSex;                                 // 性别
    private Text LbDesc;                                // 志

    private Text LbLeading;                             // 统帅
    private Text LbStrength;                            // 武力
    private Text LbWit;                                 // 智力
    private Text LbPolitics;                            // 政治
    private Text LbCharm;                               // 魅力

    private InputField TxtName;                         // 输入的名称
    private Button BtnRandom;                           // 随机

    private List<RoleCfg> m_Role_lst;
    private List<CreateRoleUnit> m_RoleUnit_lst;


    void Awake()
    {
        this.TfRoleList = transform.FindChild("RoleContainer");
        this.TfRoleNode = this.TfRoleList.FindChild("RoleList/Viewport/Content");


        Transform TfInfo = transform.FindChild("InfoContainer");
        this.LbTitle = TfInfo.FindChild("LbTitle").GetComponent<Text>();
        this.LbName = TfInfo.FindChild("LbName").GetComponent<Text>();
        this.LbAlias = TfInfo.FindChild("LbAlias").GetComponent<Text>();
        this.LbSex = TfInfo.FindChild("LbSex").GetComponent<Text>();
        this.LbDesc = TfInfo.FindChild("LbDesc").GetComponent<Text>();

        this.LbLeading = TfInfo.FindChild("LbLeading").GetComponent<Text>();
        this.LbStrength = TfInfo.FindChild("LbStrength").GetComponent<Text>();
        this.LbWit = TfInfo.FindChild("LbWit").GetComponent<Text>();
        this.LbPolitics = TfInfo.FindChild("LbPolitics").GetComponent<Text>();
        this.LbCharm = TfInfo.FindChild("LbCharm").GetComponent<Text>();

        Transform TfName = transform.FindChild("NameContainer");
        this.TxtName = TfName.FindChild("TxtName").GetComponent<InputField>();
        this.BtnRandom = TfName.FindChild("BtnRandom").GetComponent<Button>();
    }

	// Use this for initialization
	void Start () {
        // 获取显示列表
        m_Role_lst = CreateSys.Instance.GetChoiceHero();

        // 刷新UI
        RefreshUI();

       

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

    public void OnSelectIndexUpdate() {
        RefreshRoleInfoUI();
    }

    private void RefreshUI()
    {
        RefreshRoleListUI();

        RefreshRoleInfoUI();
    }

    private void RefreshRoleListUI()
    {
        for (int i = 0; i < m_Role_lst.Count; i++)
        {
            CreateRoleUnit script = this.GetCreateRoleUnit(i);
            script.UpdateUI(i, m_Role_lst[i]);
           
        }
    }

    private CreateRoleUnit GetCreateRoleUnit(int _index)
    {
        if (m_RoleUnit_lst == null)
            m_RoleUnit_lst = new List<CreateRoleUnit>();

        if (m_RoleUnit_lst.Count > _index)
        {
            m_RoleUnit_lst[_index].gameObject.SetActive(false);
            return m_RoleUnit_lst[_index];
        }

        CreateRoleUnit script = Common.Create<CreateRoleUnit>(this.TfRoleNode);
        
        m_RoleUnit_lst.Add(script);

        return script;
    }

    private void RefreshRoleInfoUI()
    {
        RoleCfg Role = CreateSys.Instance.GetSelectRole();

        this.LbName.text = Role.Name;
        this.LbAlias.text = Role.Alias;
        this.LbSex.text = Define.TEXT_SEX((eSex)Role.Sex);
        this.LbDesc.text = Role.Desc;

        this.LbLeading.text = Role.Leading.ToString();
        this.LbStrength.text = Role.Strength.ToString();
        this.LbWit.text = Role.Wit.ToString();
        this.LbPolitics.text = Role.politics.ToString();
        this.LbCharm.text = Role.Charm.ToString();
    }


}
