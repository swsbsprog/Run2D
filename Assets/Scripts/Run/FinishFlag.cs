using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Run
{
    public class FinishFlag : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform.GetComponent<Player>() == null)
                return;

            RunGameManager.instance.EndStage();
        }
    }
}
