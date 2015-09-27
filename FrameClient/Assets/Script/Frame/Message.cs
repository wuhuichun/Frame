using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

public class Message
{
    #region 字段
    private eCmd m_cmd = 0;
    private int m_len = 0;
    private byte[] m_StartFlag = new byte[2] { 0x02, 0x02 };
    private byte[] m_EndFlag = new byte[2] { 0x03, 0x03 };
    private List<byte> m_innerContent = new List<byte>();
    private int m_position;
    #endregion

    #region 属性
    public int Len
    {
        get
        {
            return m_len;
        }
        set
        {
            m_len = value;
        }
    }

    public eCmd Cmd
    {
        get
        {
            return m_cmd;
        }
        set
        {
            m_cmd = value;
        }
    }

    //只是内容
    public byte[] Content
    {
        get
        {
            return m_innerContent.ToArray();
        }
        set
        {
            m_innerContent = value.ToList();
            m_position = 0;
        }
    }
    #endregion

    #region 公有方法
    public void AddInt(int param)
    {
        var bytes = BitConverter.GetBytes(param);

        m_innerContent.AddRange(bytes);
    }

    public void AddLong(long param)
    {
        var bytes = BitConverter.GetBytes(param);

        m_innerContent.AddRange(bytes);
    }

    public void AddFloat(float param)
    {
        var bytes = BitConverter.GetBytes(param);

        m_innerContent.AddRange(bytes);
    }

    public void AddShort(short param)
    {
        var bytes = BitConverter.GetBytes(param);

        m_innerContent.AddRange(bytes);
    }

    public void AddChar(char param)
    {
        var bytes = BitConverter.GetBytes(param);

        m_innerContent.AddRange(bytes);
    }

    public void AddString(string param)
    {
        var len = Convert.ToInt16(param.Length);

        var strLenBytes = BitConverter.GetBytes(len);

        var bytes = Encoding.ASCII.GetBytes(param);

        m_innerContent.AddRange(strLenBytes);
        m_innerContent.AddRange(bytes);
    }

    public void AddBool(bool param)
    {
        var bytes = BitConverter.GetBytes(param);
        m_innerContent.AddRange(bytes);
    }

    public int GetInt()
    {
        var intLen = sizeof(int);

        var bytes = m_innerContent.GetRange(m_position, intLen).ToArray();

        m_position += intLen;

        return BitConverter.ToInt32(bytes, bytes.Length);
    }

    public long GetLong()
    {
        var longLen = sizeof(long);

        var bytes = m_innerContent.GetRange(m_position, longLen).ToArray();

        m_position += longLen;

        return BitConverter.ToInt64(bytes, bytes.Length);
    }

    public float GetFloat()
    {
        var floatLen = sizeof(float);

        var bytes = m_innerContent.GetRange(m_position, floatLen).ToArray();

        m_position += floatLen;

        return BitConverter.ToSingle(bytes, bytes.Length);
    }

    public short GetShort()
    {
        var shortLen = sizeof(short);

        var bytes = m_innerContent.GetRange(m_position, shortLen).ToArray();

        m_position += shortLen;

        return BitConverter.ToInt16(bytes, bytes.Length);
    }

    public char GetChar()
    {
        var charLen = sizeof(char);

        var bytes = m_innerContent.GetRange(m_position, charLen).ToArray();

        m_position += charLen;

        return BitConverter.ToChar(bytes, bytes.Length);
    }

    public string GetString()
    {
        //先获取长度
        var strLenBytes = m_innerContent.GetRange(m_position, sizeof(Int16)).ToArray();

        m_position += sizeof(Int16);

        var strLen = BitConverter.ToInt16(strLenBytes, strLenBytes.Length);

        //获取字符串
        var stringBytes = m_innerContent.GetRange(m_position, strLen).ToArray();

        m_position += strLen;

        return Encoding.ASCII.GetString(stringBytes);
    }

    public bool GetBool()
    {
        var boolLen = sizeof(bool);

        var bytes = m_innerContent.GetRange(m_position, boolLen).ToArray();

        m_position += boolLen;

        return BitConverter.ToBoolean(bytes, bytes.Length);
    }


    public void Send()
    {
        Debug.Log("Msg.Send, cmd:" + (int)this.m_cmd);
        MsgQunue.Instance.AddSendMsg(this);
    }

    //打包
    public byte[] Package()
    {

        var lenBytes = GetMessageLenBytes();
        var cmdBytes = GetCmdBytes();

        var tempList = new List<byte>();

        tempList.AddRange(m_StartFlag);
        tempList.AddRange(lenBytes);
        tempList.AddRange(cmdBytes);
        tempList.AddRange(Content);
        tempList.AddRange(m_EndFlag);

        return tempList.ToArray();
    }

    public bool UnPackage(byte[] bytes)
    {
        //长度不对解包失败
        if (bytes.Length - 4 > 640)
        {
            return false;
        }

        //头不对失败
        if (!CheckHead(bytes))
        {
            return false;
        }

        m_len = GetMessageLen(bytes);

        m_cmd = GetCmd(bytes);

        Content = GetContent(bytes);

        return true;
    }
    #endregion

    #region 私有方法
    private int GetMessageLen(byte[] bytes)
    {
        byte[] lenBytes = new byte[4];

        bytes.ToList().CopyTo(2, lenBytes, 0, 4);

        return BitConverter.ToInt32(lenBytes, lenBytes.Length);
    }

    private eCmd GetCmd(byte[] bytes)
    {
        byte[] cmdBytes = new byte[4];

        bytes.ToList().CopyTo(6, cmdBytes, 0, 4);

        var value = BitConverter.ToInt32(cmdBytes, cmdBytes.Length);

        return (eCmd)value;
    }

    private byte[] GetContent(byte[] bytes)
    {
        var contentLen = bytes.Length - 2 - 4 - 4 - 2;

        byte[] contentBytes = new byte[contentLen];

        bytes.ToList().CopyTo(10, contentBytes, 0, contentBytes.Length);

        return contentBytes;
    }

    public bool Check()
    {
        //减去头和尾共4字节
        if (Package().Length - 4 > 640)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private byte[] GetMessageLenBytes()
    {
        //计算长度
        var len = Convert.ToInt32(4 + 4 + Content.Length);

        var realLenBytes = BitConverter.GetBytes(len);

        return realLenBytes;
    }

    private byte[] GetCmdBytes()
    {
        var cmdValue = Convert.ToInt32(m_cmd);

        var realCmdBytes = BitConverter.GetBytes(cmdValue);

        return realCmdBytes;

    }

    private bool CheckHead(byte[] bytes)
    {
        for (int i = 0; i < 2; i++)
        {
            if (bytes[i] != 0x02)
            {
                return false;
            }
        }

        return true;
    }
    #endregion



}
