using System.Collections;
using System.Collections.Generic;
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
    private float upForce = 100.0f;
    //左右の移動できる範囲
    private float movableRange = 1.0f;
    //動きを減速させる係数（追加）
    private float coefficient = 0.95f;

    //ゲーム終了の判定（追加）
    private bool isEnd = false;

    //ジャンプしている場合
    private bool m_IsJump = false;

    // Use this for initialization
    void Start()
    {

        //走るアニメーションを開始
        //this.m_Animator.SetFloat("Speed", 1);
    }
    void Update()
    {
        //ゲーム終了ならUnityちゃんの動きを減衰する（追加）
        if (this.isEnd)
        {
            this.forwardForce *= this.coefficient;
            this.turnForce *= this.coefficient;
            this.upForce *= this.coefficient;
            this.m_Animator.speed *= this.coefficient;
        }

        //Unityちゃんに前方向の力を加える
        //this.m_Rigidbody.AddForce(this.transform.forward * this.forwardForce);

        //Unityちゃんを矢印キーまたはボタンに応じて左右に移動させる（追加）
        if ((Input.GetKey(KeyCode.LeftArrow)) && -this.movableRange < this.transform.position.x)
        {
            //左に移動（追加）
            this.m_Rigidbody.AddForce(-this.turnForce, 0, 0);
        }
        else if ((Input.GetKey(KeyCode.RightArrow)) && this.transform.position.x < this.movableRange)
        {
            //右に移動（追加）
            this.m_Rigidbody.AddForce(this.turnForce, 0, 0);
        }
        //Jumpステートの場合はJumpにfalseをセットする
        /*
        if (this.m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            this.m_Animator.SetBool("Jump", false);
        }*/

        //ジャンプしていない時にスペースが押されたらジャンプする
        if (Input.GetKeyDown(KeyCode.Space) && this.transform.position.y < 0.5f)
        {
            //ジャンプアニメを再生
            this.m_Animator.SetBool("Jump", true);
            m_IsJump = true;
            //Unityちゃんに上方向の力を加える
            this.m_Rigidbody.AddForce(this.transform.up * this.upForce);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //障害物に衝突した場合（追加）
        if ( m_IsJump  && collision.gameObject.tag == "Ground")
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
            this.isEnd = true;
            //接触したコインのオブジェクトを破棄
            Destroy(other.gameObject);
        }
        
        //ゴール地点に到達した場合（追加）
        if (other.gameObject.tag == "GoalTag")
        {
            this.isEnd = true;
            
        }
    }
}
