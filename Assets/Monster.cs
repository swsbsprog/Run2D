using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
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
