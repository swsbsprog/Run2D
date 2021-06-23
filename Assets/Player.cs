using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        //animator = transform.Find("Sprite").GetComponent<Animator>();
        animator = GetComponentInChildren<Animator>();
        animator.Play("Run");
    }
    public float speed = 20;
    void Update()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);
    }
}
