using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public GameObject dot;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            gameObject.SetActive(false);

            // Optional: Set the object's position to the contact point for precise sticking
            ContactPoint contact = collision.contacts[0];
            Instantiate(dot, contact.point, dot.transform.rotation);
        }
    }
}
