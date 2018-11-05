using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace TCPtest
{
    class TCPCONNECTION
    {
        Data1 data1;
        Socket socket;
        bool endRead;
        Task tasks;
        public void start(Data1 data1_,MainPage page)
        {
            data1 = data1_;
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            // ローカルホストの8081ポートのサーバへ接続
            socket.Connect("localhost", 10001);
            Debug.WriteLine("HELLO");
            data1.status = "HELLO";
            endRead = false; // 読み込み用スレッドの停止用

            // ログ読み取り用スレッド
            tasks = Task.Factory.StartNew(async () =>
            {
                // exitが入力されたら終了
                while (!endRead)
                {
                    if (socket.Available > 0)
                    {
                        // 受信バッファー分の配列を用意
                        byte[] recBytes = new byte[socket.ReceiveBufferSize];
                        // 受信データの取得
                        socket.Receive(recBytes, SocketFlags.None);
                        // 受信データを文字列に変換
                        string recStr = System.Text.Encoding.Unicode.GetString(recBytes).TrimEnd('\0');
                        // 受信文字列データを出力
                        if (!string.IsNullOrEmpty(recStr))
                        {
                            Debug.Write(recStr);
                            Debug.Write("> ");
                            //data1.status = recStr;
                            await page.SetTextAsync(recStr);
                        }
                    }
                    // SLEEP

                }
            });

            
        }

        public void send(string text)
        {
            // 改行コード
            byte[] newLine = { 0x0D, 0x0A };

            // 文字入力を受ける
            //string inputStr = Console.ReadLine();
            Debug.WriteLine(text);
            string inputStr = text;
            // exitの時は読み取りスレッド停止とループから抜ける
            if (inputStr.Equals("exit"))
            {
                endRead = true;


                // 受信スレッドを待機
                tasks.Wait();

                // 接続終了処理
                //socket.Shutdown(SocketShutdown.Both);
                //socket.Disconnect(false);
                socket.Dispose();
                return;

            }

            // 入力文字を送信
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(inputStr);
            socket.Send(inputBytes, inputBytes.Length, SocketFlags.None);
            // 改行コードを送信
            socket.Send(newLine, newLine.Length, SocketFlags.None);

            

        }
    }
}
