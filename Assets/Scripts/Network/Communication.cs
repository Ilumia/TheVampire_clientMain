using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public partial class Communication {
	private Socket m_Client = null;
	private static Communication comm = new Communication();
	private static Socket socket = null;
	public static Communication GetCommunication() {
		if (comm == null) {
			comm = new Communication();
		}
		return comm;
	}
	public static void DisconnectSocket() {
		/*
		 * 서버단에서 유저의 Disconnection을 판단해야 함
		 * 서버에서 모든 유저에게 n초 마다 ping 전송, m회 이상 응답이 없으면 close 
		*/
		try
		{
			socket.Close();
		}
		catch(Exception e)
		{
			Debug.Log(e.Message);
		}
	}

	public Communication()
	{
		socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		IPEndPoint _ipep = new IPEndPoint(IPAddress.Parse("192.168.0.5"), 8000);
		
		SocketAsyncEventArgs _args = new SocketAsyncEventArgs();
		_args.RemoteEndPoint = _ipep;
		_args.Completed += new EventHandler<SocketAsyncEventArgs>(Connect_Completed);
		
		socket.ConnectAsync(_args);

	}

	public void SendMessageToServer(char type, String data) {
		Message message = new Message();
		if (m_Client != null)
		{
			if (!m_Client.Connected)
			{
				m_Client = null;
				Console.WriteLine("Connection Failed!");
				Console.WriteLine("Press Any Key...");
			}
			else
			{
				byte[] _sData = Compression.CompressToBytes(data);

				message.InitSendPacket(Convert.ToByte(type), _sData);
				SocketAsyncEventArgs _sendArgs = new SocketAsyncEventArgs();
				_sendArgs.SetBuffer(BitConverter.GetBytes(message.Length), 0, 4);
				_sendArgs.Completed += new EventHandler<SocketAsyncEventArgs>(Send_Completed);
				_sendArgs.UserToken = message;
				m_Client.SendAsync(_sendArgs);
			}
		}
	}
	
	private void Connect_Completed(object sender, SocketAsyncEventArgs e)
	{
		m_Client = (Socket)sender;
		
		if (m_Client.Connected)
		{
			Message message = new Message();
			SocketAsyncEventArgs _receiveArgs = new SocketAsyncEventArgs();
			_receiveArgs.UserToken = message;
			_receiveArgs.SetBuffer(message.GetBuffer(), 0, 4);
			_receiveArgs.Completed += new EventHandler<SocketAsyncEventArgs>(Receive_Completed);
			m_Client.ReceiveAsync(_receiveArgs);
			Debug.Log("Server Connection Success");
		}
		else
		{
			m_Client = null;
			Debug.Log("Connection Failed!");
			Debug.Log("Press Any Key...");
		}
	}
	private void Send_Completed(object sender, SocketAsyncEventArgs e)
	{
		Socket _client = (Socket)sender;
		Message message = (Message)e.UserToken;
		_client.Send(message.DataBuffer);
		Console.WriteLine("Send Type: {0}", (char)message.Type);
		Console.WriteLine("Send Data: {0}", Encoding.Unicode.GetString(message.Data));
	}
	private void Receive_Completed(object sender, SocketAsyncEventArgs e)
	{
		Socket _client = (Socket)sender;
		Message message = (Message)e.UserToken;
		message.InitRecvPacket(e.Buffer);
		if (message.Length > 0)
		{
			byte[] data = Compression.DecompressToBytes(message.Data);
		}
		if (_client.Connected)
		{
			_client.Receive(message.DataBuffer, message.Length, SocketFlags.None);
			Debug.Log("Recv Type: " + (char)message.Type);
			Debug.Log("Recv Data: " + Encoding.Unicode.GetString(message.Data));
			_client.ReceiveAsync(e);

			try {
				ReceiveProcessing(message.Type, message.Data);
			} catch(Exception _e) { 
				Console.WriteLine(_e.Message);
				Console.WriteLine(_e.StackTrace);
			}
		}
		else
		{
			Debug.Log("Connection Failed!");
			m_Client = null;
		}
	}
}
