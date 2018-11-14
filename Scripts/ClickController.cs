using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class ClickController : MonoBehaviour, IInputClickHandler{
    public void OnInputClicked(InputClickedEventData eventData)
    { // 何もGazeしていないときだけ動作する
        if (GazeManager.Instance.HitObject) {
            // ログを吐くだけ
            Debug.Log("Click on something point !!!!");
        }
    }
      // Use this for initialization
        void Start () {
        InputManager.Instance.AddGlobalListener(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
