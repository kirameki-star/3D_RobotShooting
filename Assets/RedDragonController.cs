using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedDragonController : MonoBehaviour
{
    private int StateNumber = 0;

    private float TimeCounter = 0f;

    //�v���C���[�̃I�u�W�F�N�g
    private GameObject player;

    //�A�j���[�V�������邽�߂̃R���|�[�l���g������
    private Animator myAnimator;

	//�X�^�[�g�̈ʒu���L�^
	private Vector3 StartPosition;

	//�ړ�������R���|�[�l���g������
	private Rigidbody myRigidbody;

	//�ړ���
	private float velocity = 1.0f;

	//�n�ʂɐڐG
	private bool isGround = false;

	//�ǂɓ�������
	private bool isWall = false;

	//�_���[�W�L��
	private bool isDamage = false;

	//�_���[�W�L�����Z��
	private float DamageCancel = 0f;

	//���C�t
	private int Life = 3;

	//���S
	private bool isDie = false;

	//�G��HP
	public int hitPoint = 100;  //HP

	//--------------------------------------------------------------------------------
	// �p�x�N���b�v
	//--------------------------------------------------------------------------------

	private float ClipDirection360(float direction)
	{
		if (direction < 0.0f)
		{
			return 360.0f + direction;
		}
		if (direction >= 360.0f)
		{
			return direction - 360.0f;
		}
		return direction;
	}

	private float ClipDirection180(float direction)
	{
		direction = ClipDirection360(direction);
		direction = direction + 180.0f;
		if (direction >= 360.0f)
		{
			direction = direction - 360.0f;
		}
		return direction - 180.0f;
	}

	//--------------------------------------------------------------------------------
	// �����E�p�x
	//--------------------------------------------------------------------------------

	private float GetDirection(float x1, float y1, float x2, float y2)
	{
		if ((x2 - x1) == 0.0f && (y2 - y1) == 0.0f)
		{
			return 0.0f;
		}
		else
		{
			return ClipDirection360((((Mathf.PI / 2.0f) - Mathf.Atan2(y1 - y2, x1 - x2)) * (180.0f / Mathf.PI)) + 180.0f);
		}
	}

	private float GetLength(float x1, float y1, float x2, float y2)
	{
		return Mathf.Sqrt(((x1 - x2) * (x1 - x2)) + ((y1 - y2) * (y1 - y2)));
	}

	// Start is called before the first frame update
	void Start()
    {
		//�����ʒu�̋L�^
		StartPosition = this.transform.position;

		//Player�̃I�u�W�F�N�g���擾
		this.player = GameObject.Find("Player");

		//�A�j���[�^�R���|�[�l���g���擾
		this.myAnimator = GetComponent<Animator>();

		//Rigidbody�R���|�[�l���g���擾
		this.myRigidbody = GetComponent<Rigidbody>();

		//�����̑ҋ@�^�C�}�[�������_���ɂ���
		TimeCounter = Random.Range(-3f, 0f);
	}
	private void Moving()
	{
		//�����Ă�������𓾂�i�I�C���[�j
		float direction = this.transform.rotation.eulerAngles.y;

		//�����A�j���[�V�������J�n
		this.myAnimator.SetFloat("Speed", velocity);

		//���x��^����i�I�C���[�����W�A���j��Y����velocity�X���[
		this.myRigidbody.velocity = new Vector3(Mathf.Sin(direction * Mathf.Deg2Rad) * velocity, this.myRigidbody.velocity.y, Mathf.Cos(direction * Mathf.Deg2Rad) * velocity);
	}

	/*private void StateMachine()
	{
		//���j�e�B�����̕���
		float direction = GetDirection(this.transform.position.x, this.transform.position.z, player.transform.position.x, player.transform.position.z);
		//���j�e�B�����̋���
		float length = GetLength(this.transform.position.x, this.transform.position.z, player.transform.position.x, player.transform.position.z);

		//�X�e�[�g�U�蕪��
		switch (StateNumber)
		{
			//3�b�ҋ@
			case 0:
				{   //3�b�����Ȃ�
					if (TimeCounter > 3f)
					{
						//X,Z�Œ�
						//GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;

						//�N���A�[
						TimeCounter = 0f;

						//�v���C���[�̋�����10m�ȏ�Ȃ�y������Ɠ����z
						if (length > 10f)
						{
							//�����_���ŏ���������ς���
							this.transform.Rotate(new Vector3(0f, Random.Range(-30f, 30f), 0f));
							//�J�� ��������Ɠ����i���G�j
							StateNumber = 1;
						}
						else
						{
							//���E�͈̔͂ɓ����Ă���H
							if (Mathf.DeltaAngle(this.transform.rotation.eulerAngles.y, direction) > -60f && Mathf.DeltaAngle(this.transform.rotation.eulerAngles.y, direction) < 60f)
							{
								//��]
								this.transform.Rotate(new Vector3(0f, Mathf.DeltaAngle(this.transform.rotation.eulerAngles.y, direction), 0f));
								//�J�� ���v���C���[�ɋ߂Â�
								StateNumber = 2;
							}
							else
							{
								//�����_���ŏ���������ς���
								this.transform.Rotate(new Vector3(0f, Random.Range(-30f, 30f), 0f));
								//�J�� ��������Ɠ����i���G�j
								StateNumber = 1;
							}
						}
					}
				}
				break;

			//������Ɠ����i���G�j
			case 1:
				{   //�ړ�
					Moving();

					//3�b�o������3�b�ҋ@�ɖ߂�
					if (TimeCounter > 3f)
					{
						//�ŏ��̒n�_����̋��������߂�
						float lengthmax = GetLength(StartPosition.x, StartPosition.z, this.transform.position.x, this.transform.position.z);

						//7���[�g�����ꂽ
						if (lengthmax > 7f)
						{
							//�����_���Ŕ��Ε����֕ς���
							this.transform.Rotate(new Vector3(0f, Random.Range(150f, 210f), 0f));
						}

						//�N���A�[
						TimeCounter = 0f;
						//�����A�j���[�V�������~
						this.myAnimator.SetFloat("Speed", 0f);
						//�J�� ��3�b�ҋ@
						StateNumber = 0;
					}

					//�ǂɏՓ˂���
					if (isWall && isGround)
					{
						//�����_���Ŕ��Ε����֕ς��� 2021/12/27 �R�O�����甽�΂ɂ����B
						this.transform.Rotate(new Vector3(0f, Random.Range(150f, 210f), 0f));
						//�N���A�[
						TimeCounter = 0f;
						//�����A�j���[�V�������~
						this.myAnimator.SetFloat("Speed", 0f);
						//�W�����v�̃A�j���[�V����
						this.myAnimator.SetTrigger("Jump");
						//�W�����v
						StateNumber = 4;
					}
				}
				break;

			//�v���C���[�ɋ߂Â�
			case 2:
				{   //�ړ�
					Moving();

					//7�b�o������3�b�ҋ@�ɖ߂�
					if (TimeCounter > 7f)
					{
						//�N���A�[
						TimeCounter = 0f;
						//�����A�j���[�V�������~
						this.myAnimator.SetFloat("Speed", 0f);
						//�J�� ��3�b�ҋ@
						StateNumber = 0;
					}

					//�ǂɏՓ˂���
					if (isWall && isGround)
					{
						//�N���A�[
						TimeCounter = 0f;
						//�����A�j���[�V�������~
						this.myAnimator.SetFloat("Speed", 0f);
						//�W�����v�̃A�j���[�V����
						this.myAnimator.SetTrigger("Jump");
						//�W�����v
						StateNumber = 4;
					}

					//�U�����͂������H
					if (length < 1f)
					{
						//�N���A�[
						TimeCounter = 0f;
						//�����A�j���[�V�������~
						this.myAnimator.SetFloat("Speed", 0f);
						//�U���̃A�j���[�V����
						this.myAnimator.SetTrigger("Attack");
						//�J�� ���U��
						StateNumber = 3;
					}
				}
				break;

			//�U��
			case 3:
				{   //1�b�o�����H
					if (TimeCounter > 1f)
					{
						//�N���A�[
						TimeCounter = 0f;
						//�J�� ��3�b�ҋ@
						StateNumber = 0;
					}
				}
				break;

			//�W�����v
			case 4:
				{   //�ړ�
					Moving();

					//3�b�o������3�b�ҋ@�ɖ߂�
					if (TimeCounter > 3f)
					{
						//�N���A�[
						TimeCounter = 0f;
						//�J�� ��3�b�ҋ@
						StateNumber = 0;
					}
				}
				break;
			
			case 5:
                {
					if (TimeCounter > 1f)
					{
						Destroy(this.gameObject);
					}
				}
				break;
			default: break;
		}
	}*/

	// Update is called once per frame
	void Update()
	{
		if (hitPoint <= 0)
		{
			this.myAnimator.SetTrigger("Die");
			TimeCounter = 0f;
			StateNumber = 5;

		}
	}
    //�_���[�W���󂯎����HP�����炷�֐�
    public void Damage(int damage)
    {
        //�󂯎�����_���[�W��HP�����炷
        hitPoint -= damage;
    }

    void OnTriggerEnter(Collider other)
    {

        //�Ԃ������I�u�W�F�N�g��Tag��Shell�Ƃ������O�������Ă������Ȃ�΁i�����j.
        if (other.CompareTag("ShellTag"))
        {
            //HP�����炷
            hitPoint--;
        }
    }

}
