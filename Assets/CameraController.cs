using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    //�v���C���[�̃I�u�W�F�N�g
    private GameObject PBRCharacter;
    //�v���C���[�ƃJ�����̋���
    private float difference;

    // Use this for initialization
    void Start()
    {
        //�v���C���[�̃I�u�W�F�N�g���擾
        this.PBRCharacter = GameObject.Find("PBRCharacter");
        
    }

    // Update is called once per frame
    void Update()
    {
        //�v���C���[�̈ʒu�ɍ��킹�ăJ�����̈ʒu���ړ�
        //this.transform.position = new Vector3();
    }
}
