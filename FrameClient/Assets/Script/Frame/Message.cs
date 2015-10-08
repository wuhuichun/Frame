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
    private const int START_LEN = 2;
    private const int END_LEN = 2;
    private const int LEN_LEN = 4;
    private const int CMD_LEN = 4;
 

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

    // 内容 包括头尾 长度 消息头 具体内容
    public byte[] Content
    {
        get
        {
            return m_innerContent.ToArray();
        }
        set
        {
            m_innerContent = value.ToList();
            //m_position = 0;
        }
    }
    #endregion

    // 构建一个空
    public Message()
    {
    }

    // 构建一个发送包
    public Message(eCmd _cmd)
    {
        m_cmd = _cmd;
        m_len = LEN_LEN + CMD_LEN;         // START_LEN, END_LEN
    }
    #region 私有方法
    //打包
    public byte[] Encode()
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

    public bool Decode(byte[] bytes)
    {
        m_position = 0;
        Debug.Log("UnPackage 1:" + bytes.Length);

        // 验证头部
        if (!CheckHead(bytes))
        {
            return false;
        }
        m_position += START_LEN;

        // 获取长度
        Debug.Log("UnPackage 2");
        Len = DecodeLen(bytes);
        m_position += LEN_LEN;
        
        // 验证尾部
        Debug.Log("UnPackage 3, m_len:" + Len);
        if (!CheckEnd(bytes))
        {
            return false;
        }

        Debug.Log("UnPackage 4");
        m_cmd = DecodeCmd(bytes);
        m_position += CMD_LEN;

        Debug.Log("UnPackage 5, cmd:" + (int)Cmd);
        Content = GetContent(bytes);

        return true;
    }

    public void Send()
    {
        Debug.Log("Msg.Send, cmd:" + (int)this.m_cmd);
        MsgQunue.Instance.AddSendMsg(this);
    }
  
    private int DecodeLen(byte[] bytes)
    {
        byte[] lenBytes = new byte[LEN_LEN];

        bytes.ToList().CopyTo(m_position, lenBytes, 0, LEN_LEN);

        int  ret = BitConverter.ToInt32(lenBytes, 0);
        return ret;
    }

    private eCmd DecodeCmd(byte[] bytes)
    {
        byte[] cmdBytes = new byte[CMD_LEN];

        bytes.ToList().CopyTo(m_position, cmdBytes, 0, CMD_LEN);
        var value = BitConverter.ToInt32(cmdBytes, 0);

        return (eCmd)value;
    }

    private byte[] GetContent(byte[] bytes)
    {
        //Debug.Log("m_len:" + m_len);
        //var contentLen = m_len + START_LEN + END_LEN;// bytes.Length - 2 - 4 - 4 - 2;

        //byte[] contentBytes = new byte[contentLen];

        //bytes.ToList().CopyTo(0, contentBytes, 0, contentBytes.Length);

        //return contentBytes;

        return bytes;
    }

    private byte[] GetMessageLenBytes()
    {
        //计算长度
        //var len = Convert.ToInt32(4 + 4 + Content.Length);
        Debug.Log("GetMessageLenBytes, m_len:" + m_len);
        var realLenBytes = BitConverter.GetBytes(m_len);

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
            if (bytes[i] != this.m_StartFlag[i])
            {
                Debug.Log("CheckHead, ret false");
                return false;
            }
        }
        return true;
    }

    public bool CheckEnd(byte[] bytes)
    {
        for (int i = 0; i < 2; i++)
        {
            int index = START_LEN + m_len + i;
            Debug.Log("CheckEnd index:" + index + " m_len:" + m_len);
            if (bytes[index] != this.m_EndFlag[i])
            {
                Debug.Log("CheckEnd, ret false");
                return false;
            }
        }

        return true;
    }

    #endregion

    #region 公有方法
    public void AddInt(int param)
    {
        var bytes = BitConverter.GetBytes(param);
        
        m_innerContent.AddRange(bytes);
        m_len += bytes.Length;
    }

    public void AddLong(long param)
    {
        var bytes = BitConverter.GetBytes(param);

        m_innerContent.AddRange(bytes);
        m_len += bytes.Length;
    }

    public void AddFloat(float param)
    {
        var bytes = BitConverter.GetBytes(param);

        m_innerContent.AddRange(bytes);
        m_len += bytes.Length;
    }

    public void AddShort(short param)
    {
        var bytes = BitConverter.GetBytes(param);

        m_innerContent.AddRange(bytes);
        m_len += bytes.Length;
    }

    public void AddChar(char param)
    {
        var bytes = BitConverter.GetBytes(param);

        m_innerContent.AddRange(bytes);
        m_len += bytes.Length;
    }

    public void AddString(string param)
    {
        var len = Convert.ToInt16(param.Length);

        var strLenBytes = BitConverter.GetBytes(len);

        var bytes = Encoding.ASCII.GetBytes(param);

        m_innerContent.AddRange(strLenBytes);
        m_innerContent.AddRange(bytes);
        m_len += len;
        m_len += bytes.Length;
    }

    public void AddBool(bool param)
    {
        var bytes = BitConverter.GetBytes(param);
        m_innerContent.AddRange(bytes);
        m_len += bytes.Length;
    }

    public int GetInt()
    {
        var intLen = sizeof(int);
        var bytes = m_innerContent.GetRange(m_position, intLen).ToArray();
        m_position += intLen;

        return BitConverter.ToInt32(bytes, 0);
    }

    public long GetLong()
    {
        var longLen = sizeof(long);

        var bytes = m_innerContent.GetRange(m_position, longLen).ToArray();

        m_position += longLen;

        return BitConverter.ToInt64(bytes, 0);
    }

    public float GetFloat()
    {
        var floatLen = sizeof(float);

        var bytes = m_innerContent.GetRange(m_position, floatLen).ToArray();

        m_position += floatLen;

        return BitConverter.ToSingle(bytes, 0);
    }

    public short GetShort()
    {
        var shortLen = sizeof(short);

        var bytes = m_innerContent.GetRange(m_position, shortLen).ToArray();

        m_position += shortLen;

        return BitConverter.ToInt16(bytes, 0);
    }

    public char GetChar()
    {
        var charLen = sizeof(char);

        var bytes = m_innerContent.GetRange(m_position, charLen).ToArray();

        m_position += charLen;

        return BitConverter.ToChar(bytes, 0);
    }

    public string GetString()
    {
        //先获取长度
        var strLenBytes = m_innerContent.GetRange(m_position, sizeof(Int16)).ToArray();

        m_position += sizeof(Int16);

        var strLen = BitConverter.ToInt16(strLenBytes, 0);

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

        return BitConverter.ToBoolean(bytes, 0);
    }


    #endregion

}
