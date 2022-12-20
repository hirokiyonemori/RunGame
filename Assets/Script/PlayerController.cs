using System.Collections;
using System.Collections.Generic;
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
    private float turnForce = 500.0f;
    [SerializeField]
    //�W�����v���邽�߂̗�
    private float upForce = 100.0f;
    //���E�̈ړ��ł���͈�
    private float movableRange = 1.0f;
    //����������������W���i�ǉ��j
    private float coefficient = 0.95f;

    //�Q�[���I���̔���i�ǉ��j
    private bool isEnd = false;

    //�W�����v���Ă���ꍇ
    private bool m_IsJump = false;

    // Use this for initialization
    void Start()
    {

        //����A�j���[�V�������J�n
        //this.m_Animator.SetFloat("Speed", 1);
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
        }

        //Unity�����ɑO�����̗͂�������
        //this.m_Rigidbody.AddForce(this.transform.forward * this.forwardForce);

        //Unity��������L�[�܂��̓{�^���ɉ����č��E�Ɉړ�������i�ǉ��j
        if ((Input.GetKey(KeyCode.LeftArrow)) && -this.movableRange < this.transform.position.x)
        {
            //���Ɉړ��i�ǉ��j
            this.m_Rigidbody.AddForce(-this.turnForce, 0, 0);
        }
        else if ((Input.GetKey(KeyCode.RightArrow)) && this.transform.position.x < this.movableRange)
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
    }

    private void OnCollisionEnter(Collision collision)
    {
        //��Q���ɏՓ˂����ꍇ�i�ǉ��j
        if ( m_IsJump  && collision.gameObject.tag == "Ground")
        {
            Debug.Log("Jump");
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
            this.isEnd = true;
            //�ڐG�����R�C���̃I�u�W�F�N�g��j��
            Destroy(other.gameObject);
        }
        
        //�S�[���n�_�ɓ��B�����ꍇ�i�ǉ��j
        if (other.gameObject.tag == "GoalTag")
        {
            this.isEnd = true;
            
        }
    }
}
