/*

#include "TcpClient.h"

using namespace std;


TcpClient::TcpClient()
{
	//ctor
}

TcpClient::~TcpClient()
{
	//dtor
}

// 创建套接字地址结构
int TcpClient::Create()
{
	cout << "==> call TcpClient::Create()." << endl;

	m_serverFd = socket(AF_INET, SOCK_STREAM, 0);
	if(-1 == m_serverFd)
	{
        cout<< "==> TcpClient::Create() error when socket()"<<endl;
	}

	return m_serverFd;
}

// 绑定套接字地址
int TcpClient::Bind()
{
	cout << "==> call TcpClient::Bind()." << endl;

	bzero(&m_ServerAddr, sizeof(m_ServerAddr));
	m_ServerAddr.sin_family 		= AF_INET;
	m_ServerAddr.sin_port 			= htons(m_serverPort);
	m_ServerAddr.sin_addr.s_addr 	= inet_addr(m_serverIp.c_str()); 		//  htonl();

	return 0;
}

// 初始化 _ip, 服务器IP， _port服务器程序端口号
int TcpClient::Init(string _ip, uint16_t _port)
{
	cout << "==> call TcpClient::Init(). _ip:" << _ip<< ", port:"
		<< _port<<endl;

	m_serverIp		= _ip;
	m_serverPort 	= _port;

    this->Create();
    this->Bind();

	return 0;
}

// 链接服务器
int TcpClient::Connect()
{
	int ret = connect(m_serverFd, (struct sockaddr *)&m_ServerAddr,
					sizeof(m_ServerAddr));
	if(-1 == ret)
	{
		 cout<< "==> TcpClient::Connect() error when connect()"<<endl;
		 return ret;
	}

	return 0;
}

// 收发循环
int TcpClient::RecvSendLoop()
{
	cout<< "u connect sccessed. begin TcpClient::RecvSendLoop()"<< endl;
	while(1)
	{
		sleep(1);
		cout<< "msg."<< endl;

		char buf[BUF_MAX_SIZE];
		char *str1 = "Hello Server, this is Client.\n";
		memcpy(buf, str1, strlen(str1));

		send(m_serverFd, buf, strlen(buf), 0);
	}

	return 0;
}

// 断开链接
int TcpClient::Close()
{

	return 0;
}
*/
