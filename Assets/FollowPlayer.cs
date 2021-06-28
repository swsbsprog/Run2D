using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public float lerp = 0.01f;

    void Update()
    {
        var newPos = transform.position;
        newPos.x = Mathf.Lerp(newPos.x,  Player.instance.transform.position.x, lerp);
        transform.position = newPos;
    }
}
