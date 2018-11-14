using UnityEngine;
using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.IO;
using System.Text;
using System.Net;
using Script;
using UnityEngine.UI;

public class Client : MonoBehaviour {

	bool isForceStop = false;
NetworkStream stream = null;
	bool isStopReading = false;
	byte[] readbuf;
//	[SerializeField] private MyViewer _myViewer;
	[SerializeField] private Text _text;
	private IEnumerator Start(){
		Debug.Log("START START");
		readbuf = new byte[1024];

		while (!isForceStop) {
			if(!isStopReading)    {
				Debug.Log("Coroutine");
				StartCoroutine(ReadMessage ());
			}
			yield return new WaitForSeconds(1f);//あんまりしょっちゅうやらないために
		}
	}

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.A) == true) {
			isForceStop = true;
		}

	}

	public IEnumerator SendCurrentMessage(string message){

		Debug.Log ("START SendMessage:" + message);

		string playerName = "[A]: ";
//サーバーにデータを送信する
		Encoding enc = Encoding.UTF8;
		byte[] sendBytes = enc.GetBytes(playerName + message + "\n");
//データを送信する
		stream.Write(sendBytes, 0, sendBytes.Length);
		yield break;
	}

//常駐
	private IEnumerator ReadMessage(){
		NetworkStream stream = GetNetworkStream ();
// 非同期で待ち受けする
		stream.BeginRead (readbuf, 0, readbuf.Length, new AsyncCallback (ReadCallback), null);
		isStopReading = true;
		yield return null;
	}

	public void ReadCallback(IAsyncResult ar ){
		Encoding enc = Encoding.UTF8;
		NetworkStream stream =  GetNetworkStream ();
		int bytes = stream.EndRead(ar);
		string message = enc.GetString (readbuf, 0, bytes);
		message = message.Replace("\r", "").Replace("\n", "");
		isStopReading = false;
		Debug.Log(message);
//		_text.text = message;
//		Chat.Insntace.GetMessage (message);
	}   

	private NetworkStream GetNetworkStream(){

		string ipOrHost = "127.0.0.1"; 
		int port = 10000;

//TcpClientを作成し、サーバーと接続する
		TcpClient tcp = new TcpClient(ipOrHost, port);
//TcpClient tcp = new TcpClient(System.Net.IPAddress.Any.ToString(), port);

		Debug.Log("success conn server");

//NetworkStreamを取得する
		return tcp.GetStream();
	}
}