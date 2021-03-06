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

#define MSG_BLOCK 100 				// 将消息分段 每段的间隔 200
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




public:
	Message(int _fd);
	Message(int _fd, eCmd _cmd);
	Message(const Message & _Msg);
	~Message();

	// 将mp_buf 反序列化
	void Decode(char * _buf, size_t _len);

	// 将mp_buf 序列化
	void Encode();


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

	int m_fd;
	size_t m_len;
	eCmd m_cmd; 			// 消息ID
	int m_pos;

private:


	char * mp_content; 			// 消息内容缓冲区指针 include starBuf and endBuf





};

union alen{
	char a[4] = {0};
	int i;
};

union astrLen{
	char a[2] = {0};
	short s;
};

union ai{
	char a[4] = {0};
	int i;
};

union af{
	char a[4] = {0};
	float f;
};

union al{
	char a[8] = {0};
	long long l;
};

union as{
	char a[2] = {0};
	short s;
};




#endif // MSG_H
