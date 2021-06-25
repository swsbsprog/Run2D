using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rigid;
    public Vector2 jumpForce = new Vector2(0, 1000);
    public float gravityScale = 7;
    void Start()
    {
        //animator = transform.Find("Sprite").GetComponent<Animator>();
        animator = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        rigid.gravityScale = gravityScale;
        //animator.Play("Run");
    }
    public float speed = 20;
    public float midAirVelocity = 10;
    void Update()
    {
        if (RunGameManager.IsPlaying() == false)
            return;

        transform.Translate(speed * Time.deltaTime, 0, 0);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigid.velocity = Vector2.zero;
            rigid.AddForce(jumpForce);
        }

        float velocity = rigid.velocity.y;
        float absVelocity = Mathf.Abs(velocity);
        //float absVelocity = velocity > 0 ? velocity : -velocity;
        //float absVelocity = velocity;
        //if (absVelocity > 0)
        //    absVelocity = -velocity;

        //string animationName = "";
        string animationName = string.Empty;
        if (IsGround())
        {
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
