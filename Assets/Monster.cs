using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    // 앞뒤로 이동하는 몬스터.
    public float range = 5.5f;
    float minWorldX;
    float maxWorldX;
    public float speed = 5;
    public enum DirectionType
    {
        Right,
        Left,
    }
    StateType state = StateType.Patrol;
    public enum StateType
    {
        Patrol,
        Attack,
        Attacked,
        Die
    }
    DirectionType direction = DirectionType.Right;
    Animator animator;
    private IEnumerator Start()
    {
        animator = GetComponentInChildren<Animator>();
        minWorldX = transform.position.x - range;
        maxWorldX = transform.position.x + range;

        animator.Play("Run");
        while (true)
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
            if (direction == DirectionType.Right)
            {
                if(transform.position.x > maxWorldX)
                {
                    direction = DirectionType.Left;
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                }
            }
            else
            {
                if (transform.position.x < minWorldX)
                {
                    direction = DirectionType.Right;
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
            }

            yield return null;

            if (state == StateType.Die)
                yield break;
        }
    }

    public int hp = 10;
    internal void OnDamge(int damage)
    {
        hp -= damage;

        animator.Play("Hit");

        if( hp <= 0)
        {
            StartCoroutine(DieCo());
        }
    }
    public float dieDelay = 0.3f;
    public float destroyDelay = 0.7f;
    internal int damage = 1;

    private IEnumerator DieCo()
    {
        state = StateType.Die;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().gravityScale = 0;
        yield return new WaitForSeconds(dieDelay);
        GetComponentInChildren<Animator>().Play("Die");
        yield return new WaitForSeconds(destroyDelay);
        Destroy(gameObject);
    }

    internal bool IsDie()
    {
        return state == StateType.Die;
    }
}
