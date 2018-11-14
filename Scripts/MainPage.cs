using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
#if UNITY_UWP
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking;
using Windows.Networking.Connectivity;
//using Windows.Networking.Sockets;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
#endif

public class MainPage :Main{
    TCPClientManager tcpClient;
    public TextMesh text;


     //#if UNITY_UWP

    //StreamSocket serverSocket;

    //StreamSocket clientSocket;

    //StreamSocketListener listener;

   // HostName localHost;

    string port = "10001";
    Data1 data1;

    public Data1 DataContext { get; }


    //この型はあるのか？
    public void Start()
    {
        //this.InitializeComponent();
        //localHost = NetworkInformation.GetHostNames().Where(q => q.Type == HostNameType.Ipv4).First();
        //text.Text = "hello";

        //多分うまく取れてないので注意
        data1 = new Data1();

        //this.DataContext = data1;
        tcpClient = new TCPClientManager();

        text.text = "Hello";
    }

    public async System.Threading.Tasks.Task SetTextAsync(String msg)
    {
        //await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => {
            //UI code here
           // text.Text = msg;
       // });
    }



    public override  void btnClientConnection()
    {
        //text.Text = "connecting";
        //clientSocket = new StreamSocket();
        //await clientSocket.ConnectAsync(localHost, port);
        //Debug.WriteLine("connected!");
        //text.Text = "connected";

        //data1.status = "aiueo";

        tcpClient.ConnectClient();
        text.text = "connect";
    }

    public override async void btnClientStart()
    {
        //var reader = new DataReader(clientSocket.InputStream);
        //uint sizeFieldCount = await reader.LoadAsync(sizeof(uint));

        //uint size = reader.ReadUInt32();

        //uint sizeFieldCount2 = await reader.LoadAsync(size);

        //var str = reader.ReadString(sizeFieldCount2);

        //Debug.WriteLine("client receive {0}", str);
        //text.Text = "receive : " +  str;
        tcpClient.SendMessage("start");
        text.text = "start";
    }



    public override async void btnClientSend()
    {
        //var writer = new DataWriter(clientSocket.OutputStream);
        //string str = "start";
        //writer.WriteUInt32(writer.MeasureString(str));
        //writer.WriteString(str);
        //await writer.StoreAsync();

        //Debug.WriteLine("client send");
        //text.Text = "send" + str;

        // Send a request to the echo server.

        //要：追記
        //string s = textBox1.Text;
        tcpClient. SendMessage("stop");

    }

    

    public override void Exit()
    {
        //serverSocket.Dispose();
        //clientSocket.Dispose();
        tcpClient.DisConnectClient();
    }
}
