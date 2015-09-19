#include "Config.h"

using namespace std;
// 全局配置
std::map<string, string> Config::globalConfig_map;


// 加载游戏全部全局配置
int Config::LoadGlobalConfig()
{
	char *fileName = "Config/GlobalConfig.txt"; 				// 全局文件路径名称
	int lineCount = 0; 									// 读取全局配置的行数
	ifstream GlobalFile(fileName); 						// 从全局文件读取的数据流

	// 检查全局配置文件是否存在
	if(nullptr == GlobalFile)
	{
		cout<< "[Error]Not found the file of global config."<< endl;
		return lineCount;
	}

	// 读取配置
	while(!GlobalFile.eof())
	{
		char lineBuf[512] = {0}; 							// 每一条全局配置缓冲

		GlobalFile.getline(lineBuf, sizeof(lineBuf));		// 读取一行

		/// 检查是否是有效数据
		char ch1 = lineBuf[0];
		char ch2 = lineBuf[1];

		// 是否是不需要存储的
		if(IS_NOT_NEED_LOAD(ch1, ch2))
			continue;

		// 格式检查 全局配置格式应为 "key:value"
		char strArr[2][256] = {0};
		char spliter = ':';
		int ret = 0;

		ret = Common::split(strArr, lineBuf, &spliter);
		if(ret != 2)
		{
			cout<< "[Error]The No."<< lineCount + 1<< " global config format error."<< endl;
			continue;
		}

		/// 存储配置
		std::map<string, string>::iterator iter;
		iter = globalConfig_map.find((strArr[0]));

		// 是否已经存在
		if(iter != globalConfig_map.end())
		{
			cout<< "[Warn]The No."<< lineCount + 1<< " global config repeated."<< endl;
			continue;
		}

        // 存入内存
        globalConfig_map[strArr[0]] = strArr[1];
		lineCount++;
	}

	cout<<"[State]Global config load over. Got "<< lineCount<< " configs in total."<< endl;
    return 0;
}
