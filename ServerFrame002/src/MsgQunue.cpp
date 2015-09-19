#include "MsgQunue.h"

MsgQunue::MsgQunue()
{
	//ctor
}

MsgQunue::~MsgQunue()
{
	//dtor
}

void MsgQunue::PushMsg(char * _pbuf)
{
	std::cout<<"Good u got:MsgBuf"<< *_pbuf<< std::endl;
	Msg MsgTemp;
	MsgTemp.Decode(_pbuf);
	this->Msg_que.push(MsgTemp);
}

Msg MsgQunue::PopMsg(){

//	if(this->Msg_que.count == 0){
//		return nullptr;
//	}

//	this->Msg_que.count
	Msg MsgTemp = this->Msg_que.front();
	this->Msg_que.pop();

	return MsgTemp;
}

bool MsgQunue::IsEmpty(){
//
//	if(Msg_que.Msg_que.size == 0 == 0){
//		return true;
//	}

	return Msg_que.empty();
}
