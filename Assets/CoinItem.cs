using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinItem : MonoBehaviour
{
    bool isUse = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isUse)
            return;

        if (collision.transform.GetComponent<Player>() == null)
            return;

        //if (collision.transform.name == "Player")
        //    return;
        //if (collision.transform.CompareTag("Player") == false)
        //    return;

        isUse = true;
        //print(collision.transform);
        GetComponentInChildren<Animator>().Play("Hide", 1);
        RunGameManager.instance.AddCoin(100);

    }
}
