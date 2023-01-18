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
    private float turnForce = 500.0f;
    [SerializeField]
    //�W�����v���邽�߂̗�

    private float upForce = 5500.0f;
    //���E�̈ړ��ł���͈�
    public float movableRange = 3.0f;

    //����������������W���i�ǉ��j
    private float coefficient = 0.95f;

    //�Q�[���I���̔���i�ǉ��j
    private bool isEnd = false;

    //�W�����v���Ă���ꍇ
    private bool m_IsJump = false;


    [SerializeField]
    private int life = 5;

    //�@���C�t�Ǘ�
    [SerializeField]
    private List<GameObject> lifeObj;

    [SerializeField]
    private GameObject gameOverObj;

    private int m_score = 0;

    [SerializeField]
    private TextMeshProUGUI m_scoreText;

    [SerializeField]
    private GameObject mainCamera;

    [SerializeField]
    private GameObject winCamera;


    //AudioSource�^�̕ϐ�audios��錾���܂��B
    [SerializeField]
    private GameObject gameClearObj;



    //��Ȃ�������Ȗ����B
    int countDown;

    //�o�ߎ��ԁB
    float countDownElapsedTime;

    //�J�E���g�_�E���̒����B���̏ꍇ��3�b�B
    float countDownDuration = 3.0f;


    [SerializeField]
    private TextMeshProUGUI countDownText;

    //�ȉ��͐錾��
    const float LOAD_WIDTH = 6f;
    const float MOVE_MAX = 3.5f;
    Vector3 previousPos, currentPos;

    bool isStart = false;

    float posDiff = 0.5f; //x���W�̍��̂����l�B

    // Use this for initialization
    void Start()
    {

        //����A�j���[�V�������J�n

        gameOverObj.active = false;
        m_score = 0;
        mainCamera.active = true;
        winCamera.active = false;
        //�z��ϐ�clip�̃C���f�b�N�X���O��Audio�̃t�@�C�����Đ����܂��B

        AudioManager.instance.PlayBgm(1);

        StartCoroutine("CountDown");
        isStart = false;

    }
    void Update()
    {
        // �X�^�[�g�����Ă��Ȃ����͏��������Ȃ�
		if (!isStart)
		{
            return;
		}
        //�Q�[���I���Ȃ�Unity�����̓�������������i�ǉ��j
        if (this.isEnd)
        {
            this.forwardForce *= this.coefficient;
            this.turnForce *= this.coefficient;
            this.upForce *= this.coefficient;

            //this.m_Animator.speed *= this.coefficient;
            

            return;
        }

        //�_���[�W���󂯂Ă��鎞�͏��������s���Ȃ�
		if (this.m_Animator.GetBool("Damage"))
		{
            return;
		}
        if ((Input.GetKeyDown(KeyCode.LeftArrow)) && -this.movableRange < this.transform.position.x)
        {
            //���Ɉړ��i�ǉ��j
            this.m_Rigidbody.AddForce(-this.turnForce, 0, 0);
        }
        else if ((Input.GetKey(KeyCode.RightArrow)) && this.transform.position.x < this.movableRange)
        {
            //�E�Ɉړ��i�ǉ��j
            this.m_Rigidbody.AddForce(this.turnForce, 0, 0);
        }

        // �X���C�v�ɂ��ړ�����
        if (Input.GetMouseButtonDown(0))
        {
            previousPos = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            // �X���C�v�ɂ��ړ��������擾
            currentPos = Input.mousePosition;
            float diffDistance = (currentPos.x - previousPos.x) / Screen.width * LOAD_WIDTH;

            // ���̃��[�J��x���W��ݒ� �����̊O�ɂłȂ��悤��
            float newX = Mathf.Clamp(transform.localPosition.x + diffDistance, -MOVE_MAX, MOVE_MAX);
            //transform.localPosition = new Vector3(newX, 0, this.transform.localPosition.z);

            // �^�b�v�ʒu���X�V
            previousPos = currentPos;
        }



        //Jump�X�e�[�g�̏ꍇ��Jump��false���Z�b�g����
        /*
        if (this.m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            this.m_Animator.SetBool("Jump", false);
        }*/


        //�W�����v���Ă��Ȃ����ɃX�y�[�X�������ꂽ��W�����v����
        //�W�����v�̔��f�
        
        if ( ( Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonUp(0) ) && this.transform.position.y < 0.5f)
        {
            //�W�����v�A�j�����Đ�
            this.m_Animator.SetBool("Jump", true);
            m_IsJump = true;
            //Unity�����ɏ�����̗͂�������
            this.m_Rigidbody.AddForce(this.transform.up * this.upForce);

            AudioManager.instance.PlaySe(0);
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
        if ( m_IsJump && collision.gameObject.tag == "Ground")
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
                ES3.Save<int>("Coin", this.m_score);
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
            //���݂̃R�C�����i�[����
            ES3.Save<int>("Coin", this.m_score);
            this.m_Animator.SetBool("Win", true);

            mainCamera.active = false;
            winCamera.active = true;
            //�z��ϐ�clip�̃C���f�b�N�X���O��Audio�̃t�@�C�����Đ����܂��B
            //audios.clip = clips[1];
            //audios.Play();
            AudioManager.instance.PlayBgm(1);

            gameClearObj.active = true;

        }
    }



    // https://www.zkn0hr.com/unity-radial-slider-count-down/
    // https://unity-mantario.hatenablog.com/entry/2018/07/15/205727

    IEnumerator CountDown()
    {
        countDown = 0;
        countDownElapsedTime = 0;


        //�e�L�X�g�̍X�V�B
        countDownText.text = System.String.Format("{0}", Mathf.FloorToInt(countDownDuration));

        //�����A���דI�ɂ�GameObject�ւ̎Q�Ƃ͕ʂɕێ����Ă��������X�������Ǝv���������B
        //countDownImage.gameObject.SetActive(true);
        countDownText.gameObject.SetActive(true);


        while (true)
        {
            countDownElapsedTime += Time.deltaTime;

            //�~�`�X���C�_�[�̍X�V�BfillAmount��0�`1.0f�̊ԂŎw�肷��B�o�ߎ��Ԃ̏����_�ȉ��̒l�����Ă���B
            //countDownImage.fillAmount = countDownElapsedTime % 1.0f;

            if (countDown < Mathf.FloorToInt(countDownElapsedTime))
            {
                //1�b���݂ŃJ�E���g�B
                countDown++;
                //�e�L�X�g�̍X�V�B
                countDownText.text = System.String.Format("{0}", Mathf.FloorToInt(countDownDuration - countDown));
            }

            if (countDownDuration <= countDownElapsedTime)
            {
                //�J�E���g�_�E���I���B

                //countDownImage.gameObject.SetActive(false);
                countDownText.gameObject.SetActive(false);
                // 
                this.m_Animator.SetBool("Run", true);
                isStart = true;
                yield break;
            }

            yield return null;
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
