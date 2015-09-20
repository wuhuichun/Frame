#include "TcpServer.h"

using namespace std;

std::mutex MutexConnLst;

TcpServer::TcpServer()
{
	//ctor
	m_pBufRecv = new char[BUF_MAX_SIZE];  					// 接收缓冲区
	m_pBufSend = new char[BUF_MAX_SIZE]; 					// 发送缓冲区

	//OnRecvFunc = &MsgQunue::PushMsg;
}

TcpServer::~TcpServer()
{
	//dtor
	delete [] m_pBufRecv;
	delete [] m_pBufSend;
}

// 创建套接字地址结构 失败返回-1, 成功返回 m_listenfd
int TcpServer::Create()
{
	//cout<< "==> call  TcpServer::Create()"<< endl;

	m_listenfd = socket(AF_INET, SOCK_STREAM, 0);

	return m_listenfd;
}

// 帮顶套接字地址
int TcpServer::Bind()
{
	//cout<< "==> call  TcpServer::Bind()"<< endl;

	int opt = SO_REUSEADDR;
	setsockopt(m_listenfd, SOL_SOCKET, SO_REUSEADDR, &opt, sizeof(opt));

	// 复制套接字地址结构
	bzero(&m_Addr, sizeof(m_Addr));
	m_Addr.sin_family 		= AF_INET;
	m_Addr.sin_port 		= htons(m_port);
	m_Addr.sin_addr.s_addr 	= inet_addr(m_ip.c_str());

	// 绑定地址到监听套接字
	int ret = 0;
	ret = bind(m_listenfd, (struct sockaddr *)&m_Addr, sizeof(m_Addr));

	return ret;
}

// 将套接字置为监听状态
int TcpServer::Listen()
{
	//cout<< "==> call  TcpServer::Listen()"<< endl;

	int ret = 0;
	ret = listen(m_listenfd, MAX_CONNECT);

	return ret;
}

// 套接字初始化， 创建套接字， 并至于listen状态 成功返回0, 失败返回-1
int TcpServer::Init(std::string _ip, uint16_t _port)
{
	m_ip 	= _ip;
	m_port 	= _port;

	int ret = 0;

	ret = this->Create();
	if(-1 == ret)
	{
		cout<< "TcpServer::Init error when Create()"<< endl;
		return -1;
	}

	ret = this->Bind();
	if(-1 == ret)
	{
		cout<< "TcpServer::Init error when Bind()"<< endl;
		return -1;
	}

	ret = this->Listen();
	if(-1 == ret)
	{
		cout<< "TcpServer::Init error when Listen()"<< endl;
		return -1;
	}

	std::cout<< BOLDGREEN<< "TcpServer ready. IP:"<< RESET<< BOLDWHITE<< m_ip<< RESET
	<< BOLDGREEN<< " PORT:"<< RESET<< BOLDWHITE<< m_port<< RESET<< std::endl;

	return 0;
}

// 套接字等待链接循环
void TcpServer::ConnectLoop()
{
	fd_set Fset; 								// select recv监听的socket文件列表

	// 死循环检测socket。 select模型
	while(1)
	{
		FD_ZERO(&Fset);
		FD_SET(m_listenfd, &Fset);
		int maxFd = m_listenfd; 				// 最大fd
		struct timeval TimeOut = {0, 10}; 		// select 阻塞时间 1ms, 每次都需要赋值

		// 将所有在链接的套接字加入select范围
		for(auto& rIter : m_ConnectInfo_lst)
		{
			FD_SET(rIter.fd, &Fset);
			if(rIter.fd > maxFd)
				maxFd = rIter.fd;
		}

		// select 检测读取 和 链接
		int retSelect = select(maxFd + 1, &Fset, NULL, NULL, &TimeOut);
		if(0 == retSelect)
		{
			continue;
		}
		else if (-1 == retSelect)
		{
			cout<< "TcpServer::ConnectLoop() error when select()"<< endl;

			_exit(-1);
		}

		// 有新链接
		if(FD_ISSET(m_listenfd, &Fset))
		{
			this->OnNewConnect();
			continue;
		}

		// 有链接收到消息
		this->OnConnectRecvMsg(Fset);
	}
}


// 当select监听到有新链接的处理
void TcpServer::OnNewConnect()
{
	cout<< "==> call  TcpServer::OnNewConnect() FD_SETSIZE= "<< FD_SETSIZE<<
	". cur connect count:"<< m_ConnectInfo_lst.size()<< endl;

	unsigned int limit = min(FD_SETSIZE, MAX_CONNECT);
	if(m_ConnectInfo_lst.size() >= limit)
	{
		cout<< "sorry. at the max of sock connect's Count "<< FD_SETSIZE<< endl;
		return;
	}

	SockAddr4 ConnAddr; 								// 新的链接套接字地址
	socklen_t len = sizeof(ConnAddr); 					// 套接字地址长度
	bzero(&ConnAddr, sizeof(len));

	// 接受链接
	int connFd = 0; 									// 链接套接字
	connFd = accept(m_listenfd, (struct sockaddr *)&ConnAddr, &len);
	if(-1 == connFd)
	{
		cout<< "==> TcpServer::OnNewConnect() error when accept()"<< endl;
		sleep(1);
		return;
	}

	// 保存新链接信息
	ConInfo Conn; 										// 新的链接信息
	Conn.fd = connFd;
	memcpy(&Conn.Addr4, &ConnAddr, sizeof(ConnAddr)); 	// 复制地址结构

	MutexConnLst.lock();
	m_ConnectInfo_lst.push_back(Conn);
	MutexConnLst.unlock();
}

