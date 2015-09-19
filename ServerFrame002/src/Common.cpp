#include "Common.h"

Common::Common()
{
	//ctor
}

Common::~Common()
{
	//dtor
}

// split(ret, “a：b”, ":");
int Common::split(char _out[][256], char *_in, char *_spliter)
{
	char *strTemp = nullptr;
	int splitCount = 0; 			// 切割成的个数

	strTemp = strtok(_in, _spliter);

	while(strTemp != nullptr)
	{
		memcpy(_out[splitCount],  strTemp, sizeof(_out[splitCount]));

		splitCount++;

		strTemp = nullptr;
		strTemp = strtok(nullptr, _spliter);
	}

	return splitCount;
}
