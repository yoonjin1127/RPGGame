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
        // ����ؼ� Move, Fall ������
        Move();
        Fall();
    }

    private void Move()
    {
        if (moveDir.magnitude == 0)
        {
            // ������������ õõ�� ���߰Բ� �Ѵ�
            curSpeed = Mathf.Lerp(curSpeed, 0, 0.1f);
            anim.SetFloat("MoveSpeed", curSpeed);
            return;
        }


        // normalized�� ũ�Ⱑ 1�� ���͸� ��ȯ���ִ� �Ϲ�ȭ �����̴�
        Vector3 forwardVec = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z).normalized;
        Vector3 rightVec = new Vector3(Camera.main.transform.right.x, 0, Camera.main.transform.right.z).normalized;
        
        if (walk)       // ���� ��
        {
            curSpeed = Mathf.Lerp(curSpeed, walkSpeed, 0.1f);
        }

        else            // �� ��
        {
            curSpeed = Mathf.Lerp(curSpeed, runSpeed, 0.1f);
        }

        // ����ũ�⸦ 1�� ������� forwardVec.Normalize();

        controller.Move(forwardVec * moveDir.z * curSpeed * Time.deltaTime);
        controller.Move(rightVec * moveDir.x * curSpeed * Time.deltaTime);
        anim.SetFloat("MoveSpeed", curSpeed);

        // �չ���� ������ ���⿡��, ������ ����ŭ �ش� ������ �ٶ󺻴�
        Quaternion lookRotation = Quaternion.LookRotation(forwardVec * moveDir.z + rightVec * moveDir.x);
        // �ڿ������� ȸ���� ������ȯ�� ���� �������� ����
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 0.01f);       
    }

    private void OnMove(InputValue value)
    {
        moveDir.x = value.Get<Vector2>().x;
        moveDir.z = value.Get<Vector2>().y;
    }

    private void OnWalk(InputValue value)
    {
        // ������ ���� �� �ȱ�
        walk = value.isPressed;
    }

    private void Fall()
    {
            ySpeed += Physics.gravity.y * Time.deltaTime;

        // ���� �������ٰ� ����� ��
        if (controller.isGrounded && ySpeed < 0)
        {
            ySpeed = 0;
            // anim.SetBool("isJumping", false);
        }

        controller.Move(Vector3.up * ySpeed * Time.deltaTime);
    }

    private void Jump()
    {
        // ���� ���ϴ� �ӷ��� ���� ��
        ySpeed = jumpSpeed;

        // Ʈ���� �Ķ���� Jump Ȱ��ȭ
        anim.SetTrigger("Jump");
    }

    private void OnJump(InputValue value)
    {
        Jump();
    }
}
