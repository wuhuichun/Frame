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
		if (ServerSock.GetMsgQunue()->IsEmpty()){
			continue;
		}

        Msg msgTemp = ServerSock.GetMsgQunue()->PopMsg();

        int recvInt = msgTemp.GetInt();
		cout<< "U recv: "<< recvInt<< endl;
	}

}
