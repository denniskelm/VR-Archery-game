using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public static AudioPlayer Instance { get; private set; }

    [Header("Recorder Sounds")]
    [SerializeField] AudioClip BowLoading;
    [SerializeField] AudioClip BowRelease;

    [Header("Game Sounds")]
    [SerializeField] AudioClip BubblePop;

    private void Awake()
    {
        Instance = this;
    }

    public void PlayBowLoading()
    {
        GetComponent<AudioSource>().clip = BowLoading;
        GetComponent<AudioSource>().Play();
    }
    public void PlayBowRelease()
    {
        GetComponent<AudioSource>().clip = BowRelease;
        GetComponent<AudioSource>().Play();
    }
}
