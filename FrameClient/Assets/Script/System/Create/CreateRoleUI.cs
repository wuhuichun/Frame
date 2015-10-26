using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CreateRoleUI : MonoBehaviour {

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

    void Awake()
    {
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
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
