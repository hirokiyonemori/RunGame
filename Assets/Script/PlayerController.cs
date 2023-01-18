using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //アニメーションするためのコンポーネントを入れる
    [SerializeField]
    private Animator m_Animator;

    //Unityちゃんを移動させるコンポーネントを入れる
    [SerializeField]
    private Rigidbody m_Rigidbody;
    //前進するための力
    private float forwardForce = 800.0f;
    //左右に移動するための力
    private float turnForce = 500.0f;
    [SerializeField]
    //ジャンプするための力

    private float upForce = 5500.0f;
    //左右の移動できる範囲
    public float movableRange = 3.0f;

    //動きを減速させる係数（追加）
    private float coefficient = 0.95f;

    //ゲーム終了の判定（追加）
    private bool isEnd = false;

    //ジャンプしている場合
    private bool m_IsJump = false;


    [SerializeField]
    private int life = 5;

    //　ライフ管理
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


    //AudioSource型の変数audiosを宣言します。
    [SerializeField]
    private GameObject gameClearObj;



    //我ながら微妙な命名。
    int countDown;

    //経過時間。
    float countDownElapsedTime;

    //カウントダウンの長さ。この場合は3秒。
    float countDownDuration = 3.0f;


    [SerializeField]
    private TextMeshProUGUI countDownText;

    //以下は宣言文
    const float LOAD_WIDTH = 6f;
    const float MOVE_MAX = 3.5f;
    Vector3 previousPos, currentPos;

    bool isStart = false;

    float posDiff = 0.5f; //x座標の差のいき値。

    // Use this for initialization
    void Start()
    {

        //走るアニメーションを開始

        gameOverObj.active = false;
        m_score = 0;
        mainCamera.active = true;
        winCamera.active = false;
        //配列変数clipのインデックスが０のAudioのファイルを再生します。

        AudioManager.instance.PlayBgm(1);

        StartCoroutine("CountDown");
        isStart = false;

    }
    void Update()
    {
        // スタートをしていない時は処理をしない
		if (!isStart)
		{
            return;
		}
        //ゲーム終了ならUnityちゃんの動きを減衰する（追加）
        if (this.isEnd)
        {
            this.forwardForce *= this.coefficient;
            this.turnForce *= this.coefficient;
            this.upForce *= this.coefficient;

            //this.m_Animator.speed *= this.coefficient;
            

            return;
        }

        //ダメージを受けている時は処理を実行しない
		if (this.m_Animator.GetBool("Damage"))
		{
            return;
		}
        if ((Input.GetKeyDown(KeyCode.LeftArrow)) && -this.movableRange < this.transform.position.x)
        {
            //左に移動（追加）
            this.m_Rigidbody.AddForce(-this.turnForce, 0, 0);
        }
        else if ((Input.GetKey(KeyCode.RightArrow)) && this.transform.position.x < this.movableRange)
        {
            //右に移動（追加）
            this.m_Rigidbody.AddForce(this.turnForce, 0, 0);
        }

        // スワイプによる移動処理
        if (Input.GetMouseButtonDown(0))
        {
            previousPos = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            // スワイプによる移動距離を取得
            currentPos = Input.mousePosition;
            float diffDistance = (currentPos.x - previousPos.x) / Screen.width * LOAD_WIDTH;

            // 次のローカルx座標を設定 ※道の外にでないように
            float newX = Mathf.Clamp(transform.localPosition.x + diffDistance, -MOVE_MAX, MOVE_MAX);
            //transform.localPosition = new Vector3(newX, 0, this.transform.localPosition.z);

            // タップ位置を更新
            previousPos = currentPos;
        }



        //Jumpステートの場合はJumpにfalseをセットする
        /*
        if (this.m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            this.m_Animator.SetBool("Jump", false);
        }*/


        //ジャンプしていない時にスペースが押されたらジャンプする
        //ジャンプの判断基準
        
        if ( ( Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonUp(0) ) && this.transform.position.y < 0.5f)
        {
            //ジャンプアニメを再生
            this.m_Animator.SetBool("Jump", true);
            m_IsJump = true;
            //Unityちゃんに上方向の力を加える
            this.m_Rigidbody.AddForce(this.transform.up * this.upForce);

            AudioManager.instance.PlaySe(0);
        }


        if (!m_IsJump  && !this.m_Animator.GetBool("Damage"))
		{
            Vector3 force = new Vector3(0.0f, 0.0f, 10.0f);    // 力を設定
            this.m_Rigidbody.velocity = new Vector3(0, 0, 10);
        }
        

    }

    private void OnCollisionEnter(Collision collision)
    {
        //障害物に衝突した場合（追加）
        if ( m_IsJump && collision.gameObject.tag == "Ground")
        {
            Debug.Log("Jump");
            this.m_Animator.SetBool("Jump", false);
            
            m_IsJump = false;
        }
    }

    //トリガーモードで他のオブジェクトと接触した場合の処理（追加）
    void OnTriggerEnter(Collider other)
    {

        //障害物に衝突した場合（追加）
        if (other.gameObject.tag == "CarTag" || other.gameObject.tag == "TrafficConeTag")
        {
            // ライフを減らす
            life--;
            
            //lifeObj.Remove(lifeObj[0].gameObject);
            if (life <= 0)
			{
                this.m_Animator.SetBool("Down", true);
                this.isEnd = true;
                gameOverObj.active = true;

                // ライフのハートを非表示にする
                lifeObj[life].active = false;
                ES3.Save<int>("Coin", this.m_score);
            }
			else
			{
                lifeObj[life].active = false;
                // コルーチンの起動
                StartCoroutine(DelayDamageAnimation());
            }
            //接触したコインのオブジェクトを破棄
            Destroy(other.gameObject);
        }
        //障害物に衝突した場合（追加）
        if (other.gameObject.tag == "Coin")
        {
            
            //ダメージ判定
            //this.m_Animator.SetBool("Damage", true);
            m_score++;
            m_scoreText.text = "Score" + m_score;

            //接触したコインのオブジェクトを破棄
            Destroy(other.gameObject);
        }
        
        //ゴール地点に到達した場合（追加）
        if (other.gameObject.tag == "GoalTag")
        {
            this.isEnd = true;
            //現在のコインを格納する
            ES3.Save<int>("Coin", this.m_score);
            this.m_Animator.SetBool("Win", true);

            mainCamera.active = false;
            winCamera.active = true;
            //配列変数clipのインデックスが０のAudioのファイルを再生します。
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


        //テキストの更新。
        countDownText.text = System.String.Format("{0}", Mathf.FloorToInt(countDownDuration));

        //多分、負荷的にはGameObjectへの参照は別に保持していた方が宜しいかと思うが割愛。
        //countDownImage.gameObject.SetActive(true);
        countDownText.gameObject.SetActive(true);


        while (true)
        {
            countDownElapsedTime += Time.deltaTime;

            //円形スライダーの更新。fillAmountは0～1.0fの間で指定する。経過時間の小数点以下の値を入れている。
            //countDownImage.fillAmount = countDownElapsedTime % 1.0f;

            if (countDown < Mathf.FloorToInt(countDownElapsedTime))
            {
                //1秒刻みでカウント。
                countDown++;
                //テキストの更新。
                countDownText.text = System.String.Format("{0}", Mathf.FloorToInt(countDownDuration - countDown));
            }

            if (countDownDuration <= countDownElapsedTime)
            {
                //カウントダウン終了。

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



    // コルーチン本体
    private IEnumerator DelayDamageAnimation()
    {
        //ダメージ判定
        this.m_Animator.SetBool("Damage", true);
        
        // 後ろに下がる
        this.m_Rigidbody.velocity = new Vector3(0, 0, -1);
        
        yield return new WaitForSeconds(0.5f);
        this.m_Animator.SetBool("Damage", false);
    }


}
