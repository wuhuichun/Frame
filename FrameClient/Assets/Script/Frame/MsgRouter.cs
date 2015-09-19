using UnityEngine;
using System.Collections;

public class MsgRouter {


    public static void Dispatch(Message Msg)
    {
        Debug.Log("U recv a Msg, cmd: " + Msg.Cmd.ToString());
        switch (Msg.Cmd)
        {
            case eCmd.Hello :
                Debug.Log("Hello");
                break;

            default: break;
        }

    }
}
