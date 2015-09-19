/*--------------------------------------------------------------------------------
file: TcpClient.h
use	: 封装一个TcpClient类。
ver :
	1.0 by whc. at 2014-10-24. anything call me.
memo:
	只适用IPV4,
	只是适用服务器,
--------------------------------------------------------------------------------*/

/*
#ifndef _TCPCLIENT_H_
#define _TCPCLIENT_H_

#include <unistd.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <arpa/inet.h>
#include <sys/time.h>
#include <netdb.h>
#include <memory.h>
#include <thread>
#include <mutex>
#include <list>
#include <iostream> 							// YU_TODO: 以后换成日志类



#ifndef BUF_MAX_SIZE
#define BUF_MAX_SIZE 5320  						// 收发缓冲区大小， *超出不处理 并提示错误
#endif // BUF_MAX_SIZE

typedef struct sockaddr_in SockAddr4; 			// IP4 套接字地址结构



class TcpClient
{
public:
    TcpClient();
    ~TcpClient();


	// 初始化 _ip, 服务器IP， _port服务器程序端口号
	int Init(std::string _ip, uint16_t _port);

	// 链接服务器
	int Connect();

	// 收发循环
	int RecvSendLoop();

	// 断开链接
	int Close();
private:

	int 		m_serverFd; 				// 服务器套接字ID
	uint16_t 	m_serverPort; 				// 服务器端口号
	std::string m_serverIp; 				// 服务器IP地址
	SockAddr4 	m_ServerAddr; 				// 需要链接的目标套接字地址结构

private:

	// 创建套接字地址结构
	int Create();

	// 帮顶套接字地址
	int Bind();

};

#endif // _TCPCLIENT_H_
*/