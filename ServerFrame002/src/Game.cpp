#include "Game.h"

using namespace std;

Game::Game()
{
	//ctor
	m_isEixt = false;
	m_Player_lst.clear();
}

Game::~Game()
{
	//dtor
	m_Player_lst.clear();
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
    m_ServerSock.Init(loginIp, loginPort);

	std::thread thread1(hello, &m_ServerSock);
	thread1.detach();




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


void Game::Dispose()
{

}


void Game::GameLoop(){
	cout<< "Start GameLoop()"<< endl;
	while(!m_isEixt){

		if(!m_ServerSock.GetMsgQunue().IsSendEmpty()){
			Message Msg = m_ServerSock.GetMsgQunue().m_MsgSend_que.front();

			m_ServerSock.Send(&Msg);

			m_ServerSock.GetMsgQunue().m_MsgSend_que.pop();
		}

		if (!m_ServerSock.GetMsgQunue().IsRecvEmpty()){
			Message Msg = m_ServerSock.GetMsgQunue().m_MsgRecv_que.front();
			//cout<< "GameLoop() 2.2"<< endl;
			switch(Msg.m_cmd){
				case eCmd::C2S_Test_Hello:
				{

					string getf = Msg.GetString();
					//short getI = Msg.GetShort();

					cout<< "GameLoop() 2.3, getf:"<< getf<< endl;  // " getI:"<< getI<<
					Message MsgSend(Msg.m_fd, eCmd::S2C_Test_Hello);
					//cout<< "GameLoop() 2.4"<< endl;
					MsgSend.AddString(u8"shijie网络启动");
					MsgSend.AddShort(9);
					//cout<< "GameLoop() 2.5"<< endl;
					SendMsg2Client(&MsgSend);
				}
				break;

				default:;

			}

			m_ServerSock.GetMsgQunue().m_MsgRecv_que.pop();
		}
	}

}

// exit
void Game::Exit(){
	m_isEixt = true;
}

void Game::SendMsg2Client(Message* _pMsg){

	_pMsg->Encode();

	// YU_TODO: msg router. to server or to client
	int cmd = (int)_pMsg->m_cmd;
	if(( MSG_S2C_BEGIN< cmd ) && (cmd < 60000))
	{
		//cout<<"Sill SendMsg, cmd: " << cmd<<endl;

			m_ServerSock.GetMsgQunue().PushSendMsg(_pMsg);
	}
	else{
		cout<< "Error at Msg.Send(), sorry, I don't know which shall I send to."<< endl;
	}


}
