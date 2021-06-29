using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private void Start()
    {
        animator = transform.GetComponentInChildren<Animator>();
        InitWorldMoveArea();
        patrolHandle = StartCoroutine(PatrolCo());
    }
    Coroutine patrolHandle;

    private void InitWorldMoveArea()
    {
        minWorldMoveX = transform.position.x + minLocalMoveX;
        maxWorldMoveX = transform.position.x + maxLocalMoveX;
    }

    public float speed = 3;
    public float minLocalMoveX = -5;
    public float maxLocalMoveX = 5;
    public float minWorldMoveX;
    public float maxWorldMoveX;
    Animator animator;
    public DirectionType Direction
    {
        get { return direction; }
        set
        {
            direction = value;
            UpdateDirectionSprite();
            transform.rotation = Quaternion.Euler(0, direction == DirectionType.Left ? 180 : 0, 0);
        }
    }

    private void UpdateDirectionSprite()
    {
        animator.Play(direction.ToString());
    }

    public DirectionType direction = DirectionType.Right;
    public enum DirectionType
    {
        Right,
        Left,
    }
    private IEnumerator PatrolCo()
    {
        while (true)
        {
            UpdateDirectionSprite();
            while (true)
            {
                if(Direction == DirectionType.Right)
                {
                    if (maxWorldMoveX < transform.position.x)
                    {
                        Direction = DirectionType.Left;
                        break;
                    }
                }
                else
                {
                    if (minWorldMoveX > transform.position.x)
                    {
                        Direction = DirectionType.Right;
                        break;
                    }
                }

                //float move = Direction == DirectionType.Right ? speed : -speed;
                transform.Translate(speed * Time.deltaTime, 0, 0);
                yield return null;

                while (state == StateType.Hit)
                    yield return null;
            }
        }
    }

    public enum StateType
    {
        Idle,
        Run,
        Hit,
        Die,
    }
    public StateType state = StateType.Idle;
    public int hp = 10;
    internal void OnDamge(int damage)
    {
        if (state == StateType.Die)
            return;

        hp -= damage;
        GetComponentInChildren<Animator>().Play("Hit");

        if( hp > 0)
        {
            StartCoroutine(HitCo());
        }
        else
        {
            StartCoroutine(DieCo());
        }
    }

    public float hitTime = 0.4f;
    private IEnumerator HitCo()
    {
        state = StateType.Hit;
        yield return new WaitForSeconds(hitTime);
        state = StateType.Run;
        UpdateDirectionSprite();
    }

    public float dieDelay = 0.3f;
    public float destroyDelay = 0.7f;
    private IEnumerator DieCo()
    {
        state = StateType.Die;
        StopCoroutine(patrolHandle);

        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().gravityScale = 0;

        yield return new WaitForSeconds(dieDelay);
        GetComponentInChildren<Animator>().Play("Die");
        yield return new WaitForSeconds(destroyDelay);
        Destroy(gameObject);
    }

    internal bool Attackable()
    {
        return state == StateType.Idle || state == StateType.Run;
    }
}
