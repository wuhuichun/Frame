using UnityEngine;
using System;
using System.Text;
using System.Threading;
using System.Collections;
using System.Collections.Generic;

public class Net
{

    private SocketClient m_Server = new SocketClient();         // 服务器

    private MsgRouter m_MsgRouter = new MsgRouter();            // 消息路由


    private SocketClient GetServer()
    {
        return m_Server;
    }

    private MsgRouter GetMsgRouter()
    {
        return m_MsgRouter;
    }

    public void InitNetWork()
    {
        // 注册消息路由
        m_MsgRouter.Init();

        // 启动网络
        string host = Define.SERVER_IP;
        int port = Define.SERVER_PORT;

        if (!m_Server.IsConnect()) { 
            m_Server.Init(host, port);
            m_Server.Connect();
        }
        Debug.Log("Connect Success!");

        Thread thread1 = new Thread(RecvLoop);
        Debug.Log("Thread Start");
        thread1.Start();

    }

    public void Send2NetWork()
    {
        while (!MsgQunue.Instance.IsSendQueEmpty())
        {
            Message Msg = MsgQunue.Instance.PopSendMsg();
            byte[] sendBuf = Msg.Encode();
            Debug.Log("Send a Msg, cmd:" + (int)Msg.Cmd + " \tlen:" + Msg.Len);
            this.GetServer().Send(sendBuf);
        }

    }

    public void RecvLoop()
    {
        Debug.Log("wait for Recv");
        while (true)
        {
            Debug.Log("wait for Recv1");
            if(!this.GetServer().IsConnect()){
                continue;
            }
            Debug.Log("wait for Recv2");
            Byte[] recvBuf = this.GetServer().Recv();
            if (recvBuf == null)
            {
                continue;
            }
            Debug.Log("wait for Recv3");
            Message Msg = new Message();
            if (Msg.Decode(recvBuf))
            {
                Debug.Log("wait for Recv4");
                MsgQunue.Instance.AddRecvMsg(Msg);
            }
        }
    }

    public void NetWorkLoop()
    {
        if (!MsgQunue.Instance.IsRecvQueEmpty())
        {
            HandleAMsg(MsgQunue.Instance.PopRecvMsg());
        }

        if (!MsgQunue.Instance.IsSendQueEmpty())
        {
            Send2NetWork();
        }
    }

    private void HandleAMsg(Message Msg)
    {
        m_MsgRouter.Dispatch(Msg);
    }

    public void Dispose()
    {
        if (this.m_Server.IsConnect()) { 
            this.m_Server.Close();
        }
    }
}
