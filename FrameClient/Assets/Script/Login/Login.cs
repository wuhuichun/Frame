using UnityEngine;
using System.Collections;

public class Login{
    public readonly static Login Instance = new Login();

    string m_user;
    string m_pwd;

    public void SetInfo(string _user, string _pwd)
    {
        m_user = _user;
        m_pwd = _pwd;
    }

    public void TryLogin()
    {
        Debug.Log("尝试登录, Ip:" + Define.SERVER_IP + ", Port:" + Define.SERVER_PORT
            + ", user:" + m_user + ", pwd:" + m_pwd);

    }

}
