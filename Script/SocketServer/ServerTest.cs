using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;

/*
 * TestServer.cs
 * SocketServerを継承、開くポートを指定して、送受信したメッセージを具体的に処理する
 */
namespace Script.SocketServer
{
    public class ServerTest : SocketServer {
#pragma warning disable 0649
        // ポート指定（他で使用していないもの、使用されていたら手元の環境によって変更）
        [SerializeField] private int _port;
#pragma warning restore 0649

        //[SerializeField] private InputField _inputField;
        Console2 console2;



        private void Start() {
            // 接続中のIPアドレスを取得
            //			var ipAddress = Network.player.ipAddress;
            console2 = GameObject.Find("Client").GetComponent<Console2>();

            var ipAddress = "127.0.0.1";
            //			var ipAddress = "192.168.0.136";
            //			IPAddress ipAddress = "192.168."
            // 指定したポートを開く
            Listen(ipAddress, _port);
            //			ConnectToTcpServer();

            //			Connect();

            // システムに接続情報をセット（表示用）
            MyViewer.Instance.SetIpAddressPort(ipAddress + ":" + _port);

            //_inputField.text = "";
            //_inputField.ActivateInputField();
        }

        public void GetInput()
        {
            //String text = _inputField.text;
            //SendMessageToClient(text);
            Debug.Log("send msg : " + text + "\n");
            //_inputField.text = "";
        }

        public override void ShowMessage(String msg)
        {
            Debug.Log(msg);
            MyViewer.Instance.SetString(msg);
        }

        public void ShowData()
        {
            List<int> list = new List<int>();
           list = console2.GetSensorList1();
            foreach (int num in list){
                SendMessage(num.ToString());
            }
        }
		

		// クライアントからメッセージ受信
		
		protected override void OnMessage(string msg){
//			base.OnMessage(msg);
			
			// ------------------------------------------------
			// あとは送られてきたメッセージによって何かしたいことを書く
			// ------------------------------------------------

			// 今回は受信した整数値を表示用システムにセットする
			int num;
			// 整数値以外は何もしない
//			if (int.TryParse (msg, out num)) {
				// ビュアーに値をセットする
			ShowMessage(msg);
				// クライアントに受領メッセージを返す
			
			
			SendMessageToClient ("Accept:"+ msg +"\n");
//			Debug.Log("wanted to sent");
//			} else {
				// クライアントにエラーメッセージを返す
//				SendMessageToClient ("Error\n");
//			}
		}

	}
}