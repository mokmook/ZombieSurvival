using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5; //앞뒤 움직임 속도
    [SerializeField] float rotateSpeed = 180f; //좌우 회전 속도

    PlayerInput playerInput;

    Rigidbody rb;
    Animator anim;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        Move();
        Rotate();

        anim.SetFloat("Move", playerInput.move);
    }

    void Move()
    {
        Vector3 moveDistance = playerInput.move * transform.forward * moveSpeed * Time.deltaTime;

        rb.MovePosition(rb.position + moveDistance);
    }
    void Rotate()
    {
        float turn = playerInput.rotate * rotateSpeed * Time.deltaTime;

        rb.rotation = rb.rotation * Quaternion.Euler(0, turn, 0f);
    }
}
