using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //�A�j���[�V�������邽�߂̃R���|�[�l���g������
    [SerializeField]
    private Animator m_Animator;

    //Unity�������ړ�������R���|�[�l���g������
    [SerializeField]
    private Rigidbody m_Rigidbody;
    //�O�i���邽�߂̗�
    private float forwardForce = 800.0f;
    //���E�Ɉړ����邽�߂̗�
    private float turnForce = 1500.0f;
    [SerializeField]
    //�W�����v���邽�߂̗�
    private float upForce = 500.0f;
    //���E�̈ړ��ł���͈�
    private float movableRange = 2.0f;
    //����������������W���i�ǉ��j
    private float coefficient = 0.95f;

    //�Q�[���I���̔���i�ǉ��j
    private bool isEnd = false;

    //�W�����v���Ă���ꍇ
    private bool m_IsJump = false;

    [SerializeField]
    private int life = 5;

    [SerializeField]
    private List<GameObject> lifeObj;

    [SerializeField]
    private GameObject gameOverObj;

    private int m_score = 0;

    [SerializeField]
    private TextMeshProUGUI m_scoreText;


    // Use this for initialization
    void Start()
    {
        //����A�j���[�V�������J�n
        gameOverObj.active = false;
        m_score = 0;
    }
    void Update()
    {
        //�Q�[���I���Ȃ�Unity�����̓�������������i�ǉ��j
        if (this.isEnd)
        {
            this.forwardForce *= this.coefficient;
            this.turnForce *= this.coefficient;
            this.upForce *= this.coefficient;
            this.m_Animator.speed *= this.coefficient;
            return;
        }

        //Unity�����ɑO�����̗͂�������
        //this.m_Rigidbody.AddForce(this.transform.forward * this.forwardForce);

        //Unity��������L�[�܂��̓{�^���ɉ����č��E�Ɉړ�������i�ǉ��j
        if ((Input.GetKeyDown(KeyCode.LeftArrow)) && -this.movableRange < this.transform.position.x)
        {
            //���Ɉړ��i�ǉ��j
            this.m_Rigidbody.AddForce(-this.turnForce, 0, 0);
        }
        else if ((Input.GetKeyDown(KeyCode.RightArrow)) && this.transform.position.x < this.movableRange)
        {
            //�E�Ɉړ��i�ǉ��j
            this.m_Rigidbody.AddForce(this.turnForce, 0, 0);
        }
        //Jump�X�e�[�g�̏ꍇ��Jump��false���Z�b�g����
        /*
        if (this.m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            this.m_Animator.SetBool("Jump", false);
        }*/

        //�W�����v���Ă��Ȃ����ɃX�y�[�X�������ꂽ��W�����v����
        if (Input.GetKeyDown(KeyCode.Space) && this.transform.position.y < 0.5f)
        {
            //�W�����v�A�j�����Đ�
            this.m_Animator.SetBool("Jump", true);
            m_IsJump = true;
            //Unity�����ɏ�����̗͂�������
            this.m_Rigidbody.AddForce(this.transform.up * this.upForce);
        }
        if (!m_IsJump  && !this.m_Animator.GetBool("Damage"))
		{
            Vector3 force = new Vector3(0.0f, 0.0f, 10.0f);    // �͂�ݒ�
            this.m_Rigidbody.velocity = new Vector3(0, 0, 10);
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        //��Q���ɏՓ˂����ꍇ�i�ǉ��j
        if ( m_IsJump  && collision.gameObject.tag == "Ground")
        {
            this.m_Animator.SetBool("Jump", false);
            m_IsJump = false;
        }
    }

    //�g���K�[���[�h�ő��̃I�u�W�F�N�g�ƐڐG�����ꍇ�̏����i�ǉ��j
    void OnTriggerEnter(Collider other)
    {

        //��Q���ɏՓ˂����ꍇ�i�ǉ��j
        if (other.gameObject.tag == "CarTag" || other.gameObject.tag == "TrafficConeTag")
        {
            // ���C�t�����炷
            life--;
            
            //lifeObj.Remove(lifeObj[0].gameObject);
            if (life <= 0)
			{
                this.m_Animator.SetBool("Down", true);
                this.isEnd = true;
                gameOverObj.active = true;

                // ���C�t�̃n�[�g���\���ɂ���
                lifeObj[life].active = false;
            }
			else
			{
                lifeObj[life].active = false;
                // �R���[�`���̋N��
                StartCoroutine(DelayDamageAnimation());
            }
            //�ڐG�����R�C���̃I�u�W�F�N�g��j��
            Destroy(other.gameObject);
        }
        //��Q���ɏՓ˂����ꍇ�i�ǉ��j
        if (other.gameObject.tag == "Coin")
        {
            //this.isEnd = true;
            //�_���[�W����
            //this.m_Animator.SetBool("Damage", true);
            m_score++;
            m_scoreText.text = "Score" + m_score;
            //�ڐG�����R�C���̃I�u�W�F�N�g��j��
            Destroy(other.gameObject);
        }

        //�S�[���n�_�ɓ��B�����ꍇ�i�ǉ��j
        if (other.gameObject.tag == "GoalTag")
        {
            this.isEnd = true;
            
        }
    }


    // �R���[�`���{��
    private IEnumerator DelayDamageAnimation()
    {
        //�_���[�W����
        this.m_Animator.SetBool("Damage", true);
        
        // ���ɉ�����
        this.m_Rigidbody.velocity = new Vector3(0, 0, -1);
        
        yield return new WaitForSeconds(0.5f);
        this.m_Animator.SetBool("Damage", false);
    }

}
