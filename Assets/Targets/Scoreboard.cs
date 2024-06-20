using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scoreboard : MonoBehaviour
{

    public static Scoreboard Instance { get; private set; }

    [SerializeField] TMP_Text ScoreboardText;

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        switch (TargetSpawner.Instance.isRunning)
        {
            case false:
                ScoreboardText.text =
                    $"Game Paused.\n" +
                    $"Pinch with left hand to start.";
                break;
            case true:
                ScoreboardText.text =
                    $"Bubbles popped: {TargetSpawner.Instance.score:00}\n" + 
                    $"Time left: {TargetSpawner.Instance.timer:00.0}";
                break;
        }
    }
}
