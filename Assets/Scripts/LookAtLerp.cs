using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtLerp : MonoBehaviour
{

    public Vector3 lookPosition = Vector3.zero;
    public float speed = 10f;

    float distance;
    float startingZ = 0f;

    Vector3 targetPosition;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // distance = Mathf.Min(3f, Vector3.Distance(lookPosition, transform.position));
        distance = Vector3.Distance(lookPosition, transform.position);
        // float inverseDistance = 3f - distance;

        if (distance != 0f) {
        // if (inverseDistance < 2f && transform.position.z >= 0f) {
            float newZ = Mathf.Pow(distance * .3f, 3f) * -1f;
            float maxValue = 3f;
            newZ = Mathf.Max(-1f * maxValue, newZ);
            newZ = Mathf.Min(maxValue, newZ);
            // newZ = Mathf.Min(2ff, Mathf.Max(-1f, distance));
            targetPosition = new Vector3(transform.position.x, transform.position.y, newZ);
        // } else {
        //     targetPosition = new Vector3(transform.position.x, transform.position.y, 0f);
        // }

            // transform.position = Vector3.Lerp(transform.position, targetPosition, 2f * Time.deltaTime);
            transform.position = targetPosition;

        }
        // if (lookPosition != Vector3.zero && distance <= .3f) {
        //     lookPosition = new Vector3(transform.position.x, transform.position.y, -10f);
        // }

        if (lookPosition != Vector3.zero) {
            Vector3 direction = lookPosition - transform.position;
            Quaternion toRotation = Quaternion.FromToRotation(transform.forward, direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, speed * Time.deltaTime);
        }

    }
}
