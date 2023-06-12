using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;
    [SerializeField] float jumpSpeed;

    private CharacterController controller;
    private Animator anim;
    private Vector3 moveDir;
    private float curSpeed;
    private float ySpeed;
    private bool walk;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        // 계속해서 Move, Fall 시켜줌
        Move();
        Fall();
    }

    private void Move()
    {
        if (moveDir.magnitude == 0)
        {
            // 선형보간으로 천천히 멈추게끔 한다
            curSpeed = Mathf.Lerp(curSpeed, 0, 0.1f);
            anim.SetFloat("MoveSpeed", curSpeed);
            return;
        }


        // normalized는 크기가 1인 벡터를 반환해주는 일반화 과정이다
        Vector3 forwardVec = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z).normalized;
        Vector3 rightVec = new Vector3(Camera.main.transform.right.x, 0, Camera.main.transform.right.z).normalized;
        
        if (walk)       // 걸을 때
        {
            curSpeed = Mathf.Lerp(curSpeed, walkSpeed, 0.1f);
        }

        else            // 뛸 때
        {
            curSpeed = Mathf.Lerp(curSpeed, runSpeed, 0.1f);
        }

        // 벡터크기를 1로 만들려면 forwardVec.Normalize();

        controller.Move(forwardVec * moveDir.z * curSpeed * Time.deltaTime);
        controller.Move(rightVec * moveDir.x * curSpeed * Time.deltaTime);
        anim.SetFloat("MoveSpeed", curSpeed);

        // 앞방향과 오른쪽 방향에서, 누르는 값만큼 해당 방향을 바라본다
        Quaternion lookRotation = Quaternion.LookRotation(forwardVec * moveDir.z + rightVec * moveDir.x);
        // 자연스러운 회전과 방향전환을 위해 선형보간 진행
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 0.01f);       
    }

    private void OnMove(InputValue value)
    {
        moveDir.x = value.Get<Vector2>().x;
        moveDir.z = value.Get<Vector2>().y;
    }

    private void OnWalk(InputValue value)
    {
        // 누르고 있을 때 걷기
        walk = value.isPressed;
    }

    private void Fall()
    {
            ySpeed += Physics.gravity.y * Time.deltaTime;

        // 땅에 떨어지다가 닿았을 때
        if (controller.isGrounded && ySpeed < 0)
        {
            ySpeed = 0;
            // anim.SetBool("isJumping", false);
        }

        controller.Move(Vector3.up * ySpeed * Time.deltaTime);
    }

    private void Jump()
    {
        // 위로 향하는 속력을 갖게 됨
        ySpeed = jumpSpeed;

        // 트리거 파라미터 Jump 활성화
        anim.SetTrigger("Jump");
    }

    private void OnJump(InputValue value)
    {
        Jump();
    }
}
