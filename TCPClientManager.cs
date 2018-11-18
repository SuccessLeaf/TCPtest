using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking;
using Windows.Networking.Sockets;
using System.IO;
using System.Diagnostics;
using Windows.Storage.Streams;

namespace TCPtest
{
    public class TCPClientManager 
{
    public delegate void ListenerMessageEventHandler(string ms);
    public ListenerMessageEventHandler ListenerMessageEvent;

    public delegate void ListenerByteEventHandler(byte[] data);
    public ListenerByteEventHandler ListenerByteEvent;

    string ipaddress = "localhost";
    int port = 10001;

        DataWriter writer;

        //#if WINDOWS_UWP
        //private StreamWriter writer = null;
        private Task writetask = null;

//#endif
    private bool isActiveThread = true;

    public TCPClientManager() { }

    public TCPClientManager(string ipaddress, int port)
    {
        //ConnectClient(ipaddress, port);
    }



    public void ConnectClient()
    {
//#if WINDOWS_UWP
        Task.Run(async () =>
        {
            Debug.WriteLine("hello");
            StreamSocket socket = new StreamSocket();
            await socket.ConnectAsync(new HostName(ipaddress), port.ToString());
            Debug.WriteLine("HELLO");

            writer = new DataWriter(socket.OutputStream);
            //writer = new StreamWriter(socket.OutputStream.AsStreamForWrite(), Encoding.Unicode);
            Debug.WriteLine("HELLO");

            StreamReader reader = new StreamReader(socket.InputStream.AsStreamForRead());
            byte[] bytes = new byte[65536];
            Debug.WriteLine("connect");
            bool isOnce = true;
            while (isActiveThread)
            {
                try
                {

                    //int num = await reader.BaseStream.ReadAsync(bytes, 0, bytes.Length);
                    //if (isOnce)
                    //{
                    //    Debug.WriteLine("receving");
                    //    isOnce = false;
                    //}
                    //if (num > 0)
                    //{
                    //    byte[] data = new byte[num];
                    //    Array.Copy(bytes, 0, data, 0, num);
                    //    Debug.WriteLine("getMess");
                    //    if (ListenerMessageEvent != null) ListenerMessageEvent(Encoding.UTF8.GetString(data));
                    //    if (ListenerByteEvent != null) ListenerByteEvent(data);
                    //}
                }
                catch (Exception e)
                {
                    //エラー出た
                     Debug.WriteLine(e);
                }
            }
            socket.Dispose();
            if (writer != null)
            {
                writer.Dispose();
            }
            writer = null;
        });
//#endif
    }

    public void SendMessage(string ms)
    {
            Debug.WriteLine("want to send");
            byte[] bytes = Encoding.UTF8.GetBytes(ms);
           SendMessage(bytes);
            
    }

    public async void SendMessage(byte[] data)
    {
//#if WINDOWS_UWP
            if (writetask == null || writetask.IsCompleted == true)
            {
                if (writer != null)
                {
                    //writetask = Task.Run(async () =>
                    //{
                        try
                        {
                            //Debug.WriteLine("sending");
                            //await writer.BaseStream.WriteAsync(data, 0, data.Length);
                            //Debug.WriteLine("half done");
                            //await writer.FlushAsync();
                            string stringToSend = "start";
                            writer.WriteUInt32(writer.MeasureString(stringToSend));
                            writer.WriteString(stringToSend);
                        writer.UnicodeEncoding = Windows.Storage.Streams.UnicodeEncoding.Utf8;
                            Debug.WriteLine("sending");
                            await writer.StoreAsync();
                        Debug.WriteLine("sent");

                    }
                    catch (Exception e)
                        {
                            Debug.WriteLine(e);
                        }
                    //});
                }
            }
            //#endif

            //return false;
    }


    public void DisConnectClient()
    {
//#if WINDOWS_UWP
            //if (writer!=null)
            //{
            //    writer.Dispose();
            //}
            //writer = null;
    }
//#endif
    }
}