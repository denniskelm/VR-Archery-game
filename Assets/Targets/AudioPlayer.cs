using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public static AudioPlayer Instance { get; private set; }

    [Header("Recorder Sounds")]
    [SerializeField] AudioClip RecordingStart;
    [SerializeField] AudioClip RecordingEnd;
    [SerializeField] AudioClip RecordingFail; // played when a recording fails (e.g. due to the amount of features changing)

    [Header("Game Sounds")]
    [SerializeField] AudioClip BubblePop;

    private void Awake()
    {
        Instance = this;
    }

    public void PlayRecordingStart()
    {
        GetComponent<AudioSource>().clip = RecordingStart;
        GetComponent<AudioSource>().Play();
    }
    public void PlayRecordingEnd()
    {
        GetComponent<AudioSource>().clip = RecordingEnd;
        GetComponent<AudioSource>().Play();
    }
    public void PlayRecordingFail()
    {
        GetComponent<AudioSource>().clip = RecordingFail;
        GetComponent<AudioSource>().Play();
    }
}
