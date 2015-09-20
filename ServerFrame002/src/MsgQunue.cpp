#include "MsgQunue.h"

int MsgQunue::sendFd;

MsgQunue::MsgQunue()
{
	//ctor
}

MsgQunue::~MsgQunue()
{
	//dtor
}

void MsgQunue::PushRecvMsg(char * _pbuf)
{
	Message MsgTemp;
	MsgTemp.Decode(_pbuf);

	std::cout<<"Good u got:cmd: "<< (int)MsgTemp.m_cmd<< std::endl;
	this->MsgRecv_que.push(MsgTemp);
}

Message MsgQunue::PopRecvMsg(){
	Message MsgTemp = this->MsgRecv_que.front();
	this->MsgRecv_que.pop();

	return MsgTemp;
}

bool MsgQunue::IsRecvEmpty(){
	return MsgRecv_que.empty();
}


void MsgQunue::PushSendMsg(int _fd, Message* pMsg)
{
	sendFd = _fd;
	this->MsgSend_que.push(*pMsg);

}

Message MsgQunue::PopSendMsg(){
	Message MsgTemp = this->MsgSend_que.front();
	this->MsgSend_que.pop();

	return MsgTemp;
}

bool MsgQunue::IsSendEmpty(){
	return MsgSend_que.empty();
}
