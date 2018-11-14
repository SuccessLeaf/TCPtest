using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
#if UNITY_UWP
using System.IO;
using System.Threading.Tasks;
using Windows.Networking;
using Windows.Networking.Sockets;
#endif

#if UNITY_UWP
    private StreamWriter writer = null;
#endif

public class HoloClient : MonoBehaviour
{



    public HoloClient(string IP,int port)
    { 
#if UNITY_UWP
        Task.Run(async () => {
            StreamSocket socket = new StreamSocket();
            await socket.ConnectAsync(new HostName(IP),port.ToString());
            writer = new StreamWriter(socket.OutputStream.AsStreamForWrite());
            StreamReader reader = new StreamReader(socket.InputStream.AsStreamForRead());
            try
            {
                string data = await reader.ReadToEndAsync();
            }
            catch (Exception) { }
            writer = null;
        });
#endif
    }

    public void SendMessage(string data)
    {
#if UNITY_UWP
        if (writer != null) Task.Run(async () =>
        {
            await writer.WriteAsync(data);
            await writer.FlushAsync();
        });
#endif
    }
}

