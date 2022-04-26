using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShellController : MonoBehaviour
{
    //Shellオブジェクト
    //private GameObject shell;
    // Start is called before the first frame update
    void Start()
    {
        //Unityちゃんのオブジェクトを取得
        //this.shell = GameObject.Find("Shell");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnemyTag")
        {
            //接触した銃弾のオブジェクトを破棄
            Destroy(gameObject);
        }
    }

      
}