// 当select监听到有链接收到消息
void TcpServer::OnConnectRecvMsg(fd_set & _Fset)
{
	bool isCloseConn = false;
	std::list<ConInfo>::iterator dleteIter = m_ConnectInfo_lst.begin();
	std::list<ConInfo>::iterator iter = dleteIter;
	for(; iter != m_ConnectInfo_lst.end(); iter++)
	{
		int connFd = iter->fd;

		if(!FD_ISSET(connFd, &_Fset))
			continue;

		/// 找到有消息的链接了
		//char m_pBufRecv[BUF_MAX_SIZE] = {0};
		memset(&m_pBufRecv[0], 0, sizeof(m_pBufRecv)); 		// 接受缓冲区清空
		size_t recvLen = 0;
		recvLen = (size_t)recv(connFd, m_pBufRecv, BUF_MAX_SIZE, 0);
		if(recvLen >= BUF_MAX_SIZE)
		{
			std::cout<< "==>the msg len beyond limit: \t"<< BUF_MAX_SIZE<< endl;
			continue;
		}
		else if(recvLen == 0)
		{
			// 如果等于0 关闭链接 从链接列表里干掉
			this->ShutDownAConnect(connFd);
			isCloseConn = true;
			dleteIter = iter;
			continue;
		}

		std::cout<< "==> u got msg:\t"<< m_pBufRecv<< "\tlen:"<< recvLen<< endl;

		// 解包处理
		this->UnpackAndPushInQunue(recvLen);


		 // YU_TODO: 临时处理
		memset(&m_pBufSend[0], 0, sizeof(m_pBufSend)); 		// 接受缓冲区清空
		memcpy(&m_pBufSend[0], m_pBufRecv, recvLen);
		m_pBufSend[recvLen] = '\0';

		send(connFd, m_pBufSend, recvLen, 0);
	}

	if(isCloseConn)
	{
		m_ConnectInfo_lst.erase(dleteIter);
	}
}

void TcpServer::Send(int _fd, Message* _pMsg){
		 // YU_TODO: 临时处理

		int sendLen = _pMsg->m_len + 4;
		memset(&m_pBufSend[0], 0, sizeof(m_pBufSend)); 		// 接受缓冲区清空
		memcpy(&m_pBufSend[0], _pMsg->GetBuf(), sendLen);

		send(_fd, m_pBufSend, sendLen, 0);
}

// 关闭监听套接字
int TcpServer::Close()
{
	cout<< "==> call  TcpServer::Close()"<< endl;

	close(m_listenfd);
	return 0;
}

// 关闭某个链接套接字
int TcpServer::ShutDownAConnect(int _fd)
{
	cout<< "==> call  TcpServer::Close()"<< endl;

	// YU_TODO: 在这里处理断开链接， 链接列表排除在检测的时候就处理了

	// 收发都关闭
	shutdown(_fd, SHUT_RDWR);
	return 0;
}

void TcpServer::SetMsgQunue(MsgQunue* ptr){
    this->m_MsgQunue = ptr;
}

MsgQunue* TcpServer::GetMsgQunue(){
	return this->m_MsgQunue;
}

void TcpServer::UnpackAndPushInQunue(size_t _recvLen)
{
	/* 包描述 cmd + 内容 才是真真的消息
	-------------------------------------------------------------
	| 0x02 0x02 | len | cmd | ********内容********** | 0x03 0X03 |
	-------------------------------------------------------------
		  2B       4B    4B          len-8B              2B

	测试数据：02 02 31 30 30 32 30 32 48 65 6C 6C 6F 20 03 03 0D
	*/

	const size_t packetBeginLen = 2;
	const size_t packetEndLen = 2;
	const size_t lenLen = 4;

	const size_t contentLenLimit = 2000;

	const char packetBegin 	= '\2'; 		// 双左斜杠表示包开始
	const char packetEnd 		= '\3'; 		// 双右斜杠表示包结束
	size_t pos 	= 0;


	while(pos+4 < _recvLen) 				//	//判断 包是否已经读完
	{
		// 判断包头
		if((m_pBufRecv[pos] != packetBegin) || (m_pBufRecv[pos+1] != packetBegin)) 		// 开始符 2B
		{
			std::cout<< "a bad package. begin error."<< endl;
			return;
		}

		pos += packetBeginLen;

		char lenBuf[lenLen] = {0}; 									// 包len 2B
		memcpy(lenBuf, &m_pBufRecv[pos], lenLen);
		size_t len = (size_t)atoi(lenBuf);

		// 如果一个包长度大于contentLenLimit 就被认为是坏包被抛弃掉
		if(len > contentLenLimit)
		{
			std::cout<< "u got a bad package. len ="<< len<< "beyond 2000."<<endl;
			return;
		}

		char msgBuf[contentLenLimit] = {0};
		memcpy(msgBuf, &m_pBufRecv[pos], len);
		pos += len;

		// 判断包尾
		if((m_pBufRecv[pos] == packetEnd) && (m_pBufRecv[pos+1] == packetEnd))
		{
			pos += packetEndLen;
			std::cout<< "a good msg package. len ="<< len<< endl;
			// YU_TODO： 	CallBack
			m_MsgQunue->PushRecvMsg((msgBuf));
		}
		else
		{
			std::cout<< "a bad package. end error.. len ="<< len<< endl;
			return ;
		}

	}






}





