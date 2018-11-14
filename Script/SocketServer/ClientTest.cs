using System;
using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEngine;
using UnityEngine.UI;

public class ClientTest : Console2 {


    [SerializeField] private InputField _inputField;

    [SerializeField] private Text _text;


    // Use this for initialization
    void Start() {
        //		ConnectToTcpServer();
        //		Connect("localhost");
        _inputField.text = "";
        _inputField.ActivateInputField();
    }

    public void GetInput()
    {
        String text = _inputField.text;
        if (text.Equals("show1"))
        {
            ShowList1();
        }
        else if (text.Equals("count"))
        {
            ShowCount();
        }
        else if (text.Equals(""))
        {

        }
        else
        {
            SendMesssage(text);

        }
        //		Debug.Log("send msg : " + text+"\n");
        _inputField.text = "";
    }

    public void BConnect()
    {
        ClientStart();
    }

    public void BStart()
    {
        //Debug.Log("start!");
        SendMesssage("start");
    }

    public void BStop()
    {
        SendMesssage("stop");
    }

    public void BExit()
    {
        EndTask();
    }

    //	public override void ShowMessage(String msg)
    //	{
    //		Debug.Log(msg);
    //		MyViewer.Instance.SetString(msg); 
    //	}
}
