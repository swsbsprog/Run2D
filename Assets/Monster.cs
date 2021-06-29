using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private void Start()
    {
        InitWorldMoveArea();
        StartCoroutine(PatrolCo());
    }

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
            while (true)
            {
                if(direction == DirectionType.Right)
                {
                    if (maxWorldMoveX < transform.position.x)
                    {
                        direction = DirectionType.Left;
                        break;
                    }
                }
                else
                {
                    if (minWorldMoveX > transform.position.x)
                    {
                        direction = DirectionType.Right;
                        break;
                    }
                }

                float move = direction == DirectionType.Right ? speed : -speed;
                transform.Translate(move * Time.deltaTime, 0, 0);
                yield return null;
            }
        }
    }

    public int hp = 10;
    internal void OnDamge(int damage)
    {
        hp -= damage;

        GetComponentInChildren<Animator>().Play("Hit");

        if( hp <= 0)
        {
            StartCoroutine(DieCo());
        }
    }
    public float dieDelay = 0.3f;
    public float destroyDelay = 0.7f;
    private IEnumerator DieCo()
    {
        yield return new WaitForSeconds(dieDelay);
        GetComponentInChildren<Animator>().Play("Die");
        yield return new WaitForSeconds(destroyDelay);
        Destroy(gameObject);
    }
}
