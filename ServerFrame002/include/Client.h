#ifndef CLIENT_H
#define CLIENT_H


class Client
{
	public:
		Client(int _fd);
		~Client();

		int m_fd;

	protected:
	private:
};

#endif // CLIENT_H
