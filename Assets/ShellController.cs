using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShellController : MonoBehaviour
{
    //Shell�I�u�W�F�N�g
    //private GameObject shell;
    // Start is called before the first frame update
    void Start()
    {
        //Unity�����̃I�u�W�F�N�g���擾
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
            //�ڐG�����e�e�̃I�u�W�F�N�g��j��
            Destroy(gameObject);
        }
    }

      
}
