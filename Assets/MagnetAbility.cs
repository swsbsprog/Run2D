using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MagnetAbility : MonoBehaviour
{
    Dictionary<Transform, float> items = new Dictionary<Transform, float>(); //<자석에 이끌린 TR, 가속도>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.GetComponent<CoinItem>() == null)
            return;

        if (items.ContainsKey(collision.transform))
            return;

        items[collision.transform] = 0;
    }

    public float accelerate = 30; // 초당 가속도.

    Dictionary<Transform, float> tmpItems = new Dictionary<Transform, float>();
    private void Update()
    {
        var pos = transform.position;

        tmpItems.Clear();
        foreach (var item in items)
        {
            tmpItems[item.Key] = item.Value;
        }

        foreach (var item in tmpItems)
        {
            var coinTr = item.Key;
            float acceleration = item.Value + accelerate * Time.deltaTime;
            items[item.Key] = acceleration;

            Vector2 dir = (pos - coinTr.position).normalized;
            Vector2 move = dir * (acceleration) * Time.deltaTime;
            coinTr.Translate(move);
        }
    }
}
