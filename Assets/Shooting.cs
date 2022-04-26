using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float shotSpeed;
    public int shotCount = 30;
    private float shotInterval;
 
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            //�A�˗�
            shotInterval += 1;
            
            if (shotInterval % 15 == 0 && shotCount > 0)
            {
                shotCount -= 1;
 
                GameObject bullet = (GameObject)Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.parent.eulerAngles.x, transform.parent.eulerAngles.y, 0));
                Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
                bulletRb.AddForce(transform.forward * shotSpeed);
 
                //�ˌ�����Ă���3�b��ɏe�e�̃I�u�W�F�N�g��j�󂷂�.
 
                Destroy(bullet, 3.0f);
            }
 
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            shotCount = 30;
            
        }
 
    }
}
