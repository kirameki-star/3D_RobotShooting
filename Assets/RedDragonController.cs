using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedDragonController : MonoBehaviour
{
    public int hitPoint = 100;  //HP
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hitPoint <= 0)
        {
            Destroy(gameObject);
        }
    }
    //ダメージを受け取ってHPを減らす関数
    public void Damage(int damage)
    {
        //受け取ったダメージ分HPを減らす
        hitPoint -= damage;
    }
}
