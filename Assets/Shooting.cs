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
    //�ėp�^�C�}�[
    private float timeCounter = 0f;
    //�����[�h��
    private bool isReload = false;
    public PlayerController playerController;

    void Start()
    {
        //gameObject.GetComponent<Animator>();
        //Animator�R���|�[�l���g���擾
        //GameObject playerController = GameObject.Find("Player");
        //playerController = GetComponent<Animator>;

    }
    void Update()
    {
        //���N���b�N�Ŏˌ��A�����[�h���͎ˌ��s��
        if (Input.GetKey(KeyCode.Mouse0) && isReload == false)
        {
            //�A�˗�
            shotInterval += 1;


            
            if (shotInterval % 15 == 0 && shotCount > 0)
            {
                shotCount -= 1;

                //�e�ۗp��prefab����
                GameObject bullet = (GameObject)Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.parent.eulerAngles.x, transform.parent.eulerAngles.y, 0));
                Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
                bulletRb.AddForce(transform.forward * shotSpeed);
 
                //�ˌ�����Ă���3�b��ɏe�e�̃I�u�W�F�N�g��j�󂷂�.
 
                Destroy(bullet, 3.0f);
            }
 
        }
        timeCounter += Time.deltaTime;

        //R���������烊���[�h����
        if (Input.GetKeyDown(KeyCode.R) && isReload == false)
        {
            //�e��30�����U
            shotCount = 30;
                        
            //�^�C�}�[���Z�b�g
            timeCounter = 0f;

            //�����[�h��
            isReload = true;

        }
        //3�b���isReload��false��
        if (timeCounter > 3f)
        {
            //�����[�h���̉���
            isReload = false;
        }

    }
}
