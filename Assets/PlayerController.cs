using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //アニメーションするためのコンポーネントを入れる
    private Animator myAnimator;

    //Playerを移動させるコンポーネントを入れる
    private Rigidbody myRigidbody;

    //前方向の速度
    private float velocityZ = 10f;

    //横方向の速度
    private float velocityX = 10f;

    //上方向の速度
    private float velocityY = 10f;

    //左右の移動できる範囲
    private float movableRange = 3.4f;

    //動きを減速させる係数
    private float coefficient = 0.99f;



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

        //Playerに速度を与える
        this.myRigidbody.velocity = new Vector3(inputVelocityX, inputVelocityY, inputVelocityZ);

        //リロード
        if (Input.GetKey(KeyCode.R))
        {
            //Rを押したらリロードする
            this.myAnimator.SetTrigger("Reload");
        }


            //ジャンプしていない時にスペースが押されたらジャンプする（追加）
            /*if ((Input.GetKeyDown(KeyCode.Space)) && this.transform.position.y < 0.5f)
            {
                //ジャンプアニメを再生
                this.myAnimator.SetBool("Jump", true);
                //上方向への速度を代入
                inputVelocityY = this.velocityY;
            }
            else
            {
                //現在のY軸の速度を代入
                inputVelocityY = this.myRigidbody.velocity.y;
            }

            //Jumpステートの場合はJumpにfalseをセットする
            if (this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
            {
                this.myAnimator.SetBool("Jump", false);
            }*/
                        
    }
}

