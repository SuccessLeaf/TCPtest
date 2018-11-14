using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Console2 : MonoBehaviour
{
    //[SerializeField] private String IPAdress;
    //[SerializeField] private int port;
    public static int a = 0;
    private Socket socket;
    private Task tasks;
    // 改行コード
    readonly byte[] newLine = { 0x0D, 0x0A };
    List<int> timeList; 
    List<int> sensor1List;
    List<int> sensor2List;
    List<int> sensor3List;
    List<int> sensor4List;
    List<double> calList1;


    private bool startRecord;
    private bool endRead;

    private int count;

    private int index;
    int num = 0;


    public DD_DataDiagram diagram;
    private GameObject line;
    private float time;

    private void Update()
    {
        //Debug.Log(num);
        if (num > 10)
        {
            Debug.Log("show charts");
            ShowCharts();
            num = 0;
        }

    }

    public void ClientStart()
        {
        timeList = new List<int>();
        sensor1List = new List<int>();
        sensor2List = new List<int>();
        sensor3List = new List<int>();
        sensor4List = new List<int>();
        index = 0;
        calList1 = new List<double>();
        line = diagram.AddLine("muscle", Color.red);
        // ソケットをTCPプロトコルで生成
        // AddressFamily.InterNetwork: IPv4で接続(IPv6の場合はInterNetworkV6)
        // SocketType.Stream: TCPを使うので双方向のバイトストリームをサポートするStream
        // ProtocolType.Tcp: TCPなのでTcp
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            // ローカルホストの8081ポートのサーバへ接続
            socket.Connect("localhost", 10001);
            //Console.WriteLine("HELLO");
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
                           // Debug.Log(recStr);
                            String[] strings = recStr.Split(',');
                            if (!startRecord)
                            {
                                for (int i = 0; i < strings.Length; i++)
                                {
//                                    Debug.Log(i + " : " + strings[i]);
                                    
                                }
                                foreach (string s in strings)
                                {
                                    if (s.Contains("ead"))
                                    {
                                        startRecord = true;
                                      //  Debug.Log("start");
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                num++;
                                String[] separator = {"eadffff", "ead2222"};
                                String[] lineSplit = recStr.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                             
                                foreach (var line in lineSplit)
                                {

                                   // Debug.Log(line);
                                    String[] elements = line.Split(',');
                                    foreach (string element in elements)
                                    {
                                        //Debug.Log(element);
                                    }
                         
                                    try
                                    {
                                        timeList.Add(Int32.Parse(elements[1]));
                                        sensor1List.Add(Int32.Parse(elements[2]));

                                                                            }
                                    catch (Exception ex)
                                    {
                                        Debug.Log(ex.ToString());
                                        throw;
                                    }
                                   

                                    count++;
                                }
                            
                            }
                           

                            //                            Debug.Log("size" + strings.Length);

                            //                            Console.Write(recStr);
                            //                            Console.Write("> ");
                        }
                    }
                    // 速すぎるとCPUをバカ食いするので
                    //                    System.Threading.Thread.Sleep(100);
                   
                }
            });
        }

    public async Task Work(String line)
    {
       
        //sensor2List.Add(Int32.Parse(elements[3]));
        //sensor3List.Add(Int32.Parse(elements[4]));
        //sensor4List.Add(Int32.Parse(elements[5]));

        //Debug.Log("see you");
    }

    public List<int> GetSensorList1()
    {
        return sensor1List;
    }

    public void CalData()
    {
        int num = 0;
        double sum = 0;
        double result = 0;
        int av = 5;
        double sens = 0;
       // Debug.Log("num : " + sensor1List.Count);
        for (; index < sensor1List.Count; index++)
        {
            sens = Convert.ToDouble(sensor1List[index]);
            sens *= 0.073 * 0.1; //μVの値へ変換
            sum += Math.Pow((sens), 2.0);
            num++;
           // Debug.Log("sum : " + sum);
            if (num >= av)
            {
               // Debug.Log(result);
                sum /= av;
               // Debug.Log(result);
                result = Math.Sqrt(sum);
                //Debug.Log(result);
                calList1.Add(result);
                num = 0;
                sum = 0;
                
            }




        }
        
    }

    public void ShowCharts ()
    {
        Debug.Log("called");
        CalData();
        //Debug.Log("LIST " + calList1.Count);
        foreach(double d in calList1)
        {
            Debug.Log("chart " + d);
            diagram.InputPoint(line, new Vector2(1, (float)d));
        }
    }

        public void SendMesssage(string inputStr)
        {
        Debug.Log(inputStr);
            if (!endRead)
            {
                if (inputStr.Equals("exit"))
                {
                    endRead = true;
                    EndTask();
                    return;
                }

                // 入力文字を送信
                byte[] inputBytes = System.Text.Encoding.UTF8.GetBytes(inputStr);
                socket.Send(inputBytes, inputBytes.Length, SocketFlags.None);
                // 改行コードを送信
                socket.Send(newLine, newLine.Length, SocketFlags.None);
            }
        }

        public void EndTask()
        {
            // 受信スレッドを待機
            tasks.Wait();

            // 接続終了処理
           // socket.Shutdown(SocketShutdown.Both);
            //socket.Disconnect(false);
            socket.Dispose();
        }

    public void ShowCount()
    {
     Debug.Log(count);   
    }

    public void SizeCount()
    {
//        Debug.Log();
    }
    
    public void ShowList1(){
        Debug.LogError("show!!");
        Debug.Log(sensor1List.Count);
        foreach (int i in sensor1List)
        {
            Debug.Log(i);

        }
    }
    }
 