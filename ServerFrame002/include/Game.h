#ifndef GAME_H

#include "TcpServer.h"
#include "TcpClient.h"
#include "Config.h"
#include "Common.h"
#include "Player.h"

#include <iostream>
#include <thread>
#include <fstream>

#define GAME_H


class Game
{
private:
	Game();

    TcpServer m_ServerSock;

    std::list<Player> m_Player_lst; 				// Player list

    bool m_isEixt;

public:

	~Game();

public:
	static Game& GetInstance()
	{
		static Game Instance;
		return Instance;
	}

	void Init();

	void Dispose();

	void Exit();

	void GameLoop();

	void SendMsg2Client(Message* _pMsg);

protected:


};

#endif // GAME_H
