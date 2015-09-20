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

	// 入队
	void PushRecvMsg(char * _pbuf);

	// 出队
	Message PopRecvMsg();


	bool IsRecvEmpty();

		// 入队
	void PushSendMsg(Message * _pbuf);

	// 出队
	Message PopSendMsg();


	bool IsSendEmpty();

private:

	std::queue<Message> MsgRecv_que;
	std::queue<Message> MsgSend_que;

};

#endif // _MSGQUNUE_H_
