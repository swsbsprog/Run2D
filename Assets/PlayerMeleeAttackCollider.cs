using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAttackCollider : MonoBehaviour
{
    public int damage = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<BaseMonster>()?.OnDamge(damage);

        //if (monster == null)
        //    return;
        //monster.OnDamge(damage);
    }
}