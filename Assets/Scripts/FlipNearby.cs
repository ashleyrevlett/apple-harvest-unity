using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipNearby : MonoBehaviour
{
    public float magnetForce = 100;

    List<Rigidbody> caughtRigidbodies = new List<Rigidbody>();

    void FixedUpdate()
    {
        for (int i = 0; i < caughtRigidbodies.Count; i++)
        {
            Vector3 dir = (transform.position - caughtRigidbodies[i].transform.position).normalized;
            Quaternion rot = Quaternion.LookRotation(dir);
            caughtRigidbodies[i].transform.rotation = rot;
            // caughtRigidbodies[i].velocity = (transform.position - (caughtRigidbodies[i].transform.position + caughtRigidbodies[i].centerOfMass)) * magnetForce * Time.deltaTime;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Rigidbody>())
        {
            Rigidbody r = other.GetComponent<Rigidbody>();

            if(!caughtRigidbodies.Contains(r))
            {
                //Add Rigidbody
                caughtRigidbodies.Add(r);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Rigidbody>())
        {
            Rigidbody r = other.GetComponent<Rigidbody>();

            if (caughtRigidbodies.Contains(r))
            {
                //Remove Rigidbody
                r.transform.rotation = Quaternion.identity;
                caughtRigidbodies.Remove(r);
            }
        }
    }
}
