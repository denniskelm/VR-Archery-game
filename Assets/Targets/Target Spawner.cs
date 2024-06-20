using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class TargetSpawner : MonoBehaviour
{
    public static TargetSpawner Instance { get; private set; }

    [SerializeField] Light CeilingLight;
    [SerializeField] GameObject TargetPrefab;
    [SerializeField] Color[] TargetColors;
    [SerializeField] Transform SpawnOrigin;
    [Space(10)]
    [SerializeField] bool DoRecordCSV; // will tell the recorder to record if it exists

    [Header("Settings")]
    [SerializeField] float MinTargetScale;
    [SerializeField] float MaxTargetScale;
    [SerializeField] [Range(0.1f, 2.0f)] float SpawnDelay = 1;
    [SerializeField] float RunDuration = 10;

    List<GameObject> targets = new();
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
    }

    public void GamePauseOrUnpause()
    {
        if (isRunning) RunEnd();
        else RunStart();
    }

    public void RunStart()
    {
        isRunning = true;
        timer = RunDuration;
        score = 0;
        spawmTimer = SpawnDelay;

        StartCoroutine(FadeCeilingLight(new(0.8f,1,0.8f), 1));
    }

    private void FixedUpdate()
    {
        //Todo: remove
        if (Keyboard.current.spaceKey.wasPressedThisFrame) RunStart();

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

        StartCoroutine(FadeCeilingLight(new(1, 0.5f, 0.5f), 1));

        foreach (var target in targets) Destroy(target);
        targets.Clear();
    }

    void SpawnTarget()
    {
        GameObject target = Instantiate(TargetPrefab, transform);
        targets.Add(target);

        target.transform.position = RandomSpawnPos();
        target.transform.localScale = Random.Range(MinTargetScale, MaxTargetScale) * target.transform.localScale;
        //target.transform.LookAt(GameObject.Find("Main Camera").transform);

        target.GetComponent<MeshRenderer>().material.color = TargetColors[Random.Range(0, TargetColors.Length)];
    }

    public void DestroyTarget(GameObject target)
    {
        score++;

        targets.Remove(target);
        Destroy(target, 1f);
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

    IEnumerator FadeCeilingLight(Vector4 goalColor, float duration) 
    {
        Vector4 initColor = CeilingLight.color;
        for (float time = 0; time < duration; time += Time.deltaTime)
        {
            CeilingLight.color =  Vector4.Lerp(initColor, goalColor, time / duration);
            yield return null;
        }
    }
}
