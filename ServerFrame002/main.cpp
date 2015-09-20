#include "TcpServer.h"
#include "TcpClient.h"
// #include "Config.h"
#include "Common.h"
#include "Game.h"

#include <stdio.h>
#include <iostream>
#include <thread>
#include <fstream>


using namespace std;

int main()
{
    std::cout << BOLDGREEN << "hello world" << RESET << std::endl;

	Game Game1 = Game::GetInstance();

//	std::cout<< pGame<<endl;

	Game1.Init();

	cout<< "Start GameLoop()"<< endl;
	Game1.GameLoop();

    return 0;
}
