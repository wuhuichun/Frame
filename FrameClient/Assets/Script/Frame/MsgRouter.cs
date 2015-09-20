using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;



public enum eCmd
{
    C2S = Define.MSG_C2S_BEGIN,
    /// <summary>
    ///  C2S的消息
    /// </summary>

    C2S_System = Define.MSG_BLOCK * 0x00,                               // 系统消息段
    C2S_System_001,                                                     // 系统001消息
    C2S_System_002,                                                     // 系统002消息

    C2S_Test = Define.MSG_BLOCK * 0x01,                                 // 测试系统消息段
    C2S_Test_Hello,                                                     // 测试系统Hello消息



    S2C = Define.MSG_S2C_BEGIN,
    /// <summary>
    ///  S2C的消息
    /// </summary>

    S2C_System = Define.MSG_S2C_BEGIN + Define.MSG_BLOCK * 0x00,        // 系统消息段
    S2C_System_001,                                                     // 系统001消息
    S2C_System_002,                                                     // 系统002消息

    S2C_Test = Define.MSG_S2C_BEGIN + Define.MSG_BLOCK * 0x01,          // 测试系统消息段
    S2C_Test_Hello,                                                     // 测试系统Hello消息

}


public class MsgRouter {

    private Dictionary<eCmd, Action<Message>> m_MsgCallDic;

    public void Dispatch(Message _Msg)
    {
        Debug.Log("U recv a Msg, cmd: " + _Msg.Cmd.ToString());

        if (m_MsgCallDic.ContainsKey(_Msg.Cmd))
        {
            m_MsgCallDic[_Msg.Cmd](_Msg);
        }

    }

    public void Init()
    {
        m_MsgCallDic = new Dictionary<eCmd,Action<Message>>(1000);
        RegisterAllMsg();

    }

    public void PrintHello(Message Msg)
    {
        Debug.Log("Hello");
    }

    private void Register(eCmd cmd, Action<Message> fun)
    {
        if (m_MsgCallDic.ContainsKey(cmd))
        {
            Debug.LogError("Msg Register Error. cmd repeated. cmd:" + cmd.ToString());
            return;
        }
        m_MsgCallDic[cmd] = fun;
    }
 
    private void RegisterAllMsg()
    {
        Register(eCmd.S2C_Test_Hello, PrintHello);
        // TODO: 请在下面注册你的消息
    }

}
