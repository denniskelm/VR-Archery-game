using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowLauncher : MonoBehaviour
{

    public GameObject ArrowPrefab;

    private GameObject instance;
    
    void Start()
    {
        InvokeRepeating("launchArrow", 1.0f, 0.3f);
    }

    void FixedUpdate(){
        if(instance != null && instance.GetComponent<Rigidbody>().velocity != Vector3.zero)
            instance.GetComponent<Rigidbody>().rotation = Quaternion.LookRotation(instance.GetComponent<Rigidbody>().velocity);  
    }

    public void launchArrow()
    {
        GameObject arrow = ArrowPrefab;
        arrow.transform.position = new Vector3(1, 2, 3);
        

        instance = Instantiate(arrow);
        instance.transform.forward =
            Vector3.Slerp(instance.transform.forward, instance.GetComponent<Rigidbody>().velocity.normalized, Time.deltaTime);

        instance.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-2f, 2f), 1f, Random.Range(2f, 5f));
    }
}
