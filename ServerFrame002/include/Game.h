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
	public:
		Game();
		~Game();

		void Init();

		void GameLoop();

	protected:
	private:
    TcpServer ServerSock;

};

#endif // GAME_H
