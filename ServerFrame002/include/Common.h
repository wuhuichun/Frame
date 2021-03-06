#ifndef COMMON_H
#define COMMON_H

#include <string.h>

#define RESET "\033[0m"
#define BLACK "\033[30m" /* Black */
#define RED "\033[31m" /* Red */
#define GREEN "\033[32m" /* Green */
#define YELLOW "\033[33m" /* Yellow */
#define BLUE "\033[34m" /* Blue */
#define MAGENTA "\033[35m" /* Magenta */
#define CYAN "\033[36m" /* Cyan */
#define WHITE "\033[37m" /* White */
#define BOLDBLACK "\033[1m\033[30m" /* Bold Black */
#define BOLDRED "\033[1m\033[31m" /* Bold Red */
#define BOLDGREEN "\033[1m\033[32m" /* Bold Green */
#define BOLDYELLOW "\033[1m\033[33m" /* Bold Yellow */
#define BOLDBLUE "\033[1m\033[34m" /* Bold Blue */
#define BOLDMAGENTA "\033[1m\033[35m" /* Bold Magenta */
#define BOLDCYAN "\033[1m\033[36m" /* Bold Cyan */
#define BOLDWHITE "\033[1m\033[37m" /* Bold White */


class Common
{
public:
	Common();
	~Common();

	// 切割字符串函数
	static int split(char _out[][256], char *_in, char *_spliter);

protected:
private:
};

//
#define SAFE_DELETE(p) { if((p) != nullptr){ delete (p); (p) = nullptr;} }

#define SAFE_DELETE_ARY(p) { if((p) != nullptr){ delete [] (p); (p) = nullptr;} }

// 检查某一行文本数据是否是空行 ch:每条数据第一个字符
#define IS_EMPTY_LINE(ch) ((ch == '\n') || (ch == '\r\n') || (ch == 0))

// 检查某一行文本数据是否是注释 ch1,ch2:每条数据前两个字符
#define IS_COMMENT(ch1,ch2) ((ch1 == '/') && (ch2 == '/'))

// 检查某一行文本数据是否是不需要加载的 ch1,ch2:每条数据前两个字符
#define IS_NOT_NEED_LOAD(ch1,ch2) (IS_EMPTY_LINE(ch1) || IS_COMMENT(ch1,ch2))

// 检查某一行文本数据是否是需要加载的 ch1,ch2:每条数据前两个字符
#define IS_NEED_LOAD(ch1,ch2) (!IS_NOT_NEED_LOAD(ch1,ch2))

#endif // COMMON_H
