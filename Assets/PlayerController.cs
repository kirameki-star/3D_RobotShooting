using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //�A�j���[�V�������邽�߂̃R���|�[�l���g������
    private Animator myAnimator;

    //Unity�������ړ�������R���|�[�l���g������i�ǉ��j
    private Rigidbody myRigidbody;

    //�O�����̑��x
    private float velocityZ = 10f;

    //�������̑��x
    private float velocityX = 10f;

    //������̑��x
    private float velocityY = 10f;

    //���E�̈ړ��ł���͈�
    private float movableRange = 3.4f;

    //����������������W��
    private float coefficient = 0.99f;
    // Start is called before the first frame update
    void Start()
    {
        //Animator�R���|�[�l���g���擾
        this.myAnimator = GetComponent<Animator>();

        //����A�j���[�V�������J�n
        this.myAnimator.SetFloat("Speed", 1);

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

        //Unity��������L�[�܂��̓{�^���ɉ����č��E�Ɉړ�������
        if (Input.GetKey(KeyCode.A))
        {
            //�������ւ̑��x����
            inputVelocityX = -this.velocityX;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            //�E�����ւ̑��x����
            inputVelocityX = this.velocityX;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            //�O�����ւ̑��x����
            inputVelocityZ = this.velocityZ;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            //�O�����ւ̑��x����
            inputVelocityZ = -this.velocityZ;
        }

            //�W�����v���Ă��Ȃ����ɃX�y�[�X�������ꂽ��W�����v����i�ǉ��j
            if ((Input.GetKeyDown(KeyCode.Space)) && this.transform.position.y < 0.5f)
        {
            //�W�����v�A�j�����Đ�
            this.myAnimator.SetBool("Jump", true);
            //������ւ̑��x����
            inputVelocityY = this.velocityY;
        }
        else
        {
            //���݂�Y���̑��x����
            inputVelocityY = this.myRigidbody.velocity.y;
        }

        //Jump�X�e�[�g�̏ꍇ��Jump��false���Z�b�g����
        if (this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            this.myAnimator.SetBool("Jump", false);
        }

        //Unity�����ɑ��x��^����i�ύX�j
        this.myRigidbody.velocity = new Vector3(inputVelocityX, inputVelocityY, inputVelocityZ);
    }
}

