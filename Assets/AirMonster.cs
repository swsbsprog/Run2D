using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AirMonster : MonoBehaviour
{
    AIPath aiPath;

    private IEnumerator Start()
    {
        aiPath = GetComponent<AIPath>();
        // 오른쪽을 갈때 0도, 왼쪽 갈때는 180 로테이션 y를 설정하자.
        while (true)
        {
            if (aiPath.desiredVelocity.x > 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if(aiPath.desiredVelocity.x < 0) //왼쪽으로 이동중 -> 180
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }

            yield return null;
        }
    }

}
