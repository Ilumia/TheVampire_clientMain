using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;

public partial class Communication {
	private Socket m_Client = null;
	private Socket socket = null;
	private static Communication comm;
	public static Communication Instance() {
		if (comm == null) {
			comm = new Communication();
		}
		return comm;
	}
	private char sendType = '-';
	private string sendMessage = null;

	private Communication()
	{
		Debug.Log ("start socket!");
		socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		IPEndPoint _ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 50005);
		
		SocketAsyncEventArgs _args = new SocketAsyncEventArgs();
		_args.RemoteEndPoint = _ipep;
		_args.Completed += new EventHandler<SocketAsyncEventArgs>(Connect_Completed);
		
		socket.ConnectAsync(_args);
	}

	public void SendMessageToServer(char type, String data) {
		Message message = new Message();
		//Debug.Log ("Send type: " + type + ", data: " + data);
		if (m_Client != null) {
			if (!m_Client.Connected) {
				DisconnectProcessing ();
			} else {
				byte[] _sData = Compression.CompressToBytes (data);

				message.InitSendPacket (Convert.ToByte (type), _sData);
				SocketAsyncEventArgs _sendArgs = new SocketAsyncEventArgs ();
				_sendArgs.SetBuffer (BitConverter.GetBytes (message.Length), 0, 4);
				_sendArgs.Completed += new EventHandler<SocketAsyncEventArgs> (Send_Completed);
				_sendArgs.UserToken = message;
				m_Client.SendAsync (_sendArgs);
			}
		} else {
			sendType = type;
			sendMessage = data;
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
			if(sendType != '-') {
				SendMessageToServer(sendType, sendMessage);
			}
		}
		else
		{
			DisconnectProcessing();
		}
	}
	private void Send_Completed(object sender, SocketAsyncEventArgs e)
	{
		Socket _client = (Socket)sender;
		Message message = (Message)e.UserToken;
		_client.Send(message.DataBuffer);
		Console.WriteLine("Send Type: {0}, Data: {1}", (char)message.Type, Encoding.Unicode.GetString(message.Data));
	}
	private void Receive_Completed(object sender, SocketAsyncEventArgs e)
	{
		Socket _client = (Socket)sender;
		Message message = (Message)e.UserToken;
		message.InitRecvPacket(e.Buffer);

		if (_client.Connected)
		{
			_client.Receive(message.DataBuffer, message.Length, SocketFlags.None);
			if(message.Data.Length > 0) {
				Debug.Log("Recv Type: " + (char)message.Type + ", Data: " + Encoding.Unicode.GetString(message.Data));
			}
			_client.ReceiveAsync(e);

			try {
				byte[] _data = Compression.DecompressToBytes(message.Data);
				ReceiveProcessing(message.Type, _data);
				//UISet.SetDebug("RecvType: " + (char)message.Type + "\nRecvData: " + Encoding.Unicode.GetString (message.Data));
			} catch(Exception _e) { 
				Debug.Log(_e.Message);
				Debug.Log(_e.StackTrace);
				//Console.WriteLine(_e.Message);
				//Console.WriteLine(_e.StackTrace);
			}
		}
		else
		{
			DisconnectProcessing();
		}
	}
	public void DisconnectProcessing() {
		m_Client = null;
		Debug.Log("Connection Failed!");
		socket.Close();
		socket = null;
		comm = null;
		GC.Collect();
		
		UISet.SetUILock(false);
		UISet.ActiveUI(UISet.UIState.MAIN_SELECT);
		UISet.ActiveUI(UISet.UIState.CAUTION);
		UISet.SetCaution("서버에 연결할 수 없습니다");
	}
	public void Disconnect() {
		this.socket.Close();
	}
	public Socket GetSocket() {
		return this.socket;
	}
}
