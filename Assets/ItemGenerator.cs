using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour {

	// carPrefabを入れる
	public GameObject carPrefab;

	// coinPrefabを入れる
	public GameObject coinPrefab;

	// conePrefabを入れる
	public GameObject conePrefab;

	// Main Cameraを入れる
	public GameObject mainCamera;
	// スタート地点
	private int startPos = -160;

	// ゴール地点
	private int goalPos = 120;

	// アイテムを出すx軸方向の範囲
	private float posRange = 3.4f;

	// アイテムを出すz軸方向の範囲(前方50mまでアイテム生成)
	private float posDis = 50f;

	// アイテムを出す座標のz軸の位置
	private float posZ;





	// Use this for initialization
	void Start () {

		// MainCameraオブジェクトを取得
		this.mainCamera = GameObject.Find("Main Camera");

		// アイテムを出す座標のz軸の位置をスタート地点に合わせて初期化
		// 一度アイテムを生成した後は、一つ前のアイテム生成場所の座標を保存しておく
		posZ = startPos;


//		// 一定の距離ごとにアイテムを生成
//		for(int i = startPos; i < goalPos; i += 15) {
//			// どのアイテムを出すかランダムに設定
//			int num = Random.Range(0, 10);
//			if(num <= 1) {
//				// コーンをx軸方向に一直線に生成
//				for(float j = -1; j <= 1; j += 0.4f) {
//					GameObject cone = Instantiate(conePrefab) as GameObject;
//					cone.transform.position = new Vector3(4 * j, cone.transform.position.y, i);
//				}
//			} else {
//				// レーンごとにアイテムを生成
//				for(float j = -1; j < 2; j++) {
//					// アイテムの種類を決める
//					int item = Random.Range(1, 11);
//					// アイテムをおくz座標のオフセットをランダムに設定
//					int offsetZ = Random.Range(-5, 6);
//					// 60%コイン配置:30%車配置:10%何もなし
//					if(1 <= item && item <= 6) {
//						// コインを生成
//						// coinPrefabからインスタンスを生成　→  「Instantiate () as GameObject」は、()内に指定したPrefabのインスタンスをGameObject型として生成します。
//						// また生成したインスタンスは、GameObject型の変数に代入します。
//						GameObject coin = Instantiate(coinPrefab) as GameObject;
//						coin.transform.position = new Vector3(posRange * j, coin.transform.position.y, i + offsetZ);
//					} else if(7 <= item && item <= 9) {
//						// 車を生成
//						GameObject car = Instantiate(carPrefab) as GameObject;
//						car.transform.position = new Vector3(posRange * j, car.transform.position.y, i + offsetZ);
//					}
//				}
//			}
//		}
	}
	
	// Update is called once per frame
	void Update () {
		// mainCameraのz軸方向の位置+mainCameraから先50fの位置の座標が、posZ以上でgoalPosより小さい時
		if(mainCamera.transform.position.z + posDis >= posZ && posZ < goalPos) {
			// posZの位置にアイテムを生成
			ItemGenerate (posZ);
			// 15メートル間隔でアイテム出す
			posZ += 15f;
		}
	}

	void ItemGenerate(float posZ) {
		int num = Random.Range(0, 10);
		if(num <= 1) {
			// コーンをx軸方向に一直線に生成
			for(float j = -1; j <= 1; j += 0.4f) {
				GameObject cone = Instantiate(conePrefab) as GameObject;
				cone.transform.position = new Vector3(4 * j, cone.transform.position.y, posZ);
			}
		} else {
			// レーンごとにアイテムを生成
			for(float j = -1; j < 2; j++) {
				// アイテムの種類を決める
				int item = Random.Range(1, 11);
				// アイテムをおくz座標のオフセットをランダムに設定
				int offsetZ = Random.Range(-5, 6);
				// 60%コイン配置:30%車配置:10%何もなし
				if(1 <= item && item <= 6) {
					// コインを生成
					// coinPrefabからインスタンスを生成　→  「Instantiate () as GameObject」は、()内に指定したPrefabのインスタンスをGameObject型として生成します。
					// また生成したインスタンスは、GameObject型の変数に代入します。
					GameObject coin = Instantiate(coinPrefab) as GameObject;
					coin.transform.position = new Vector3(posRange * j, coin.transform.position.y, posZ + offsetZ);
				} else if(7 <= item && item <= 9) {
					// 車を生成
					GameObject car = Instantiate(carPrefab) as GameObject;
					car.transform.position = new Vector3(posRange * j, car.transform.position.y, posZ + offsetZ);
				}
			}
		}
	}

		
}
