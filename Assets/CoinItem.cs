using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.GetComponent<Player>() == null)
            return;

        //if (collision.transform.name == "Player")
        //    return;
        //if (collision.transform.CompareTag("Player") == false)
        //    return;
              
        GetComponent<Collider2D>().enabled = false;

        //print(collision.transform);
        GetComponentInChildren<Animator>().Play("Hide", 1);
        RunGameManager.instance.AddCoin(100);
        MagnetAbility.instance.RemoveItem(transform);

        Destroy(gameObject, 2);
    }
}
