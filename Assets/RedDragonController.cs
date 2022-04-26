using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedDragonController : MonoBehaviour
{
    private int StateNumber = 0;

    private float TimeCounter = 0f;

    //プレイヤーのオブジェクト
    private GameObject player;

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

	//ライフ
	private int Life = 3;

	//死亡
	private bool isDie = false;

	//敵のHP
	public int hitPoint = 100;  //HP

	//--------------------------------------------------------------------------------
	// 角度クリップ
	//--------------------------------------------------------------------------------

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

	//--------------------------------------------------------------------------------
	// 方向・角度
	//--------------------------------------------------------------------------------

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

	// Start is called before the first frame update
	void Start()
    {
		//初期位置の記録
		StartPosition = this.transform.position;

		//Playerのオブジェクトを取得
		this.player = GameObject.Find("Player");

		//アニメータコンポーネントを取得
		this.myAnimator = GetComponent<Animator>();

		//Rigidbodyコンポーネントを取得
		this.myRigidbody = GetComponent<Rigidbody>();

		//初期の待機タイマーをランダムにする
		TimeCounter = Random.Range(-3f, 0f);
	}
	private void Moving()
	{
		//向いている方向を得る（オイラー）
		float direction = this.transform.rotation.eulerAngles.y;

		//歩くアニメーションを開始
		this.myAnimator.SetFloat("Speed", velocity);

		//速度を与える（オイラー→ラジアン）※Y軸のvelocityスルー
		this.myRigidbody.velocity = new Vector3(Mathf.Sin(direction * Mathf.Deg2Rad) * velocity, this.myRigidbody.velocity.y, Mathf.Cos(direction * Mathf.Deg2Rad) * velocity);
	}

	/*private void StateMachine()
	{
		//ユニティちゃんの方向
		float direction = GetDirection(this.transform.position.x, this.transform.position.z, player.transform.position.x, player.transform.position.z);
		//ユニティちゃんの距離
		float length = GetLength(this.transform.position.x, this.transform.position.z, player.transform.position.x, player.transform.position.z);

		//ステート振り分け
		switch (StateNumber)
		{
			//3秒待機
			case 0:
				{   //3秒動かない
					if (TimeCounter > 3f)
					{
						//X,Z固定
						//GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;

						//クリアー
						TimeCounter = 0f;

						//プレイヤーの距離が10m以上なら【ちょっと動く】
						if (length > 10f)
						{
							//ランダムで少し方向を変える
							this.transform.Rotate(new Vector3(0f, Random.Range(-30f, 30f), 0f));
							//遷移 ※ちょっと動く（索敵）
							StateNumber = 1;
						}
						else
						{
							//視界の範囲に入っている？
							if (Mathf.DeltaAngle(this.transform.rotation.eulerAngles.y, direction) > -60f && Mathf.DeltaAngle(this.transform.rotation.eulerAngles.y, direction) < 60f)
							{
								//回転
								this.transform.Rotate(new Vector3(0f, Mathf.DeltaAngle(this.transform.rotation.eulerAngles.y, direction), 0f));
								//遷移 ※プレイヤーに近づく
								StateNumber = 2;
							}
							else
							{
								//ランダムで少し方向を変える
								this.transform.Rotate(new Vector3(0f, Random.Range(-30f, 30f), 0f));
								//遷移 ※ちょっと動く（索敵）
								StateNumber = 1;
							}
						}
					}
				}
				break;

			//ちょっと動く（索敵）
			case 1:
				{   //移動
					Moving();

					//3秒経ったら3秒待機に戻す
					if (TimeCounter > 3f)
					{
						//最初の地点からの距離を求める
						float lengthmax = GetLength(StartPosition.x, StartPosition.z, this.transform.position.x, this.transform.position.z);

						//7メートル離れた
						if (lengthmax > 7f)
						{
							//ランダムで反対方向へ変える
							this.transform.Rotate(new Vector3(0f, Random.Range(150f, 210f), 0f));
						}

						//クリアー
						TimeCounter = 0f;
						//歩くアニメーションを停止
						this.myAnimator.SetFloat("Speed", 0f);
						//遷移 ※3秒待機
						StateNumber = 0;
					}

					//壁に衝突した
					if (isWall && isGround)
					{
						//ランダムで反対方向へ変える 2021/12/27 ３０°から反対にした。
						this.transform.Rotate(new Vector3(0f, Random.Range(150f, 210f), 0f));
						//クリアー
						TimeCounter = 0f;
						//歩くアニメーションを停止
						this.myAnimator.SetFloat("Speed", 0f);
						//ジャンプのアニメーション
						this.myAnimator.SetTrigger("Jump");
						//ジャンプ
						StateNumber = 4;
					}
				}
				break;

			//プレイヤーに近づく
			case 2:
				{   //移動
					Moving();

					//7秒経ったら3秒待機に戻す
					if (TimeCounter > 7f)
					{
						//クリアー
						TimeCounter = 0f;
						//歩くアニメーションを停止
						this.myAnimator.SetFloat("Speed", 0f);
						//遷移 ※3秒待機
						StateNumber = 0;
					}

					//壁に衝突した
					if (isWall && isGround)
					{
						//クリアー
						TimeCounter = 0f;
						//歩くアニメーションを停止
						this.myAnimator.SetFloat("Speed", 0f);
						//ジャンプのアニメーション
						this.myAnimator.SetTrigger("Jump");
						//ジャンプ
						StateNumber = 4;
					}

					//攻撃が届く距離？
					if (length < 1f)
					{
						//クリアー
						TimeCounter = 0f;
						//歩くアニメーションを停止
						this.myAnimator.SetFloat("Speed", 0f);
						//攻撃のアニメーション
						this.myAnimator.SetTrigger("Attack");
						//遷移 ※攻撃
						StateNumber = 3;
					}
				}
				break;

			//攻撃
			case 3:
				{   //1秒経った？
					if (TimeCounter > 1f)
					{
						//クリアー
						TimeCounter = 0f;
						//遷移 ※3秒待機
						StateNumber = 0;
					}
				}
				break;

			//ジャンプ
			case 4:
				{   //移動
					Moving();

					//3秒経ったら3秒待機に戻す
					if (TimeCounter > 3f)
					{
						//クリアー
						TimeCounter = 0f;
						//遷移 ※3秒待機
						StateNumber = 0;
					}
				}
				break;
			
			case 5:
                {
					if (TimeCounter > 1f)
					{
						Destroy(this.gameObject);
					}
				}
				break;
			default: break;
		}
	}*/

	// Update is called once per frame
	void Update()
	{
		if (hitPoint <= 0)
		{
			this.myAnimator.SetTrigger("Die");
			TimeCounter = 0f;
			StateNumber = 5;

		}
	}
    //ダメージを受け取ってHPを減らす関数
    public void Damage(int damage)
    {
        //受け取ったダメージ分HPを減らす
        hitPoint -= damage;
    }

    void OnTriggerEnter(Collider other)
    {

        //ぶつかったオブジェクトのTagにShellという名前が書いてあったならば（条件）.
        if (other.CompareTag("ShellTag"))
        {
            //HPを減らす
            hitPoint--;
        }
    }

}
