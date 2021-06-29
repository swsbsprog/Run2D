using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAttackCollider : MonoBehaviour
{
    public int damage = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<IDamgeable>()?.OnDamge(damage);

        //Monster monster = collision.GetComponent<Monster>();
        //if (monster == null)
        //    return;
        //monster.OnDamge(damage);
    }
}

public interface IDamgeable
{
    public void OnDamge(int damage);
}
