using UnityEngine;
using System.Collections;

public class Define{
    /// <summary>
    /// 常量
    /// </summary>
    /// 
    public const string SERVER_IP = "192.168.1.107";
    public const int SERVER_PORT = 9527;

    public const int MSG_BLOCK = 100;                // 消息段, 每个系统一个消息段, 一段100个消息;
    public const int MSG_C2S_BEGIN = 0;              //
    public const int MSG_S2C_BEGIN = 40000;

    /// <summary>
    /// 下面是获取常亮或者枚举的中文映射函数
    /// </summary>
    public static string TEXT_SEX(eSex _sex){
        switch(_sex){
            case eSex.Male:
                return "男";
            case eSex.Female:
                return "女";
            default: return "未映射";
        }
    }

}

/// <summary>
/// 枚举
/// </summary>

// 性别
public enum eSex
{
    Male = 1,                                       // 男       
    Female,                                         // 女
}

// 角色类型
public enum eRoleType
{
    King = 1,                                       // 君主
    Wu,                                             // 武士
    Wen,                                            // 文士  
}

// 角色性格
public enum eTrait
{
    Utility = 1,                                    // 功利
    Justice,                                        // 正义
}

//// 州
public enum eState
{
    Si = 1,                                         // 司州
    Qing,                                           // 青州
    Xu,                                             // 徐州
    Yan,                                            // 兖州
    Yu,                                             // 豫州
    You,                                            // 幽州
    Ji,                                             // 冀州
    Bing,                                           // 并州
    Jing,                                           // 荆州
    Yang,                                           // 扬州
    Yi,                                             // 凉州
    Jiao,                                           // 益州
    Yong,                                           // 雍州
}