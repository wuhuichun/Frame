/*--------------------------------------------------------------------------------
file: Msg.h
use	: 封装一个Msg类。
ver :
	1.0 by whc. at 2014-10-26. anything call me.
memo:
	Msg = Message

	*： 消息头前缀
	L2C_  		: 代表 LoginServer 	to Client
	C2L_ 		: 代表 Client 		to LoginServer
	C2G_ 		: 代表 Client 		to GameServer
	G2C_ 		: 代表 GameServer 	to Client
	L2G_ 		: 代表 LoginServer 	to Client
	G2L_ 		: 代表 GameServer 	to LoginServer
--------------------------------------------------------------------------------*/


#ifndef MSG_H
#define MSG_H


#include <memory.h>
#include <string>

//using namespace std;

#define MSG_BLOCK 200 				// 将消息分段 每段的间隔 200
#define MSG_C2S_BEGIN 0
#define MSG_S2C_BEGIN 40000
#define MSG_LEN_MAX 640


// 消息头。
enum class eCmd{

	C2S = MSG_C2S_BEGIN,
	/// <summary>
	/// C2S Message
	/// </summary>
	C2S_System = MSG_BLOCK*0x00,
	C2S_System_001,
	C2S_System_002,

	C2S_Test = MSG_BLOCK*0x01,
	C2S_Test_Hello, 								// test msg



	/// <summary>
	/// C2S Message
	/// </summary>
	S2C = MSG_S2C_BEGIN,
	S2C_System = MSG_S2C_BEGIN + MSG_BLOCK * 0x00,
	S2C_System_001,
	S2C_System_002,

	S2C_Test = MSG_S2C_BEGIN + MSG_BLOCK * 0x01,
	S2C_Test_Hello, 								// test msg

};

class Message
{
private:

	// 将mp_buf 序列化
	void Encode();


public:
	Message();
	Message(eCmd _cmd);
	Message(const Message & _Msg);
	~Message();

	// 将消息发送出去, 需要和 AppendBuf() 配合使用
	void Send(int fd);

	// 将mp_buf 反序列化
	void Decode(char * _buf);


	//
	char * GetBuf();

	///
	//
	char GetChar();
	void AddChar(char _value);

	//
	std::string GetString();
	void AddString(std::string _value);

	//
	short GetShort();
	void AddShort(short _value);

	//
	int GetInt();
	void AddInt(int _value);

	//
	long long GetLong();
	void AddLong(long long _value);

	//
	float GetFloat();
	void AddFloat(float _value);

	//
	bool GetBool();
	void AddBool(bool _value);

public:

	size_t m_len;
		eCmd m_cmd; 			// 消息ID
	private:


	char * mp_content; 			// 消息内容缓冲区指针

	int m_pos;


};

#endif // MSG_H
