using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class TargetSpawner : MonoBehaviour
{
    public static TargetSpawner Instance { get; private set; }

    public enum Difficulty { easy, medium, hard };

    [SerializeField] Light CeilingLight;
    [SerializeField] GameObject TargetPrefab;
    [SerializeField] GameObject StarttargetsPrefab;
    [SerializeField] Color[] TargetColors;
    [SerializeField] Transform SpawnOrigin;

    [Header("Settings")]
    [SerializeField] float MinTargetScale;
    [SerializeField] float MaxTargetScale;
    [SerializeField] [Range(0.1f, 2.0f)] float SpawnDelay = 1;
    [SerializeField] float RunDuration;

    List<GameObject> targets = new();
    GameObject Starttargets;
    float spawmTimer; // seconds before the next target spawns
    public float timer { get; private set; } // seconds left the state progresses
    public int score { get; private set; }
    public int runCounter { get; private set; } //counts how many runs were made this session
    public bool isRunning { get; private set; }


    private void Awake()
    {
        Instance = this;
        isRunning = false;
        runCounter = 0;

        Starttargets = Instantiate(StarttargetsPrefab);
    }

    public void RunStart(Difficulty difficulty)
    {
        switch (difficulty)
        {
            case Difficulty.easy:
                MinTargetScale = 1.5f;
                MaxTargetScale = 2f;
                break;
            case Difficulty.medium:
                MinTargetScale = 0.8f;
                MaxTargetScale = 1.3f;
                break;
            case Difficulty.hard:
                MinTargetScale = 0.3f;
                MaxTargetScale = 0.8f;
                break;
        }

        Destroy(Starttargets);

        isRunning = true;
        timer = RunDuration;
        score = 0;
        spawmTimer = SpawnDelay;

        StartCoroutine(FadeCeilingLight(-30, 1));
    }
        
    private void FixedUpdate()
    {
        if (isRunning)
        {
            spawmTimer -= Time.deltaTime;
            timer -= Time.deltaTime;

            if (false || spawmTimer < 0)
            {
                SpawnTarget();
                spawmTimer = SpawnDelay * targets.Count;
            }

            if (timer < 0)
            {
                runCounter++;
                RunEnd();
            }
        }
    }

    void RunEnd()
    {
        isRunning = false;

        StartCoroutine(FadeCeilingLight(30, 1));

        foreach (var target in targets) Destroy(target);
        targets.Clear();

        Starttargets = Instantiate(StarttargetsPrefab);
    }

    void SpawnTarget()
    {
        GameObject target = Instantiate(TargetPrefab);
        targets.Add(target);

        target.transform.position = RandomSpawnPos();
        target.transform.localScale = Random.Range(MinTargetScale, MaxTargetScale) * target.transform.localScale;
        //target.transform.LookAt(GameObject.Find("Main Camera").transform);

        target.GetComponent<MeshRenderer>().material.color = TargetColors[Random.Range(0, TargetColors.Length)];
    }

    public void DestroyTarget(GameObject target)
    {
        score++;
        
        AudioPlayer.Instance.PlayTargetHit();

        targets.Remove(target);
        Destroy(target);
    }

    Vector3 RandomSpawnPos()
    {
        Vector3 b = SpawnOrigin.localScale/2;

        return SpawnOrigin.position + new Vector3(
                Random.Range(-b.x, b.x),
                Random.Range(-b.y, b.y),
                Random.Range(-b.z, b.z)
            );
    }

    IEnumerator FadeCeilingLight(float xDeg, float duration) 
    {
        Vector3 initRot = CeilingLight.transform.rotation.eulerAngles;
        Vector3 goalRot = new(initRot.x + xDeg, initRot.y, initRot.z);
        for (float time = 0; time < duration; time += Time.deltaTime)
        {
            CeilingLight.transform.eulerAngles =  Vector3.Lerp(initRot, goalRot, time / duration);
            yield return null;
        }
    }
}
