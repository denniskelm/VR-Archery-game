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

        if (collision.gameObject.CompareTag("Target"))
        {
            TargetSpawner.Instance.DestroyTarget(collision.gameObject);
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Starttarget"))
        {
            if (collision.gameObject.name == "Easy") TargetSpawner.Instance.RunStart(TargetSpawner.Difficulty.easy);
            if (collision.gameObject.name == "Medium") TargetSpawner.Instance.RunStart(TargetSpawner.Difficulty.medium);
            if (collision.gameObject.name == "Hard") TargetSpawner.Instance.RunStart(TargetSpawner.Difficulty.hard);

            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
