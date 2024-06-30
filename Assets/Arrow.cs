using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public GameObject dot;

    void FixedUpdate()
    {
        if (GetComponent<Rigidbody>().velocity != Vector3.zero)
            GetComponent<Rigidbody>().rotation = Quaternion.LookRotation(GetComponent<Rigidbody>().velocity);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);

            // Optional: Set the object's position to the contact point for precise sticking
            ContactPoint contact = collision.contacts[0];
            Instantiate(dot, contact.point, dot.transform.rotation);
        }
    }
}
