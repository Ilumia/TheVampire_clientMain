using System.Text;

public class Compression{
	public static byte[] CompressToBytes(string data)
	{
		byte[] _data = Encoding.UTF8.GetBytes(data);
		return _data;
	}
	public static byte[] CompressToBytes(byte[] data)
	{
		string tData = Encoding.Unicode.GetString(data);
		byte[] _data = Encoding.UTF8.GetBytes(tData);
		return _data;
	}
	public static string DecompressToUnicode(byte[] data)
	{
		string _data = Encoding.UTF8.GetString(data);
		return _data;
	}
	public static byte[] DecompressToBytes(byte[] data)
	{
		string __data = Encoding.UTF8.GetString(data);
		byte[] _data = Encoding.Unicode.GetBytes(__data);
		return _data;
	}
}
