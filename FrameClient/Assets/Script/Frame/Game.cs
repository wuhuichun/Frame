using UnityEngine;
using System;
using System.Text;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using System;


public class Game{

    public static Game Instance = new Game();

    private bool m_isInit = false; 

    private Net m_Net;

    System.Random m_Rand;

    public System.Random Rand
    {
        get { return m_Rand; }
    }

    // 游戏初始化
    public void Init()
    {
        m_isInit = true;
        m_Rand = new System.Random((int)DateTime.Now.Millisecond);
        

    }

    public void InitNet()
    {
        // 网络初始化
        this.GetNet().InitNetWork();

    }

    // 游戏主循环
    public void MainLoop()
    {
        // 网络循环
        this.GetNet().NetWorkLoop();
    }

    // 游戏扫尾
    public void Dispose()
    {
        if (IsInit()) { 
            this.GetNet().Dispose();
        }
    }

    // 是否初始化
    public bool IsInit()
    {
        return m_isInit;
    }

    private Net GetNet()
    {
        if (m_Net == null)
        {
            m_Net = new Net();
        }

        return m_Net;
    }

}
