using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtLerp : MonoBehaviour
{

    public Vector3 lookPosition = Vector3.zero;
    public float speed = 10f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(lookPosition, transform.position);
        if (lookPosition != Vector3.zero && distance <= .3f) {
            lookPosition = new Vector3(transform.position.x, transform.position.y, -10f);
        }

        if (lookPosition != Vector3.zero) {
            Vector3 direction = lookPosition - transform.position;
            Quaternion toRotation = Quaternion.FromToRotation(transform.forward, direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, speed * Time.deltaTime);
        }

    }
}
