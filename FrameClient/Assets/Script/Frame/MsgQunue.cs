using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class MsgQunue{

    public static MsgQunue Instance = new MsgQunue();

    private Queue<Message> m_SendQue;
    private Queue<Message> m_RecvQue;

    private Queue<Message> GetSendQue()
    {
        if (m_SendQue == null)
        {
            m_SendQue = new Queue<Message>();
        }

        return m_SendQue;
    }

    private Queue<Message> GetRecvQue()
    {
        if (m_RecvQue == null)
        {
            m_RecvQue = new Queue<Message>();
        }

        return m_RecvQue;
    }

    public bool IsRecvQueEmpty()
    {
        return (this.GetRecvQue().Count == 0);
    }

    public bool IsSendQueEmpty()
    {
        return (this.GetSendQue().Count == 0);
    }

    public void AddSendMsg(Message _Msg){
        this.GetSendQue().Enqueue(_Msg);
    }

    public void AddRecvMsg(Message _Msg)
    {
        this.GetRecvQue().Enqueue(_Msg);
    }

    public Message PopRecvMsg()
    {
        Message Msg = this.GetRecvQue().Dequeue();
        return Msg;
    }

    public Message PopSendMsg()
    {
        Message Msg = this.GetSendQue().Dequeue();
        return Msg;
    }

	
}
