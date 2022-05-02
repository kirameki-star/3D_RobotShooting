using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RedDragonController : MonoBehaviour
{
	//攻撃判定用オブジェクト
	//public GameObject EnemyAttackPrefab;

	private int StateNumber = 0;

	//汎用タイマー
    private float timeCounter = 0f;

	//攻撃用タイマー
	private float attacktimeCounter = 0f;

    //プレイヤーのオブジェクト
    public GameObject player;

	public GameObject enemyAttackPrefab;

    //アニメーションするためのコンポーネントを入れる
    private Animator myAnimator;

	//スタートの位置を記録
	private Vector3 StartPosition;

	//移動させるコンポーネントを入れる
	private Rigidbody myRigidbody;

	//移動量
	private float velocity = 1.0f;

	//地面に接触
	private bool isGround = false;

	//壁に当たった
	private bool isWall = false;

	//ダメージ有り
	private bool isDamage = false;

	//ダメージキャンセル
	private float DamageCancel = 0f;

	//死亡フラグ
	private bool isDie = false;

	private Collider attack;

	//敵のHP
	public int life = 10;  //HP

	//NavMeshAgent型を変数agentで宣言
	NavMeshAgent agent;

	private float ClipDirection360(float direction)
	{
		if (direction < 0.0f)
		{
			return 360.0f + direction;
		}
		if (direction >= 360.0f)
		{
			return direction - 360.0f;
		}
		return direction;
	}

	private float ClipDirection180(float direction)
	{
		direction = ClipDirection360(direction);
		direction = direction + 180.0f;
		if (direction >= 360.0f)
		{
			direction = direction - 360.0f;
		}
		return direction - 180.0f;
	}

	private float GetDirection(float x1, float y1, float x2, float y2)
	{
		if ((x2 - x1) == 0.0f && (y2 - y1) == 0.0f)
		{
			return 0.0f;
		}
		else
		{
			return ClipDirection360((((Mathf.PI / 2.0f) - Mathf.Atan2(y1 - y2, x1 - x2)) * (180.0f / Mathf.PI)) + 180.0f);
		}
	}

	private float GetLength(float x1, float y1, float x2, float y2)
	{
		return Mathf.Sqrt(((x1 - x2) * (x1 - x2)) + ((y1 - y2) * (y1 - y2)));
	}

	private void Moving()
	{
		//向いている方向を得る（オイラー）
		float direction = this.transform.rotation.eulerAngles.y;

	}


	// Start is called before the first frame update
	void Start()
    {
		//GetComponentでNavMeshAgentを取得して
		//変数agentで宣言します。
		agent = GetComponent<NavMeshAgent>();

		//初期位置の記録
		StartPosition = this.transform.position;

		//Playerのオブジェクトを取得
		this.player = GameObject.Find("Player");

		//攻撃判定用オブジェクトを取得
		this.enemyAttackPrefab = GameObject.Find("EnemyAttackPrefab");

		//アニメータコンポーネントを取得
		this.myAnimator = GetComponent<Animator>();

		//Rigidbodyコンポーネントを取得
		this.myRigidbody = GetComponent<Rigidbody>();

		//攻撃判定用オブジェクトのBoxColliderコンポーネントを取得
		attack = enemyAttackPrefab.GetComponent<BoxCollider>();

	}
	

	// Update is called once per frame
	void Update()
	{
		//プレイヤーの方向
		float direction = GetDirection(this.transform.position.x, this.transform.position.z, player.transform.position.x, player.transform.position.z);
		//プレイヤーの距離
		float length = GetLength(this.transform.position.x, this.transform.position.z, player.transform.position.x, player.transform.position.z);

		//Debug.Log("距離"+length);
		//Debug.Log("方向" + direction);
		
		//プレイヤーに近づいたら攻撃
		if (length < 7.0f)
        {
			//this.myAnimator.SetTrigger("Attack");
			this.myAnimator.SetInteger("Attack", 1);
			attacktimeCounter = 0f;

		    //攻撃時のみ攻撃判定用オブジェクトの衝突判定をオンにする
			attack.isTrigger = false;

		}


		attacktimeCounter += Time.deltaTime;

		//攻撃の後に攻撃判定用オブジェクトの衝突判定をオフにする
		if (attacktimeCounter > 1.5f　&& attack.isTrigger == false)
        {
			attack.isTrigger = true;
			this.myAnimator.SetInteger("Attack", 0);

		}
		//死亡フラグがfalseのとき
		if (isDie == false)
        {
			//Nav Mesh Agentが設定されたObjectがPlayerGameObjectの後を追いかける
			agent.destination = player.transform.position;
			this.myAnimator.SetFloat("Speed", 2);

			//Debug.Log("" + target.transform.position);
		}
			
		//敵の体力が0以下で死亡フラグがfalseのとき
		if (life <= 0 && isDie == false)
		{
			this.myAnimator.SetTrigger("Die");
			timeCounter = 0f;
			
			//死亡フラグをtrueにする
			isDie = true;
			//Nav Mesh Agentに自身の現在座標を取得させ動かないようにする
			agent.destination = this.transform.position;
		}
		
		//死亡してから4秒後にオブジェクト破棄
		if(isDie == true)
        {
			timeCounter += Time.deltaTime;

			if (timeCounter > 4f)
			{
				Destroy(this.gameObject);
			}
		}

	}
	void OnTriggerEnter(Collider other)
    {
		//ShellTag（銃弾）に当たったら体力を減らす
		if (other.gameObject.tag == "ShellTag")
        {
			life--;
        }

	}
	
}
