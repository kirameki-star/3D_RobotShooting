using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //アニメーションするためのコンポーネントを入れる
    private Animator myAnimator;

    //Playerを移動させるコンポーネントを入れる
    private Rigidbody myRigidbody;

    //汎用タイマー
    private float timeCounter = 0f;

    //無敵時間タイマー
    private float invincibletimeCounter = 0f;

    //前方向の速度
    private float velocityZ = 10f;

    //横方向の速度
    private float velocityX = 10f;

    //リロード中フラグ
    private bool isReload = false;

    //プレイヤーの体力
    public int life = 5;



    // Start is called before the first frame update
    void Start()
    {
        //Animatorコンポーネントを取得
        this.myAnimator = GetComponent<Animator>();

        //Rigidbodyコンポーネントを取得
        this.myRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //横方向の入力による速度
        float inputVelocityX = 0;
        //上方向の入力による速度
        float inputVelocityY = 0;
        float inputVelocityZ = 0;
        timeCounter += Time.deltaTime;
        invincibletimeCounter += Time.deltaTime;

        if (Input.GetKey(KeyCode.Mouse0) && isReload == false)
        {
            this.myAnimator.SetTrigger("Attack");
        }

        if (Input.GetKeyDown(KeyCode.R) && isReload == false)
        {
            this.myAnimator.SetTrigger("Reload");
            //タイマーリセット
            timeCounter = 0f;

            //リロード中
            isReload = true;
        }
        if (timeCounter > 3f)
        {
            //リロード中の解除
            isReload = false;
        }
        
        //プレイヤーをWASDに応じて前後左右に移動させる
        //前方向
        if (Input.GetKey(KeyCode.W))
        {
            //前方向への速度を代入
            inputVelocityZ = this.velocityZ;
            //前移動のアニメーション
            this.myAnimator.SetInteger("Type", 1);
            this.myAnimator.SetFloat("Speed", 2);
            if (Input.GetKey(KeyCode.LeftShift))
            {
                this.myAnimator.SetFloat("Speed", 6);
                inputVelocityZ = this.velocityZ + 10;
            }
            
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            // Wを離したら
            this.myAnimator.SetFloat("Speed", 0);
        }

        //左方向
        if (Input.GetKey(KeyCode.A))
        {
            //左方向への速度を代入
            inputVelocityX = -this.velocityX;
            //左移動のアニメーション
            this.myAnimator.SetInteger("Type" , 2);
            this.myAnimator.SetFloat("Speed", 2);
            
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            // Aを離したら
            this.myAnimator.SetFloat("Speed", 0);
        }

        //後ろ方向
        if (Input.GetKey(KeyCode.S))
        {
            //後ろ方向への速度を代入
            inputVelocityZ = -this.velocityZ;
            //後ろ移動のアニメーション
            this.myAnimator.SetInteger("Type", 3);
            this.myAnimator.SetFloat("Speed", 2);
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            // Sを離したら
            this.myAnimator.SetFloat("Speed", 0);
        }

        //右方向
        if (Input.GetKey(KeyCode.D))
        {
            //右方向への速度を代入
            inputVelocityX = this.velocityX;
            //右移動のアニメーション
            this.myAnimator.SetInteger("Type", 4);
            this.myAnimator.SetFloat("Speed", 2);
           
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            // Dを離したら
            this.myAnimator.SetFloat("Speed", 0);
        }

        //Playerに速度を与える ※Yはthis.myRigidbody.velocityをスルーする
        this.myRigidbody.velocity = new Vector3(inputVelocityX, this.myRigidbody.velocity.y, inputVelocityZ);

        
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "EnemyTag" && invincibletimeCounter > 2f)
        {
            //Debug.Log("life ="+life);
            life--;
            invincibletimeCounter = 0f;
            //GetComponent<CapsuleCollider>().isTrigger = false;

            if (life <= 0)
            {
                //衝突判定と物理演算を止める
                GetComponent<CapsuleCollider>().isTrigger = true;
                GetComponent<Rigidbody>().isKinematic = true;
                //死亡
                this.myAnimator.SetTrigger("Die");
                //GetComponent<AudioSource>().PlayOneShot(seDie);

                //消えるまでのウエイト
                //timeCounter = 0f;

            }
            else
            {
                //GetComponent<CapsuleCollider>().isTrigger = true;
                //Debug.Log("Hit");
                //GetComponent<AudioSource>().PlayOneShot(seDamage);

            }
        }
    }
}

