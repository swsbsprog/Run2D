using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetAbility : MonoBehaviour
{
    Dictionary<Transform, float> items = new Dictionary<Transform, float>(); //<자석에 이끌린 TR, 가속도>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.GetComponent<CoinItem>() == null)
            return;

        items[collision.transform] = 0;
    }

    public float accelerate = 3000;
    private void Update()
    {
        var pos = transform.position;
        
        foreach(var item in items)
        {
            var coinTr = item.Key;
            float acceleration = item.Value;
            Vector2 dir = (pos - coinTr.position).normalized;
            Vector2 move = dir * (acceleration + accelerate) * Time.deltaTime;
            coinTr.Translate(move);
        }
    }
}
