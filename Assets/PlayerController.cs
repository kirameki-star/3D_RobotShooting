using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //�A�j���[�V�������邽�߂̃R���|�[�l���g������
    private Animator myAnimator;

    //Player���ړ�������R���|�[�l���g������
    private Rigidbody myRigidbody;

    //�ėp�^�C�}�[
    private float timeCounter = 0f;

    //���G���ԃ^�C�}�[
    private float invincibletimeCounter = 0f;

    //�O�����̑��x
    private float velocityZ = 10f;

    //�������̑��x
    private float velocityX = 10f;

    //�����[�h���t���O
    private bool isReload = false;

    //�v���C���[�̗̑�
    public int life = 5;



    // Start is called before the first frame update
    void Start()
    {
        //Animator�R���|�[�l���g���擾
        this.myAnimator = GetComponent<Animator>();

        //Rigidbody�R���|�[�l���g���擾
        this.myRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //�������̓��͂ɂ�鑬�x
        float inputVelocityX = 0;
        //������̓��͂ɂ�鑬�x
        float inputVelocityY = 0;
        float inputVelocityZ = 0;
        timeCounter += Time.deltaTime;
        invincibletimeCounter += Time.deltaTime;

        if (Input.GetKey(KeyCode.Mouse0) && isReload == false)
        {
            this.myAnimator.SetTrigger("Attack");
        }

        if (Input.GetKeyDown(KeyCode.R) && isReload == false)
        {
            this.myAnimator.SetTrigger("Reload");
            //�^�C�}�[���Z�b�g
            timeCounter = 0f;

            //�����[�h��
            isReload = true;
        }
        if (timeCounter > 3f)
        {
            //�����[�h���̉���
            isReload = false;
        }
        
        //�v���C���[��WASD�ɉ����đO�㍶�E�Ɉړ�������
        //�O����
        if (Input.GetKey(KeyCode.W))
        {
            //�O�����ւ̑��x����
            inputVelocityZ = this.velocityZ;
            //�O�ړ��̃A�j���[�V����
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
            // W�𗣂�����
            this.myAnimator.SetFloat("Speed", 0);
        }

        //������
        if (Input.GetKey(KeyCode.A))
        {
            //�������ւ̑��x����
            inputVelocityX = -this.velocityX;
            //���ړ��̃A�j���[�V����
            this.myAnimator.SetInteger("Type" , 2);
            this.myAnimator.SetFloat("Speed", 2);
            
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            // A�𗣂�����
            this.myAnimator.SetFloat("Speed", 0);
        }

        //������
        if (Input.GetKey(KeyCode.S))
        {
            //�������ւ̑��x����
            inputVelocityZ = -this.velocityZ;
            //���ړ��̃A�j���[�V����
            this.myAnimator.SetInteger("Type", 3);
            this.myAnimator.SetFloat("Speed", 2);
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            // S�𗣂�����
            this.myAnimator.SetFloat("Speed", 0);
        }

        //�E����
        if (Input.GetKey(KeyCode.D))
        {
            //�E�����ւ̑��x����
            inputVelocityX = this.velocityX;
            //�E�ړ��̃A�j���[�V����
            this.myAnimator.SetInteger("Type", 4);
            this.myAnimator.SetFloat("Speed", 2);
           
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            // D�𗣂�����
            this.myAnimator.SetFloat("Speed", 0);
        }

        //Player�ɑ��x��^���� ��Y��this.myRigidbody.velocity���X���[����
        this.myRigidbody.velocity = new Vector3(inputVelocityX, this.myRigidbody.velocity.y, inputVelocityZ);

        
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "EnemyTag" && invincibletimeCounter > 2f)
        {
            //Debug.Log("life ="+life);
            life--;
            invincibletimeCounter = 0f;
            //GetComponent<CapsuleCollider>().isTrigger = false;

            if (life <= 0)
            {
                //�Փ˔���ƕ������Z���~�߂�
                GetComponent<CapsuleCollider>().isTrigger = true;
                GetComponent<Rigidbody>().isKinematic = true;
                //���S
                this.myAnimator.SetTrigger("Die");
                //GetComponent<AudioSource>().PlayOneShot(seDie);

                //������܂ł̃E�G�C�g
                //timeCounter = 0f;

            }
            else
            {
                //GetComponent<CapsuleCollider>().isTrigger = true;
                //Debug.Log("Hit");
                //GetComponent<AudioSource>().PlayOneShot(seDamage);

            }
        }
    }
}

