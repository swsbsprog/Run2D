﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public float speed = 20;
    void Update()
    {
        if (RunGameManager.IsPlaying() == false)
            return;

        transform.Translate(speed * Time.deltaTime, 0, 0);
    }
}
