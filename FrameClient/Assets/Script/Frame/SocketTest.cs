using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using System.Threading;

public class SocketTest : MonoBehaviour {

    SocketClient Server;

    bool isConnect = false;
    bool isRecving = false;

    Queue<string> MsgQue;

	// Use this for initialization
	void Start () {

        MsgQue = new Queue<string>();
        
	}
	
	// Update is called once per frame
	void Update () {
        if (isConnect)
        {


            if (MsgQue.Count > 0)
            {
                Debug.Log("GetStr: " + MsgQue.Dequeue());
               
            }

            //if (!isRecving) 
            //{
                /*
                isRecving = true;
                isConnect = false;
                Debug.Log("try Thread");
                ThreadStart thloop = new ThreadStart(WaitRecvLoop);
                Thread thread1 = new Thread(thloop);
                thread1.Start();
                */
            //}

           // StartCoroutine(WaitRecv());
        }
	}

    public void Connect()
    {
        string host = "192.168.1.102";
        int port = 9527;
        Server = new SocketClient();

        Server.Init(host, port);
        Server.Connect();
        isConnect = true;
    }

    public void Send()
    {

        string sendStr = "5";
        byte[] sendBuf = Encoding.ASCII.GetBytes(sendStr);
        Server.Send(sendBuf);
        Debug.Log("Send already. str:" + sendStr);


    }

    public void WaitRecvLoop()
    {
//        Console.WriteLine("start WaitRecvLoop()");
        //Debug.Log("start WaitRecvLoop()");
        //while (true) { 
            Byte[] recvBuf = Server.Recv();

            string recvStr = Encoding.ASCII.GetString(recvBuf);
            MsgQue.Enqueue(recvStr);
            //Console.WriteLine("Recv sth. str:" + recvStr);

 //           if (recvBuf.Length > 0)
   //         {

     //           Debug.Log("Recv sth. str:" + recvStr);
       //     }
        //}
    }




    public void Close()
    {
        Debug.Log("try Thread");
        ThreadStart thloop = new ThreadStart(WaitRecvLoop);
        Thread thread1 = new Thread(thloop);
        thread1.Start();
        //Server.Close();
    }

    void Destroy()
    {
        Debug.Log("Destroy");
    }

    void Disable()
    {
        Debug.Log("Disable");
    }
}
