using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MagnetAbility : MonoBehaviour
{
    private void Awake()
    {
        instance = this;
    }
    class RefFloat
    {
        public float acc;
    }
    Dictionary<Transform, RefFloat> items = new Dictionary<Transform, RefFloat>(); //<자석에 이끌린 TR, 가속도>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.GetComponent<CoinItem>() == null)
            return;

        if (items.ContainsKey(collision.transform))
            return;

        items[collision.transform] = new RefFloat();
    }

    public float accelerate = 30; // 초당 가속도.
    internal static MagnetAbility instance;

    internal void RemoveItem(Transform transform)
    {
        items.Remove(transform);
    }

    private void Update()
    {
        var pos = transform.position;

        foreach (var item in items)
        {
            var coinTr = item.Key;
            float acceleration = item.Value.acc + accelerate * Time.deltaTime;
            items[item.Key].acc = acceleration;

            Vector2 dir = (pos - coinTr.position).normalized;
            Vector2 move = dir * (acceleration) * Time.deltaTime;
            coinTr.Translate(move);
        }
    }
}
