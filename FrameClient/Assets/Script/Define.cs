using UnityEngine;
using System.Collections;

public class Define{
    public const string SERVER_IP = "192.168.1.107";
    public const int SERVER_PORT = 9527;

    public const int MSG_BLOCK = 100;                // 消息段, 每个系统一个消息段, 一段100个消息;
    public const int MSG_C2S_BEGIN = 0;
    public const int MSG_S2C_BEGIN = 40000;

}
