using UnityEngine;
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
        Debug.Log("TestFun1()");
        Game.Instance.Init();

    }

    public void TestFun2()
    {
        Debug.Log("TestFun2()");
        
        Message Msg = new Message();
        Msg.Cmd = eCmd.C2S_Test_Hello;
        Msg.AddInt(5);
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
