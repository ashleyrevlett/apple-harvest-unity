using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CubeTouchMagnet : MonoBehaviour
{
    List<Rigidbody> caughtRigidbodies = new List<Rigidbody>();

    // Start is called before the first frame update
    void Start()
    {

    }

    public Vector3 GetWorldPositionOnPlane(Vector3 screenPosition, float z) {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        Plane xy = new Plane(Vector3.forward, new Vector3(0, 0, z));
        float distance;
        xy.Raycast(ray, out distance);
        return ray.GetPoint(distance);
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            // Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
            Vector2 touchPos = GetWorldPositionOnPlane(touch.position, 0f);
            Vector3 objPos = new Vector3(touchPos.x, touchPos.y, 0f);
            gameObject.transform.position = objPos;
            // Debug.Log($"moving to {objPos}");
        } else {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -5f);
        }
    }

    // void OnTriggerEnter(Collider other)
    // {
    //     if (other.GetComponent<Rigidbody>())
    //     {
    //         Rigidbody r = other.GetComponent<Rigidbody>();

    //         if(!caughtRigidbodies.Contains(r))
    //         {
    //             //Add Rigidbody
    //             caughtRigidbodies.Add(r);
    //         }
    //     }
    // }

    // void OnTriggerExit(Collider other)
    // {
    //     if (other.GetComponent<Rigidbody>())
    //     {
    //         Rigidbody r = other.GetComponent<Rigidbody>();

    //         if (caughtRigidbodies.Contains(r))
    //         {
    //             //Remove Rigidbody
    //             // r.transform.rotation = Quaternion.identity;
    //             caughtRigidbodies.Remove(r);
    //         }
    //     }
    // }
}
