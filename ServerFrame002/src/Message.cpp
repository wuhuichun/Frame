#include "Message.h"
#include "Game.h"

using namespace std;

Message::Message()
{
	//ctor
	m_pos = 0;


}

Message::Message(eCmd _cmd)
{
	//ctor
	this->m_cmd = _cmd;
	m_pos = 0;
	mp_content = new char[MSG_LEN_MAX];
	memset(mp_content, 0, MSG_LEN_MAX);

	memset(&(mp_content[m_pos]), '\2', 2);
	m_pos += 2;

	const int lenLen = 4;
	m_pos += 4;

	const int cmdLen = 4;
	memcpy(&(mp_content[m_pos]), &m_cmd, cmdLen);
	m_pos += cmdLen;
}

Message::Message(const Message & _Msg)
{
	m_len = _Msg.m_len;
	m_cmd = _Msg.m_cmd;

	mp_content = new char[m_len];
	memcpy(mp_content, _Msg.mp_content, m_len);



	//ctor
}

Message::~Message()
{
	//dtor
	if(mp_content != nullptr)
	{
		delete [] mp_content;
	}

}

void Message::Encode(){
	m_len = m_pos - 2;
	const int lenLen = 4;
	memcpy(&(mp_content[2]), &m_len, lenLen);

	memset(&(mp_content[m_pos]), '\3', 2);
	m_pos += 2;


}

void Message::Decode(char* _pbuf){
	/* 包描述 cmd + 内容 才是真真的消息
	-------------------------------------------------------------
	| 0x02 0x02 | len | cmd | ********内容********** | 0x03 0X03 |
	-------------------------------------------------------------
		  2B       4B    4B          len-4B              2B

	测试数据：02 02 31 30 30 32 30 32 48 65 6C 6C 6F 20 03 03 0D
	*/
	const size_t lenLen = 4;
	const size_t cmdLen = 4;
	char lenBuf[lenLen] = {0};
	char cmdBuf[cmdLen] = {0};
	int pos = 0;
	m_pos = 0;

	// len
	memcpy(lenBuf, &(_pbuf[pos]), lenLen);
	pos += lenLen;
	m_len = atoi(lenBuf);

	// cmd
	memcpy(cmdBuf, &(_pbuf[pos]), cmdLen);
	pos += cmdLen;
	m_pos = pos;
	m_cmd = (eCmd)atoi(cmdBuf);

	// content
	mp_content = new char[m_len];
	memcpy(mp_content, _pbuf, m_len);

}


void Message::Send(int _fd){
	this->Encode();

	// YU_TODO: msg router. to server or to client
	if(((int)m_cmd > MSG_S2C_BEGIN)
		&& ((int)m_cmd < 60000))
	{
		cout<<"Sill SendMsg, cmd: " << (int)m_cmd<<endl;
		Game::GetInstance().SendMsg2Client(_fd, this);
	}


}

char * Message::GetBuf(){
	return mp_content;
}

//
char Message::GetChar()
{
	char ret;
	ret = mp_content[m_pos];
	m_pos += 1;

	return ret;
}

void  Message::AddChar(char _value)
{
	const int len = 1;
	memcpy(&(mp_content[m_pos]), &_value, len);
	m_pos += len;
}

//
std::string Message::GetString()
{
	char lenBUf[2] = {0};

	memcpy(lenBUf, &(mp_content[m_pos]), 2);
	m_pos += 2;

	int strLen = atoi(lenBUf);
	char strBuf[640] = {0};

	memcpy(strBuf, &(mp_content[m_pos]), strLen);
	m_pos += strLen;
    memset(&(strBuf[strLen]), '\0', 1);

	std::string strRet = strBuf;
	return strRet;
}

void  Message::AddString(std::string _value)
{
	int16_t strLen = _value.length();
	const int lenLen = 2;
	memcpy(&(mp_content[m_pos]), &strLen, lenLen);
	m_pos += lenLen;


	memcpy(&(mp_content[m_pos]), &_value, strLen);
	m_pos += strLen;
}

//
short Message::GetShort()
{
	const int len = 2;
	char retBUf[len] = {0};

	memcpy(retBUf, &(mp_content[m_pos]), len);
	m_pos += len;

	short ret = atoi(retBUf);
	return ret;

}

void  Message::AddShort(short _value)
{
	const int len = 2;
	memcpy(&(mp_content[m_pos]), &_value, len);
	m_pos += len;
}

//
int Message::GetInt()
{
	const int len = 4;
	char retBUf[len] = {0};

	memcpy(retBUf, &(mp_content[m_pos]), len);
	m_pos += len;

	int ret = atoi(retBUf);
	return ret;

}

void  Message::AddInt(int _value)
{
	const int len = 4;
	memcpy(&(mp_content[m_pos]), &_value, len);
	m_pos += len;
}

	//
long long Message::GetLong()
{
	const int len = 8;
	char retBUf[len] = {0};

	memcpy(retBUf, &(mp_content[m_pos]), len);
	m_pos += len;

	int ret = atol(retBUf);
	return ret;
}

void  Message::AddLong(long long _value)
{
	const int len = 8;
	memcpy(&(mp_content[m_pos]), &_value, len);
	m_pos += len;
}

	//
float Message::GetFloat()
{
	const int len = 4;
	char retBUf[len] = {0};

	memcpy(retBUf, &(mp_content[m_pos]), len);
	m_pos += len;

	float ret = atof(retBUf);
	return ret;
}

void  Message::AddFloat(float _value)
{
	const int len = 4;
	memcpy(&(mp_content[m_pos]), &_value, len);
	m_pos += len;
}

//
bool Message::GetBool()
{
	const int len = 1;
	char retBuf = '0';

	memcpy(&retBuf, &(mp_content[m_pos]), len);
	m_pos += len;

	bool ret = bool(retBuf);
	return ret;
}

void  Message::AddBool(bool _value)
{
	const int len = 1;
	memcpy(&(mp_content[m_pos]), &_value, len);
	m_pos += len;
}
