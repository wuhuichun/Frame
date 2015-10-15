using UnityEngine;
using System;
using System.Collections;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void TestFun1()
    {
        //Debug.Log("TestFun1()");
        //Game.Instance.Init();
        //byte[] b4 = new byte[] { 0x0C, 0x00, 0x00, 0x00 };
        //int a = BitConverter.ToInt32(b4, 0);

        //Debug.Log("a: " + a.ToString());
    }

    public void TestFun2()
    {
        //Debug.Log("TestFun2()");
        
        Message Msg = new Message(eCmd.C2S_Test_Hello);
        Msg.AddString("hello");
        Msg.AddShort(5);

        Debug.Log("SendMsg, len:" + Msg.Len);
        Msg.Send();


    }

    public void TestFun3()
    {
        Debug.Log("TestFun3()");


    }

    void OnDisable()
    {
        Debug.Log("OnDisable");
        Game.Instance.Dispose();
    }
}
