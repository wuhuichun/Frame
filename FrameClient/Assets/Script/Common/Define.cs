using UnityEngine;
using System.Collections;

public class Define{
    public const string SERVER_IP = "192.168.1.107";
    public const int SERVER_PORT = 9527;

    public const int MSG_BLOCK = 100;                // 消息段, 每个系统一个消息段, 一段100个消息;
    public const int MSG_C2S_BEGIN = 0;              //
    public const int MSG_S2C_BEGIN = 40000;

}

// 性别
enum eSex
{
    Male = 1,                                       // 男       
    Female,                                         // 女
}

// 角色类型
enum eRoleType
{
    King = 1,                                       // 君主
    Wu,                                             // 武士
    Wen,                                            // 文士  
}

// 角色性格
enum eDisposition
{
    Utility = 1,                                    // 功利
    Justice,                                        // 正义
}

//// 州
//enum eState
//{

//}