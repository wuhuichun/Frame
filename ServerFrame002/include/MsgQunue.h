#ifndef _MSGQUNUE_H_
#define _MSGQUNUE_H_

#include "Msg.h"
#include <queue>
#include <iostream>

//using namespace std;


class MsgQunue
{
public:
	MsgQunue();
	~MsgQunue();

	// 入队
	void PushMsg(char * _pbuf);

	// 出队
	Msg PopMsg();

	bool IsEmpty();

private:

	std::queue<Msg> Msg_que;

};

#endif // _MSGQUNUE_H_
