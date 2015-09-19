#ifndef _CONFIG_H_
#define _CONFIG_H_

#include "Common.h"

#include <map>
#include <fstream>
#include <iostream>
//using namespace std;



class Config
{
public:

	static Config& GetInstance()
	{
		static Config Instance;

		return Instance;
	}

	// 加载游戏全部全局配置
	static int LoadGlobalConfig();

public:
	// 全局配置
	static std::map<std::string, std::string> globalConfig_map;

private:
	Config(){}
	Config(const Config &){}
	Config& operator = (const Config &){}

	~Config(){}

};

#endif // _CONFIG_H_
