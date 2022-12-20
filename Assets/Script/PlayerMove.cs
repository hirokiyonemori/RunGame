using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //レーンの移動の数値をそれぞれの変数で宣言します。
    const int MinLane = -2;
    const int MaxLane = 2;
    const float LaneWidth = 1.0f;

    //CharacterController型を変数controllerで宣言します。
    CharacterController controller;
    //Animator型を変数animatorで宣言します。
    Animator animator;

    //それぞれの座標を０で宣言します。
    Vector3 moveDirection = Vector3.zero;
    //int型を変数targetLaneで宣言します。
    int targetLane;

    //それぞれのパラメーターの設定をInspectorで変える様にします。
    public float gravity;
    public float speedZ;
    public float speedX;
    public float speedJump;
    public float accelerationZ;

    void Start()
    {
        //GetComponentでCharacterControllerwp取得して変数controllseで参照します。
        controller = GetComponent<CharacterController>();
        //GetComponentでAnimatorを取得して変数animatorで参照します。
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        //それぞれの矢印が押されたらそれぞれの関数を実行します。
        if (Input.GetKeyDown("left")) MoveToLeft();
        if (Input.GetKeyDown("right")) MoveToRight();
        if (Input.GetKeyDown("space")) Jump();

        float acceleratedZ = moveDirection.z + (accelerationZ * Time.deltaTime);
        moveDirection.z = Mathf.Clamp(acceleratedZ, 0, speedZ);

        float ratioX = (targetLane * LaneWidth - transform.position.x) / LaneWidth;
        moveDirection.x = ratioX * speedX;

        moveDirection.y -= gravity * Time.deltaTime;

        Vector3 globalDirection = transform.TransformDirection(moveDirection);
        controller.Move(globalDirection * Time.deltaTime);

        if (controller.isGrounded) moveDirection.y = 0;

        animator.SetBool("run", moveDirection.z > 0.0f);

    }

    //新しく作った関数のそれぞれの処理。
    public void MoveToLeft()
    {
        if (controller.isGrounded && targetLane > MinLane) targetLane--;
    }

    public void MoveToRight()
    {
        if (controller.isGrounded && targetLane < MaxLane) targetLane++;
    }

    public void Jump()
    {
        if (controller.isGrounded)
        {
            moveDirection.y = speedJump;

            animator.SetTrigger("jump");
        }
    }

}