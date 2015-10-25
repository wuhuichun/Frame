// Define From City.xlsx
public struct CityCfg
{
	public int ID; // 编号
	public string Name; // 名称
	public int CountryID; // 所属势力
	public int StateID; // 所属州
	public int Level; // 等级
	public int Hp; // 耐久上限
	public int Defense; // 防御
	public int Soldiers; // 兵力上限
	public int Populations; // 人口
	public int Specialty1; // 特色1
	public int Specialty2; // 特色2
	public int Specialty3; // 特色3
	public int Building; // 建筑上限
	public int X; // 坐标X
	public int Y; // 坐标Y
	public int Z; // 坐标Z
	public string Desc; // 说明
}


// Define From Contry.xlsx
public struct ContryCfg
{
	public int ID; // 编号
	public string Name; // 名称
	public int Color; // 代表色
	public string Font; // 代表字
	public int Leader; // 领袖ID
	public int Flag; // 旗帜
	public string Desc; // 描述
}


// Define From State.xlsx
public struct StateCfg
{
	public int ID; // 编号
	public string Name; // 名称
	public string Capital; // 首府
	public string Desc; // 描述
}


