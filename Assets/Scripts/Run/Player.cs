﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Run
{
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
            //animator = transform.Find("Sprite").GetComponent<Animator>();
            animator = GetComponentInChildren<Animator>();
            rigid = GetComponent<Rigidbody2D>();
            rigid.gravityScale = gravityScale;
            cameraTr = Camera.main.transform;
            offsetXCameraPos = cameraTr.position.x - transform.position.x;
            // offsetXCameraPos : 3
            //animator.Play("Run");
        }

        public Transform cameraTr;
        public float offsetXCameraPos;  // 카메라와 나의 차이 기본값
        public float allowOffsetX = 0.2f;
        public float restoreSpeed = 40;
        private void RestoreXPosition()
        {
            float offsetX = cameraTr.position.x - transform.position.x;
            if (offsetX > offsetXCameraPos + allowOffsetX)
            {
                // 위치를 수정해야한다.
                transform.Translate(restoreSpeed * Time.deltaTime, 0, 0);
            }
        }

        public float speed = 20;
        public float midAirVelocity = 10;
        int jumpCount = 0;
        void Update()
        {
            if (RunGameManager.IsPlaying() == false)
                return;

            transform.Translate(speed * Time.deltaTime, 0, 0);

            if (jumpCount < 1)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    jumpCount++;
                    rigid.velocity = Vector2.zero;
                    rigid.AddForce(jumpForce);
                }
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


            RestoreXPosition();
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
}
