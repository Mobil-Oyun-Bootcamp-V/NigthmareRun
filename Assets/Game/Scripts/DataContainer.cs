using System.Collections.Generic;

public class DataContainer
{
	private Dictionary<string, object> _datas;

	public DataContainer()
	{
		_datas = new Dictionary<string, object>();
	}

	public void AddData(string name, object data)
	{
		_datas[name] = data;
	}

	public object GetData(string name)
	{
		return _datas[name];
	}
}