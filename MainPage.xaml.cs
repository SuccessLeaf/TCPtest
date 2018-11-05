using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking;
using Windows.Networking.Connectivity;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// 空白ページの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x411 を参照してください

namespace TCPtest
{
    /// <summar>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    /// 

        //サーバー作って通信どんな感じになるか確認

    public sealed partial class MainPage : Page
    {
        StreamSocket serverSocket;

        StreamSocket clientSocket;

        StreamSocketListener listener;

        HostName localHost;

        string port = "10001";
        Data1 data1;

        TCPCONNECTION tCPCONNECTION;
        public MainPage()
        {
            this.InitializeComponent();
            //localHost = NetworkInformation.GetHostNames().Where(q => q.Type == HostNameType.Ipv4).First();
            //text.Text = "hello";

            data1 = new Data1();
            this.DataContext = data1;
            tCPCONNECTION = new TCPCONNECTION();
        }

        

        private async void btnClientConnection_Click(object sender, RoutedEventArgs e)
        {
            //text.Text = "connecting";
            //clientSocket = new StreamSocket();
            //await clientSocket.ConnectAsync(localHost, port);
            //Debug.WriteLine("connected!");
            //text.Text = "connected";

            data1.status = "aiueo";

            tCPCONNECTION.start(data1,this);
        }

        private async void btnClientRecv_Click(object sender, RoutedEventArgs e)
        {
            //var reader = new DataReader(clientSocket.InputStream);
            //uint sizeFieldCount = await reader.LoadAsync(sizeof(uint));

            //uint size = reader.ReadUInt32();

            //uint sizeFieldCount2 = await reader.LoadAsync(size);

            //var str = reader.ReadString(sizeFieldCount2);

            //Debug.WriteLine("client receive {0}", str);
            //text.Text = "receive : " +  str;
            tCPCONNECTION.send("start");

        }

       

        private async void btnClientSend_Click(object sender, RoutedEventArgs e)
        {
            //var writer = new DataWriter(clientSocket.OutputStream);
            //string str = "start";
            //writer.WriteUInt32(writer.MeasureString(str));
            //writer.WriteString(str);
            //await writer.StoreAsync();

            //Debug.WriteLine("client send");
            //text.Text = "send" + str;

            // Send a request to the echo server.

            string s = textBox1.Text;
            tCPCONNECTION.send(s);
            
        }

        public async System.Threading.Tasks.Task SetTextAsync(String msg)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => {
                //UI code here
                text.Text = msg;
            });
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            //serverSocket.Dispose();
            //clientSocket.Dispose();
            tCPCONNECTION.send("exit");
        }
    }
}
