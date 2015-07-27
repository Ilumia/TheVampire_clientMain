﻿using System;

public struct Message
{
	private byte[] data;
	private int length;
	
	public byte Type { get { return data[0]; } }
	public byte[] Data
	{
		get
		{
			byte[] temp = new byte[Length - 1];
			for (int i = 0; i < Length - 1; i++) { temp[i] = data[i + 1]; }
			return temp;
		}
	}
	public int Length { get { return length; } }
	public void InitSendPacket(byte type, byte[] _data)
	{
		length = _data.Length + sizeof(byte);
		data = new byte[Length];
		data[0] = type;
		for (int i = 0; i < _data.Length; i++) { data[i + 1] = _data[i]; }
	}
	public void InitRecvPacket(byte[] _data)
	{
		if (_data.Length < 4)
			return;
		length = BitConverter.ToInt32(_data, 0);
		data = new byte[Length + 1];
	}
	public byte[] DataBuffer { get { return data; } }
	public byte[] GetBuffer()
	{
		return new byte[4];
	}
}
