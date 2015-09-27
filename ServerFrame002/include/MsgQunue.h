#ifndef _MSGQUNUE_H_
#define _MSGQUNUE_H_

#include "Message.h"
#include <queue>
#include <iostream>

//using namespace std;


class MsgQunue
{
public:
	MsgQunue();
	~MsgQunue();
	static int sendFd;

	// 入队
	void PushRecvMsg(const Message& _pbuf);
	void PushSendMsg(Message* pMsg);

	bool IsRecvEmpty();
	bool IsSendEmpty();

	std::queue<Message> m_MsgRecv_que;
	std::queue<Message> m_MsgSend_que;
private:



};

#endif // _MSGQUNUE_H_
