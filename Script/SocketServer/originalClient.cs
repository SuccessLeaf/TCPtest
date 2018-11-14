using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using UnityEngine;

public class originalClient : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
		IPAddress ipAddress = IPAddress.Loopback;
		Debug.Log(ipAddress);
//		DoSomething();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public async Task DoSomething()
	{
		Debug.Log("before : " + Thread.CurrentThread.ManagedThreadId);

		Task<int> task = Task.Run(new Func<int>(Calculate));
//		for (int i = 0; i < 10; i++)
//		{
			Debug.Log("before2 : " + Thread.CurrentThread.ManagedThreadId);
//		}
		int result =  task.Result;
		Debug.Log("after : " + Thread.CurrentThread.ManagedThreadId);

		Debug.Log(result);
	}

	int Calculate()
	{
		int total = 0;
		for (int i = 0; i < 100; i++)
		{
			total += i;
//			Debug.Log(i);
		}
		Thread.Sleep(1000);
		Debug.Log("in : " + Thread.CurrentThread.ManagedThreadId);
		return total;
	}
}
