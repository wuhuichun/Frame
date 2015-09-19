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

#define MSG_BLOCK 0x80 				// 将消息分段 每段的间隔 128


	// 消息头。
	enum class eCmd{

		C2L_TEST = 0x00*MSG_BLOCK, 		// 测试数据
		C2L_TEST_1,



	};

class Msg
{


public:
	Msg();
	Msg(eCmd _cmd);
	Msg(const Msg & _Msg);
	~Msg();

	// 将消息发送出去, 需要和 AppendBuf() 配合使用
	void Send(int fd);

	// 将mp_buf 反序列化
	void Decode(char * _buf);

	// 将mp_buf 序列化
	void Encode();

	///
	//
	char GetChar();
	void AddChar(char _value);

	//
	std::string GetString();
	void AddString();

	//
	int GetInt();
	void AddInt(int _value);

	//
	long long GetLong();
	void AddLong(long long _value);

	//
	float GetFloat();
	void AddFloat();

	//
	double GetDouble();
	void AddDouble();

	//
	bool GetBool();
	void AddBool();




public:

	size_t m_len;
	private:

	eCmd m_cmd; 			// 消息ID
	char * mp_content; 			// 消息内容缓冲区指针

	int m_decodePos;


};

#endif // MSG_H
