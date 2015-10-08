using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LoginUI : MonoBehaviour {

    private InputField TxtUser;
    private InputField TxtPwd;
    private Button BtnEnsure;
    private Button BtnExit;

    void Awake()
    {
        // 控件赋值
        TxtUser = transform.FindChild("TxtUer").GetComponent<InputField>();
        TxtPwd = transform.FindChild("TxtPwd").GetComponent<InputField>();
        BtnEnsure = transform.FindChild("BtnEnsure").GetComponent<Button>();
        BtnExit = transform.FindChild("BtnExit").GetComponent<Button>();

        // 绑定控件事件
        BtnEnsure.onClick.AddListener(OnBtnEnsureClick);
        BtnExit.onClick.AddListener(OnBtnExitClick);
    }

	// Use this for initialization
	void Start () {
        Init();
	}

    private void Init()
    {
        TxtUser.text = "wy001";
        TxtPwd.text = "123";
    }

    // 确认按钮响应事件
    private void OnBtnEnsureClick()
    {
        Debug.Log("On OnBtnEnsureClick, user:" + TxtUser.text + " / pwd:" + TxtPwd.text);
        string user = TxtUser.text.Trim();
        string pwd = TxtPwd.text.Trim();

        if (string.IsNullOrEmpty(user))
        {
            Debug.LogError("请输入账号");
            return;
        }

        if (string.IsNullOrEmpty(user))
        {
            Debug.LogError("请输入密码");
            return;
        }

        Login.Instance.SetInfo(user, pwd);
        Login.Instance.TryLogin();
    }

    // 确认按钮响应事件
    private void OnBtnExitClick()
    {
        Debug.Log("On OnBtnExitClick");
        Application.Quit();
    }
}
