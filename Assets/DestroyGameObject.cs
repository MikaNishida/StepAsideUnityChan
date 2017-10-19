using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGameObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// 画面外にでたゲームオブジェクトを順次削除
	void OnBecameInvisible() {
		Destroy (this.gameObject);
	}
}
