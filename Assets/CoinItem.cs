using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Coin Enter:" + collision.transform);
        GetComponentInChildren<Animator>().Play("HideCoin", 1);
    }
}
