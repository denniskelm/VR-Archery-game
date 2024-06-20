using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowLauncher : MonoBehaviour
{

    public GameObject ArrowPrefab;
    
    void Start()
    {
        InvokeRepeating("launchArrow", 1.0f, 0.3f);
    }

    void Update()
    {
        
    }

    public void launchArrow()
    {
        GameObject arrow = ArrowPrefab;
        arrow.transform.position = new Vector3(1, 2, 3);
        

        GameObject instance = Instantiate(arrow);
        instance.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-2f, 2f), 1f, Random.Range(2f, 5f));
    }
}
