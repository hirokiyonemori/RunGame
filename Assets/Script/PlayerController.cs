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

    
    private bool tutorialFlag;

    [SerializeField]
    TutorialManager tutorialManager;



    // Use this for initialization
    void Start()
    {

        //����A�j���[�V�������J�n

        gameOverObj.active = false;
        m_score = 0;
        
        
        foreach (var obj in (lifeObj))
        {
            obj.SetActive((false));
        }

        life = (int)ES3.Load(SaveManager.LIFE);
        for (int i = 0; i < life; i ++ )
        {
            lifeObj[i].SetActive(true);
        }
        
        
        //�z��ϐ�clip�̃C���f�b�N�X���O��Audio�̃t�@�C�����Đ����܂��B

        AudioManager.instance.PlayBgm(1);

        
        isStart = false;
        tutorialFlag = ES3.Load<bool>("Tutorial");
		if (tutorialFlag)
		{
            StartCoroutine("CountDown");
		}
		else
		{
            this.m_Animator.SetBool("Run", true);
            countDownText.text = "";
        }

        StageNo stageNo = (StageNo) PlayerManager.instance.stageNo;
        int _stageNo = PlayerManager.instance.stageNo;
        string _name = SaveManager.STAGE_CLEAR;
            
        switch (stageNo)
        {
            // 初級
            case StageNo.elementary:
            // 中級
            case StageNo.intermediate:
                speed = 10;
                break;
            //　上級
            case StageNo.senior:
                speed = 10;
                break;
            //　達人
            default:
                speed = 10;
                break;
        }

        PlayerManager.instance.lastFlag = false;


    }
    private float speed = 0;

    private float lastSpeed = 10;

    

    void Update()
    {

		if (!tutorialFlag && tutorialManager.CheckTutorialEnd())
		{
            tutorialFlag = true;
            StartCoroutine("CountDown");
            ES3.Save<bool>("Tutorial", true);
        }

        // スタート
        if (!isStart && tutorialFlag)
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
            transform.localPosition = new Vector3(newX, this.transform.localPosition.y, this.transform.localPosition.z);

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
        
        //�A�ő΍�
        if ( ( Input.GetKeyDown(KeyCode.Space) 
               || Input.GetMouseButtonUp(0) ) 
             && this.transform.position.y < 0.5f && !this.isGround  )
        {
            //�W�����v�A�j�����Đ�
            this.m_Animator.SetBool("Jump", true);
            m_IsJump = true;
            //Unity�����ɏ�����̗͂�������
            
            this.m_Rigidbody.AddForce(new Vector3(0,upForce,0 ));
            
            AudioManager.instance.PlaySe(0);
            //Debug.Log(" transform.position.y  " + transform.position.y );
        }

        if (this.transform.position.y >=0.5f)
        {
            m_IsJump = true;
        }
        
        
        if (!tutorialFlag) return;
        // �ړ������͎��s���Ȃ�
        if (!m_IsJump  && !this.m_Animator.GetBool("Damage"))
        {
			if (PlayerManager.instance.lastFlag)
			{
                this.m_Rigidbody.velocity = new Vector3(0, 0, lastSpeed);
            }
			else
			{
                this.m_Rigidbody.velocity = new Vector3(0, 0, speed);
            }
            
        }
        

    }

    private bool isGround = false;
    private void OnCollisionExit(Collision collision)
    {
        //��Q���ɏՓ˂����ꍇ�i�ǉ��j
        if ( collision.gameObject.tag == "Ground")
        {
            this.isGround = false;
            Debug.Log("Ground");
            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //��Q���ɏՓ˂����ꍇ�i�ǉ��j
        if ( collision.gameObject.tag == "Ground")
        {
            
            if (m_IsJump)
            {
                Debug.Log("m_IsJump");
                this.m_Animator.SetBool("Jump", false);

                if (this.transform.position.y <= 0.1f)
                {
                    m_IsJump = false;
                    Debug.Log("m_IsJump2");
                }
            }
            this.isGround = true;
        }
    }

    //�g���K�[���[�h�ő��̃I�u�W�F�N�g�ƐڐG�����ꍇ�̏����i�ǉ��j
    void OnTriggerEnter(Collider other)
    {

        //��Q���ɏՓ˂����ꍇ�i�ǉ��j
        if (other.gameObject.tag == "CarTag" || other.gameObject.tag == "TrafficConeTag")
        {
            // plaerのライフを引く
            life--;
            //�Փ˂��鉹
            AudioManager.instance.PlaySe(1);
            //lifeObj.Remove(lifeObj[0].gameObject);
            if (life <= 0)
            {
                life = 0;
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
                StartCoroutine(DelayDamageAnimation());
            }
            //�ڐG�����R�C���̃I�u�W�F�N�g��j��
            Destroy(other.gameObject);
        }
        //��Q���ɏՓ˂����ꍇ�i�ǉ��j
        if (other.gameObject.tag == "Coin")
        {
            //�������擾���鉹
            AudioManager.instance.PlaySe(2);
            //�_���[�W����
            //this.m_Animator.SetBool("Damage", true);
            m_score++;
            m_scoreText.text = "" + m_score;

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

            if (PlayerManager.instance.stageNo == (int)StageNo.elementary)
            {
                
            }

            StageNo stageNo = (StageNo) PlayerManager.instance.stageNo;
            int _stageNo = PlayerManager.instance.stageNo;
            string _name = SaveManager.STAGE_CLEAR;
            
            switch (stageNo)
            {
                // 初級
                case StageNo.elementary:
                    SaveManager.instance.SaveBool( _name+ _stageNo,true);
                    break;
                // 中級
                case StageNo.intermediate:
                    if (m_score >= 5 )
                    {
                        SaveManager.instance.SaveBool( _name+ _stageNo,true);   
                    }
                    break;
                //　上級
                case StageNo.senior:
                    if (m_score >= 10 )
                    {
                        SaveManager.instance.SaveBool( _name+ _stageNo,true);   
                    }
                    break;
            }
            // ライフが現在のライフの更新
            int l =(int)ES3.Load(SaveManager.LIFE);
            //50 の場合はライフを５つに増やし
            // 20の場合は４つに増やす
            if (m_score >= 50)
            {
                if (l <= 4)
                {
                    ES3.Save(SaveManager.LIFE,5);    
                }
            }else if (m_score >= 20)
            {
                if (l <= 3)
                {
                    ES3.Save(SaveManager.LIFE, 4);
                }
            }

            //�z��ϐ�clip�̃C���f�b�N�X���O��Audio�̃t�@�C�����Đ����܂��B
            AudioManager.instance.PlayBgm(2);
            AudioManager.instance.loop(false);

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
        
        this.m_Animator.SetBool("Damage", true);
        
        // キャラクターを後ろに移動させる
        this.m_Rigidbody.velocity = new Vector3(0, 0, -1);
        
        yield return new WaitForSeconds(0.5f);
        this.m_Animator.SetBool("Damage", false);
    }


}
