using UnityEngine;
using System;
using System.Text;
using System.Threading;
using System.Collections;
using System.Collections.Generic;

public class Net{

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
        string host = "192.168.1.107";
        int port = 9527;

        m_Server.Init(host, port);
        m_Server.Connect();
        Debug.Log("Connect Success!");

        Thread thread1 = new Thread(RecvLoop);
        Debug.Log("Thread Start");
        thread1.Start();

    }

    public void Send2NetWork()
    {
        while (MsgQunue.Instance.IsSendQueEmpty())
        {
            Message Msg = MsgQunue.Instance.PopSendMsg();
            byte[] sendBuf = Encoding.ASCII.GetBytes(Msg.content);
            this.GetServer().Send(sendBuf);
        }

    }

    public void RecvLoop()
    {
        while (this.GetServer().IsConnect())
        {
            Byte[] recvBuf = this.GetServer().Recv();
            string recvStr = Encoding.ASCII.GetString(recvBuf);
            Message Msg = new Message();
            Msg.content = recvStr;
            MsgQunue.Instance.AddRecvMsg(Msg);
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
}
