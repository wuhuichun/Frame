#include "MsgQunue.h"

using namespace std;

int MsgQunue::sendFd;

MsgQunue::MsgQunue()
{

}

MsgQunue::~MsgQunue()
{

}

void MsgQunue::PushRecvMsg(const Message& _Msg)
{
	std::cout<<"Recv a msg, fd:"<< _Msg.m_fd<< " \tcmd:"<<
		(int)_Msg.m_cmd<< " \tlen:"<< _Msg.m_len<< std::endl;
	this->m_MsgRecv_que.push(_Msg);
}



void MsgQunue::PushSendMsg(Message* pMsg)
{
	this->m_MsgSend_que.push(*pMsg);
}



bool MsgQunue::IsRecvEmpty(){
	return m_MsgRecv_que.empty();
}

bool MsgQunue::IsSendEmpty(){
	return m_MsgSend_que.empty();
}
