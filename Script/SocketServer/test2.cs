using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class test2 : MonoBehaviour {

	public void Start() {
//		Connect("192.168.0.137","start");
	}

	private TcpClient client;

	public  void Connect(String server) {
		try {
			// サーバーに接続
			Debug.Log("start");
			Int32 port = 10000;
			client = new TcpClient(server, port);
			
			Debug.Log("connect");

			String message = "hello";
			
			Send("aaa");

			
			
//			Byte[] data = System.Text.Encoding.UTF8.GetBytes(message);
////			Byte[] data2 = System.Text.Encoding.UTF8.GetBytes("yeah");
//			NetworkStream stream = client.GetStream();
//			stream.Write(data, 0, data.Length);
////			stream.Write(data2, 1, data2.Length);
//			Debug.Log("送信: " + message);

//			client.Close();
		}
		catch (Exception e) {
			Console.WriteLine(e.Message);
		}
	}

	public void Send(String message)
	{
		// サーバーにメッセージ送信
		Byte[] data = System.Text.Encoding.UTF8.GetBytes(message);
//			Byte[] data2 = System.Text.Encoding.UTF8.GetBytes("yeah");
		NetworkStream stream = client.GetStream();
		stream.Write(data, 0, data.Length);
//			stream.Write(data2, 1, data2.Length);
		Debug.Log("送信: " + message);
	}
	
	
}
