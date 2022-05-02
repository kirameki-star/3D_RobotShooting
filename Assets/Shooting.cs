using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float shotSpeed;
    public int shotCount = 30;
    public Animator myAnimator;
    private float shotInterval;
    //汎用タイマー
    private float timeCounter = 0f;
    //リロード中
    private bool isReload = false;
    public PlayerController playerController;

    void Start()
    {
        //gameObject.GetComponent<Animator>();
        //Animatorコンポーネントを取得
        //GameObject playerController = GameObject.Find("Player");
        //playerController = GetComponent<Animator>;

    }
    void Update()
    {
        //左クリックで射撃、リロード中は射撃不可
        if (Input.GetKey(KeyCode.Mouse0) && isReload == false)
        {
            //連射力
            shotInterval += 1;


            
            if (shotInterval % 15 == 0 && shotCount > 0)
            {
                shotCount -= 1;

                //弾丸用のprefab生成
                GameObject bullet = (GameObject)Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.parent.eulerAngles.x, transform.parent.eulerAngles.y, 0));
                Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
                bulletRb.AddForce(transform.forward * shotSpeed);
 
                //射撃されてから3秒後に銃弾のオブジェクトを破壊する.
 
                Destroy(bullet, 3.0f);
            }
 
        }
        timeCounter += Time.deltaTime;

        //Rを押したらリロードする
        if (Input.GetKeyDown(KeyCode.R) && isReload == false)
        {
            //弾を30発装填
            shotCount = 30;
                        
            //タイマーリセット
            timeCounter = 0f;

            //リロード中
            isReload = true;

        }
        //3秒後にisReloadをfalseに
        if (timeCounter > 3f)
        {
            //リロード中の解除
            isReload = false;
        }

    }
}
