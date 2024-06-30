using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public static AudioPlayer Instance { get; private set; }

    [Header("Recorder Sounds")]
    [SerializeField] AudioClip BowLoading;
    [SerializeField] AudioClip BowRelease;
    [SerializeField] AudioClip TargetHit;

    private void Awake()
    {
        Instance = this;
    }

    public void PlayBowLoading()
    {
        GetComponent<AudioSource>().PlayOneShot(BowLoading);
    }
    public void PlayBowRelease()
    {
        GetComponent<AudioSource>().PlayOneShot(BowRelease);
    }
    public void PlayTargetHit()
    {
        GetComponent<AudioSource>().PlayOneShot(TargetHit);
    }
}
