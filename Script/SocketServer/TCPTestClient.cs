using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class TCPTestClient : MonoBehaviour {  	
	#region private members 	
	private TcpClient socketConnection; 	
	private Thread clientReceiveThread; 	
	#endregion


//	 Use this for initialization 	
//	void Start () {
//		ConnectToTcpServer();     
//	}  	
//	// Update is called once per frame
//	void Update () {         
//		if (Input.GetKeyDown(KeyCode.Space)) {             
//			SendMessage("getd");         
//		}     
//	}  	
	/// <summary> 	
	/// Setup socket connection. 	
	/// </summary> 	
	public void ConnectToTcpServer () { 		
		try {  			
			clientReceiveThread = new Thread (new ThreadStart(ListenForData)); 			
			clientReceiveThread.IsBackground = true; 			
			clientReceiveThread.Start();  		
		} 		
		catch (Exception e) { 			
			Debug.Log("On client connect exception " + e); 		
		} 	
	}  	
	
//	public void 
	/// <summary> 	
	/// Runs in background clientReceiveThread; Listens for incomming data. 	
	/// </summary>     
	private void ListenForData() { 		
		try { 			
			socketConnection = new TcpClient("192.168.0.137", 10000);  		
			Debug.Log("connect");
			Byte[] bytes = new Byte[1024];             
			while (true) { 				
				// Get a stream object for reading 				
				using (NetworkStream stream = socketConnection.GetStream()) { 					
					int length; 					
					// Read incomming stream into byte arrary. 					
					while ((length = stream.Read(bytes, 0, bytes.Length)) != 0) { 						
						var incommingData = new byte[length]; 						
						Array.Copy(bytes, 0, incommingData, 0, length); 						
						// Convert byte array to string message. 						
						string serverMessage = Encoding.ASCII.GetString(incommingData); 						
						ShowMessage(serverMessage);
//						Debug.Log("server message received as: " + serverMessage);
//						ShowMessage(serverMessage);

					} 				
				} 			
			}         
		}         
		catch (SocketException socketException) {             
			Debug.Log("Socket exception: " + socketException);         
		}     
	}  	
	
	public virtual void ShowMessage(String msg)
	{
		Debug.Log(msg);
	}
	
	
	/// <summary> 	
	/// Send message to server using socket connection. 	
	/// </summary> 	
	public void SendMessage(String text) {         
		if (socketConnection == null) {  
			Debug.Log("null connection");
			return;         
		}  		
		try { 	
//			Debug.Log("try to send");
			// Get a stream object for writing. 
			
			//うまく行かない
			//ここやる
			Byte[] data = System.Text.Encoding.UTF8.GetBytes(text);
			NetworkStream stream = socketConnection.GetStream(); 	
			
//			try{
				stream.Write(data, 0, data.Length);
				Debug.Log("send : " + text);
//			}catch {
//			}
//			Console.WriteLine("送信: {0}",text);
//			if (stream.CanWrite) {                 
//				string clientMessage = text; 				
//				// Convert string message to byte array.                 
//				byte[] clientMessageAsByteArray = Encoding.ASCII.GetBytes(clientMessage); 				
//				// Write byte array to socketConnection stream.                 
//				stream.Write(clientMessageAsByteArray, 0, clientMessageAsByteArray.Length);                 
//				Debug.Log("Client sent his message - should be received by server");             
//			}         
		} 		
		catch (SocketException socketException) {             
			Debug.Log("Socket exception: " + socketException);         
		}     
	} 
}