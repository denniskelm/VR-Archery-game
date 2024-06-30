using UnityEngine;
using TMPro;

public class Scoreboard : MonoBehaviour
{

    public static Scoreboard Instance { get; private set; }

    public TMP_Text TargetsHitText;
    public TMP_Text TimeLeftText;
    public GameObject GamePausedText;
    
    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        switch (TargetSpawner.Instance.isRunning)
        {
            case false:
                GamePausedText.SetActive(true);
                break;
            case true:
                GamePausedText.SetActive(false);
                TargetsHitText.text = $"{TargetSpawner.Instance.score:00}";
                TimeLeftText.text = $"{TargetSpawner.Instance.timer:00.0}";
                break;
        }
    }
}
