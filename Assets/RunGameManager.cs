using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RunGameManager : MonoBehaviour
{
    public static RunGameManager instance;
    private void Awake()
    {
        instance = this;
    }

    TextMeshProUGUI timeText;
    public int waitSeconds = 3;
    IEnumerator Start()
    {
        // 캐릭터랑 카메라랑 뭠춰야한다.
        gameStateType = GameStateType.Ready;
        // 3,2,1, Start표시.
        timeText = transform.Find("TimeText").GetComponent<TextMeshProUGUI>();
        for (int i = waitSeconds; i > 0; i--)
        {
            timeText.text = i.ToString();
            yield return new WaitForSeconds(1);
        }
        timeText.text = "Start";
        gameStateType = GameStateType.Playing;

        yield return new WaitForSeconds(0.5f);
        timeText.text = "";

    }

    internal static bool IsPlaying()
    {
        return instance.gameStateType != GameStateType.Playing;
    }

    public GameStateType gameStateType = GameStateType.NotInit;

    public enum GameStateType
    {
        NotInit,
        Ready,
        Playing,
        End,
    }
    // 게임시작전인지?
    /// 게임중인지?
    /// // 끝났는지?
}
