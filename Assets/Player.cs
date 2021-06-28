using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    Animator animator;
    Rigidbody2D rigid;
    public Vector2 jumpForce = new Vector2(0, 1000);
    public float gravityScale = 7;
    private void Awake()
    {
        instance = this;
    }

    internal void OnEndStage()
    {
        animator.Play("Idle");
    }

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        rigid.gravityScale = gravityScale;
    }


    public float speed = 20;
    public float midAirVelocity = 10;
    int jumpCount = 0;
    void Update()
    {
        Move();

        Jump();

        UpdateSprite();
    }

    private void UpdateSprite()
    {
        float velocity = rigid.velocity.y;
        float absVelocity = Mathf.Abs(velocity);

        string animationName = string.Empty;
        if (IsGround())
        {
            jumpCount = 0;
            animationName = "Run";
        }
        else
        {
            if (absVelocity < midAirVelocity)
                animationName = "Jump_MidAir";
            else if (velocity > 0)
                animationName = "Jump_Up";               //상승. 
            else//하락
            {
                animationName = "Jump_Fall";
            }
        }
        animator.Play(animationName);
    }

    private void Jump()
    {
        if (jumpCount < 1)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                jumpCount++;
                rigid.velocity = Vector2.zero;
                rigid.AddForce(jumpForce);
            }
        }
    }

    private void Move()
    {
        //a, d, 좌우 이동.
        float move = 0;
        if (Input.GetKey(KeyCode.A)) move = -1;
        if (Input.GetKey(KeyCode.D)) move = 1;

        transform.Translate(move * speed * Time.deltaTime, 0, 0, Space.World);

        UpdateRotation(move);
    }

    private void UpdateRotation(float currentMove)
    {
        if (currentMove == 0)
            return;

        transform.rotation = Quaternion.Euler(0, currentMove < 0 ? 180 : 0, 0);
    }

    public Transform rayStart;
    public float rayCheckDistance = 0.1f;
    public LayerMask groundLayer;
    private bool IsGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(rayStart.position, Vector2.down, rayCheckDistance, groundLayer);
        return hit.transform != null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(rayStart.position, Vector2.down * rayCheckDistance);
    }
}