using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnityChanController : MonoBehaviour {
	

	// Use this for initialization

	// アニメーションするためのコンポーネントを入れる
	private Animator myAnimator;

	// unitychanに物理演算を適用するコンポーネントを入れる(動かす)
	private Rigidbody myRigidbody;

	// 前進する力
	private float forwardForce = 800.0f;

	// 左右に移動する力
	private float turnForce = 500.0f;

	// ジャンプする力
	private float upForce = 500.0f;

	// 左右の移動できる範囲
	private float movableRange = 3.4f;

	// 動きを減速させる係数
	private float coefficient = 0.95f;

	// ゲーム終了の判定
	private bool isEnd = false;

	// ゲーム終了時に表示するテキスト
	private GameObject stateText;

	// スコアを表示するテキスト
	private GameObject scoreText;

	// 左ボタン押下の判定
	private bool isLButtonDown = false;

	private bool isRButtonDown = false;

	// 得点
	private int score = 0;
		
	void Start () {
		// Animatorコンポーネントを取得
		this.myAnimator = GetComponent<Animator>();

		// 走るアニメーションを開始
		this.myAnimator.SetFloat("Speed", 1);

		// Rigidbodyコンポーネントを取得
		this.myRigidbody = GetComponent<Rigidbody>();

		// シーンの中のstateTextオブジェクトを取得
		this.stateText = GameObject.Find("GameResultText");

		// シーンの中のscoreTextオブジェクトを取得
		this.scoreText = GameObject.Find("ScoreText");
	}
	
	// Update is called once per frame
	void Update () {

		// ゲーム終了ならunitychanの動きを減速する
		if(this.isEnd) {
			this.forwardForce *= this.coefficient;
			this.turnForce *= this.coefficient;
			this.upForce *= this.coefficient;
			this.myAnimator.speed *= this.coefficient;
		}

		// unitychanに前方向の力を加える
		// Rigidbodyクラスの「AddForce」関数は、引数で指定した方向の力をRigidbodyにかける関数
		this.myRigidbody.AddForce (this.transform.forward * this.forwardForce);

		// unitychanを矢印キーまたはボタンに応じて左右に移動させる
		if((Input.GetKey(KeyCode.LeftArrow) || this.isLButtonDown) && -this.movableRange < this.transform.position.x) {
			// 左に移動
			this.myRigidbody.AddForce(-this.turnForce, 0, 0);
		} else if((Input.GetKey(KeyCode.RightArrow) || this.isRButtonDown) && this.transform.position.x < this.movableRange) {
			this.myRigidbody.AddForce (this.turnForce, 0, 0);
		}

		// unitychanをジャンプさせる
		// 先にunitychanのAnimatorコンポーネントの
		// Jumpステートの場合(ジャンプ中)はJumpにfalseをセットする
		if(this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump")) {
			this.myAnimator.SetBool ("Jump", false);
		}

		// ジャンプしてない時にspace押されたらジャンプする
		if(Input.GetKeyDown(KeyCode.Space) && this.transform.position.y < 0.5f) {
			// ジャンプアニメを再生
			this.myAnimator.SetBool("Jump", true);
			// unitychanに上方向の力を加える
			this.myRigidbody.AddForce(this.transform.up * this.upForce);
		}
	}

	// トリガーモードで他のオブジェクトと接触した場合の処理
	void OnTriggerEnter(Collider other) {
		// 障害物に衝突した場合
		if(other.gameObject.tag == "CarTag" || other.gameObject.tag == "TrafficConeTag") {
			this.isEnd = true;
			// stateTextにGAME OVERテキストを表示
			this.stateText.GetComponent<Text>().text = "GAME OVER";
		}

		// ゴール地点に到達した場合
		if(other.gameObject.tag == "GoalTag") {
			this.isEnd = true;
			// stateTextにGAME OVERテキストを表示
			this.stateText.GetComponent<Text>().text = "CLEAR!!";
		}

		// コインに衝突した場合
		if(other.gameObject.tag == "CoinTag") {

			// スコアを加算
			this.score += 10;

			// ScoreTextに獲得した点数を表示
			this.scoreText.GetComponent<Text>().text = "Score" + this.score + "pt";

			// パーティクルを再生
			// ParticleSystemクラスの「Play」関数を呼ぶとパーティクルが再生される。
			GetComponent<ParticleSystem>().Play();

			// 接触したコインのオブジェクトを破棄
			Destroy(other.gameObject);
		}
	}

	// ジャンプボタンを押した場合の処理
	public void GetMyJumpButtonDown() {
		if(this.transform.position.y < 0.5f) {
			this.myAnimator.SetBool ("Jump", true);
			this.myRigidbody.AddForce (this.transform.up * this.upForce);
		}
	}

	//左ボタンを押し続けた場合の処理
	public void GetMyLeftButtonDown() {
		this.isLButtonDown = true;
	}
	// 左ボタンを話し続けた場合の処理
	public void GetMyLeftButtonUp() {
		this.isLButtonDown = false;
	}
	// 右ボタンを押し続けた場合の処理
	public void GetMyRightButtonDown() {
		this.isRButtonDown = true;
	}
	// 右ボタンを話した場合の処理
	public void GEtMyRightButtonUp() {
		this.isRButtonDown = false;
	}
}
