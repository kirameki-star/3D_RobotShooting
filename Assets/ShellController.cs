using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShellController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnemyTag")
        {
            //�ڐG�����R�C���̃I�u�W�F�N�g��j���i�ǉ��j
            Destroy(other.gameObject);
        }
    }

      
}
