using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowLauncher : MonoBehaviour
{
    public ArrowLauncher Instance { get; private set; }

    public GameObject ArrowPrefab;
    
    void Awake()
    {
        Instance = this;
        InvokeRepeating("LaunchArrow", 1.0f, 0.3f);
    }

    public void LaunchArrow()
    {
        GameObject arrow = Instantiate(ArrowPrefab);
        arrow.transform.position = new Vector3(1, 2, 3);
        arrow.transform.forward =
            Vector3.Slerp(arrow.transform.forward, arrow.GetComponent<Rigidbody>().velocity.normalized, Time.deltaTime);

        arrow.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-2f, 2f), 1f, Random.Range(2f, 5f));
    }
}
