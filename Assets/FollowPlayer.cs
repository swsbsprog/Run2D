using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public float lerp = 0.01f;

    public Vector3 offset = new Vector3(20, 18, 0);
    public float lerpMiddleY = 0.02f;
    void Update()
    {
        Player.Direction playerDirection = Player.instance.MoveDirection;
        float forwardMove = playerDirection == Player.Direction.Stop ? 0
            : (playerDirection == Player.Direction.Right ? 1
            : -1);

        var newPos = transform.position;
        newPos.x = Mathf.Lerp(newPos.x, Player.instance.transform.position.x + forwardMove * offset.x, lerp);
        newPos.y = Mathf.Lerp(newPos.y, Player.instance.transform.position.y, lerp);
        newPos.y = Mathf.Lerp(newPos.y, offset.y, lerpMiddleY);

        transform.position = newPos;
    }
}
