using UnityEngine;
using System.Collections;

public enum eCmd
{
    Hello = 0x00,
}


public class Message {

    private int m_len = 0;
    public int Len{
        get { return m_len; }
        set { m_len = value; }
    }

    private eCmd m_cmd = 0;
    public eCmd Cmd
    {
        get { return m_cmd; }
        set { m_cmd = value; }
    }

    // YU_TODO: 临时
    public string content = "";


    public void Send()
    {
        MsgQunue.Instance.AddSendMsg(this);
    }



}
