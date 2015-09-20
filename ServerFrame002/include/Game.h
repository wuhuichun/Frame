#ifndef GAME_H

#include "TcpServer.h"
#include "TcpClient.h"
#include "Config.h"
#include "Common.h"

#include <iostream>
#include <thread>
#include <fstream>

#define GAME_H


class Game
{
private:
		Game();
    TcpServer ServerSock;
    static Game* mp_Instance;

public:

	~Game();

public:
	static Game& GetInstance()
	{
		/*
		if(mp_Instance == nullptr)
		{
			mp_Instance = new Game();
		}
*/
		static Game Instance;
		return Instance;
	}

	void Init();

	void GameLoop();

	void SendMsg2Client(Message* _pMsg);

protected:


};

#endif // GAME_H
