using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    //プレイヤーのオブジェクト
    private GameObject PBRCharacter;
    //プレイヤーとカメラの距離
    private float difference;

    // Use this for initialization
    void Start()
    {
        //プレイヤーのオブジェクトを取得
        this.PBRCharacter = GameObject.Find("PBRCharacter");
        
    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤーの位置に合わせてカメラの位置を移動
        //this.transform.position = new Vector3();
    }
}
