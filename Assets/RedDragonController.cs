using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RedDragonController : MonoBehaviour
{
	//�U������p�I�u�W�F�N�g
	//public GameObject EnemyAttackPrefab;

	private int StateNumber = 0;

	//�ėp�^�C�}�[
    private float timeCounter = 0f;

	//�U���p�^�C�}�[
	private float attacktimeCounter = 0f;

    //�v���C���[�̃I�u�W�F�N�g
    public GameObject player;

	public GameObject enemyAttackPrefab;

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

	//���S�t���O
	private bool isDie = false;

	private Collider attack;

	//�G��HP
	public int life = 10;  //HP

	//NavMeshAgent�^��ϐ�agent�Ő錾
	NavMeshAgent agent;

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

	private void Moving()
	{
		//�����Ă�������𓾂�i�I�C���[�j
		float direction = this.transform.rotation.eulerAngles.y;

	}


	// Start is called before the first frame update
	void Start()
    {
		//GetComponent��NavMeshAgent���擾����
		//�ϐ�agent�Ő錾���܂��B
		agent = GetComponent<NavMeshAgent>();

		//�����ʒu�̋L�^
		StartPosition = this.transform.position;

		//Player�̃I�u�W�F�N�g���擾
		this.player = GameObject.Find("Player");

		//�U������p�I�u�W�F�N�g���擾
		this.enemyAttackPrefab = GameObject.Find("EnemyAttackPrefab");

		//�A�j���[�^�R���|�[�l���g���擾
		this.myAnimator = GetComponent<Animator>();

		//Rigidbody�R���|�[�l���g���擾
		this.myRigidbody = GetComponent<Rigidbody>();

		//�U������p�I�u�W�F�N�g��BoxCollider�R���|�[�l���g���擾
		attack = enemyAttackPrefab.GetComponent<BoxCollider>();

	}
	

	// Update is called once per frame
	void Update()
	{
		//�v���C���[�̕���
		float direction = GetDirection(this.transform.position.x, this.transform.position.z, player.transform.position.x, player.transform.position.z);
		//�v���C���[�̋���
		float length = GetLength(this.transform.position.x, this.transform.position.z, player.transform.position.x, player.transform.position.z);

		//Debug.Log("����"+length);
		//Debug.Log("����" + direction);
		
		//�v���C���[�ɋ߂Â�����U��
		if (length < 7.0f)
        {
			//this.myAnimator.SetTrigger("Attack");
			this.myAnimator.SetInteger("Attack", 1);
			attacktimeCounter = 0f;

		    //�U�����̂ݍU������p�I�u�W�F�N�g�̏Փ˔�����I���ɂ���
			attack.isTrigger = false;

		}


		attacktimeCounter += Time.deltaTime;

		//�U���̌�ɍU������p�I�u�W�F�N�g�̏Փ˔�����I�t�ɂ���
		if (attacktimeCounter > 1.5f�@&& attack.isTrigger == false)
        {
			attack.isTrigger = true;
			this.myAnimator.SetInteger("Attack", 0);

		}
		//���S�t���O��false�̂Ƃ�
		if (isDie == false)
        {
			//Nav Mesh Agent���ݒ肳�ꂽObject��PlayerGameObject�̌��ǂ�������
			agent.destination = player.transform.position;
			this.myAnimator.SetFloat("Speed", 2);

			//Debug.Log("" + target.transform.position);
		}
			
		//�G�̗̑͂�0�ȉ��Ŏ��S�t���O��false�̂Ƃ�
		if (life <= 0 && isDie == false)
		{
			this.myAnimator.SetTrigger("Die");
			timeCounter = 0f;
			
			//���S�t���O��true�ɂ���
			isDie = true;
			//Nav Mesh Agent�Ɏ��g�̌��ݍ��W���擾���������Ȃ��悤�ɂ���
			agent.destination = this.transform.position;
		}
		
		//���S���Ă���4�b��ɃI�u�W�F�N�g�j��
		if(isDie == true)
        {
			timeCounter += Time.deltaTime;

			if (timeCounter > 4f)
			{
				Destroy(this.gameObject);
			}
		}

	}
	void OnTriggerEnter(Collider other)
    {
		//ShellTag�i�e�e�j�ɓ���������̗͂����炷
		if (other.gameObject.tag == "ShellTag")
        {
			life--;
        }

	}
	
}
