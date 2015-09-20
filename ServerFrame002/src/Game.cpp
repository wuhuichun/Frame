#include "Game.h"

using namespace std;

Game::Game()
{
	//ctor
}

Game::~Game()
{
	//dtor
}

// YU_TODO:
void hello(TcpServer* _pSock)
{
	_pSock->ConnectLoop();
}

/*
void world(TcpClient* _pSock)
{
	_pSock->RecvSendLoop();
}
*/

void Game::Init()
{

	/// 配置读取
	Config::LoadGlobalConfig();

	/// 网络启动

	// 服务器部分Test
    string loginIp = Config::globalConfig_map["TestServerIp"];
    uint16_t loginPort = std::atoi( Config::globalConfig_map["TestServerPort"].c_str() );


    ServerSock.Init(loginIp, loginPort);

	MsgQunue MsgQue;
	ServerSock.SetMsgQunue(&MsgQue);

	std::thread thread1(hello, &ServerSock);
	thread1.join();




	// 客户端部分Test
//    string ip = Config::globalConfig_map["TestServerIp"];
//    uint16_t port = (uint16_t)std::atoi( (Config::globalConfig_map["TestServerPort"]).c_str() );
//
//	cout<< Config::globalConfig_map["TestServerPort"]<< endl;
//	cout<< "[State]Ready for connect Server. ip:"<< ip<< " port:"<< port<< endl;
//
//	TcpClient myClient;
//	myClient.Init(ip, port);
//	if(0 == myClient.Connect())
//	{
//		 std::thread thread2(world, &myClient);
//
//		 thread2.join();
//	}
//	else
//	{
//		cout<< "socket connect Error. maybe server not run."<< endl;
//		return 0;
//	}
}

void Game::GameLoop(){
	while(true){
			cout<<"GameLoop()"<<endl;

		if(!ServerSock.GetMsgQunue()->IsSendEmpty()){
			Message msgTemp = ServerSock.GetMsgQunue()->PopRecvMsg();
			std::cout<<"Send Msg, cmd:"<< (int)msgTemp.m_cmd<< endl;
			ServerSock.Send(&msgTemp);  //ServerSock.GetMsgQunue()->sendFd,
		}


		if (!ServerSock.GetMsgQunue()->IsRecvEmpty()){

			Message msgTemp = ServerSock.GetMsgQunue()->PopRecvMsg();

			switch(msgTemp.m_cmd){
				case eCmd::C2S_Test_Hello:
				{
					Message Msg(MsgQunue::sendFd, eCmd::S2C_Test_Hello);
					Msg.AddInt(6);
					this->SendMsg2Client(&Msg);
				}
				break;

				default:;

			}
		}


	}

}

void Game::SendMsg2Client(Message* _pMsg){

	_pMsg->Encode();

	// YU_TODO: msg router. to server or to client
	int cmd = (int)_pMsg->m_cmd;
	if(( MSG_S2C_BEGIN< cmd ) && (cmd < 60000))
	{
		cout<<"Sill SendMsg, cmd: " << cmd<<endl;

			ServerSock.GetMsgQunue()->PushSendMsg(*_pMsg);
	}
	else{
		cout<< "Error at Msg.Send(), sorry, I don't know which shall I send to."<< endl;
	}


}
