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

void MsgQunue::PushRecvMsg(const Message& _Msg)
{
	std::cout<<"Good u got:cmd: "<< (int)_Msg.m_cmd<< std::endl;
	this->MsgRecv_que.push(_Msg);
}

Message MsgQunue::PopRecvMsg(){
	Message MsgTemp = this->MsgRecv_que.front();
	this->MsgRecv_que.pop();

	return MsgTemp;
}

bool MsgQunue::IsRecvEmpty(){
	return MsgRecv_que.empty();
}


void MsgQunue::PushSendMsg(const Message& pMsg)
{
	this->MsgSend_que.push(pMsg);
}

Message MsgQunue::PopSendMsg(){
	Message MsgTemp = this->MsgSend_que.front();
	this->MsgSend_que.pop();

	return MsgTemp;
}

bool MsgQunue::IsSendEmpty(){
	return MsgSend_que.empty();
}
