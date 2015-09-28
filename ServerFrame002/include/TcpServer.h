/*--------------------------------------------------------------------------------
file: TcpServer.h
use	: 封装一个TcpServer类。
ver :
	1.0 by whc. at 2014-10-21. anything call me.
memo:
	只适用IPV4,
	只是适用服务器,
--------------------------------------------------------------------------------*/

#ifndef _TCPSERVER_H_
#define _TCPSERVER_H_

#include <unistd.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <arpa/inet.h>
#include <sys/time.h>
#include <memory.h>
#include <thread>
#include <mutex>
#include <list>
#include <iostream> 							// YU_TODO: 以后换成日志类

#include "Common.h"
#include "MsgQunue.h"
#include "Client.h"

#define MAX_CONNECT 1000 						// 客户端最大链接数
#ifndef BUF_MAX_SIZE
#define BUF_MAX_SIZE 4096 *8 					// 收发缓冲区大小， *超出不处理 并提示错误
#endif // BUF_MAX_SIZE

typedef struct sockaddr_in SockAddr4; 			// IP4 套接字地址结构

/*
// 链接信息结构体
typedef struct _ConInfo{
	int fd;										// 套接字fd
	SockAddr4 Addr4; 							// 套接字地址结构
}ConInfo;
*/


class TcpServer
{
// 外部
public:
	TcpServer();
	~TcpServer();

	// 套接字初始化， 创建套接字， 并至于listen状态
	int Init(std::string _ip, uint16_t _port);

	// 套接字等待链接循环
	void ConnectLoop();

	// 关闭监听套接字
	int Close();

	// 关闭某个链接套接字
	int ShutDownAConnect(int _fd);

	void (MsgQunue:: *m_CallBack2)(char * _buf);

	// SockCallBack m_CallBack;
	void SetMsgQunue(MsgQunue* ptr);

	MsgQunue& GetMsgQunue();

	//
	void Send(Message* _Msg);

// 私有成员
private:
	uint16_t 	m_port; 						// 端口号
	int 		m_listenfd; 					// 监听套接字ID
	std::string m_ip;				 			// 服务器IP地址
	SockAddr4 	m_Addr;							// 服务器套接字地址结构
	char * 		m_pBufRecv;  					// 接收缓冲区
	char * 		m_pBufSend; 					// 发送缓冲区

	//SockCallBack OnRecvFunc; 					// 接受到消息的回调函数
	std::list<Client> m_Client_lst; 			// 所有客户端链接信息

    MsgQunue m_MsgQunue;

	// 创建套接字地址结构
	int Create();

	// 帮顶套接字地址
	int Bind();

	// 将套接字置为监听状态
	int Listen();

	// 当select监听到有新链接的处理
	void OnNewConnect();

	// 当select监听到有链接收到消息
	void OnConnectRecvMsg(fd_set & _Fset);

	// 将缓冲区数据解包并加入消息队列
	void UnpackAndPushInQunue(int fd, size_t _recvLen);


};

	// 一个函数指针
	//typedef void (TcpServer::* SockCallBack)(char* _buf);


#endif // TcpServer_H
