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
        //Game.Instance.Init();

    }

    public void TestFun2()
    {
        Debug.Log("TestFun2()");
        /*
        Message Msg = new Message();
        Msg.Cmd = eCmd.Hello;
        Msg.Len = 8;
        Msg.content = "5";
        Msg.Send();
*/

    }

    public void TestFun3()
    {
        Debug.Log("TestFun3()");


    }
}
