using UnityEngine;
using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

public class SocketClient{



    private IPAddress m_Ip;
    private IPEndPoint m_Ipe;
    private Socket m_sock;

    public void Init(string host, int port)
    {
        //string m_host = "127.0.0.1";
        //int port = 9527;

        // 创建
        m_Ip = IPAddress.Parse(host);
        m_Ipe = new IPEndPoint(m_Ip, port);
        m_sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

    }

    public void Connect()
    {
        Debug.Log("Contenting...");
        m_sock.Connect(m_Ipe);
        Debug.Log("Contented");
    }

    public void Send(byte[] buf)
    {
        m_sock.Send(buf, buf.Length, 0);
    }

    public byte[] Recv()
    {
        byte[] recvBytes = new byte[1024];
        int len;

        //Debug.Log("Try Recv...");
        len = m_sock.Receive(recvBytes, recvBytes.Length, 0);


        return recvBytes;        
    }

    public void Close()
    {
        this.m_sock.Shutdown(SocketShutdown.Both);
        this.m_sock.Close();
    }

}
