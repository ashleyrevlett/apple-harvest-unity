using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LeafBlower : MonoBehaviour
{
    // Variables for the leaf blower
    public float force = 10.0f;
    public float radius = 5.0f;
    // Variables for detecting touch input
    private Vector2 touchPosition;
    private Vector2 touchDelta;
    private Vector2 previousTouchPosition;
    private float planePos;
    private Plane plane;

    void Start()
    {
        planePos = gameObject.transform.position.y;
    }


    public Vector3 GetWorldPositionOnPlane(Vector3 screenPosition, float z) {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        Plane xy = new Plane(Vector3.up, new Vector3(0, 0, z));
        plane = xy;
        float distance;
        xy.Raycast(ray, out distance);
        return ray.GetPoint(distance);
    }


    void Update()
    {
        Debug.DrawLine(Vector3.zero, plane.normal, Color.white, 0f);

         if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            // Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
            Vector2 touchPos = GetWorldPositionOnPlane(touch.position, 0f);
            Debug.Log($"touchPos: {touchPos}");
            // Vector3 objPos = new Vector3(touchPos.x, planePos, touchPos.y);
            Vector3 objPos = new Vector3(touchPos.x, touchPos.y, 0f);

            gameObject.transform.position = objPos;
        }

        // Check for touch input
        // if (Input.touchCount > 0)
        // {
        //     Touch touch = Input.GetTouch(0);
        //     // Store the current touch position
        //     touchPosition = touch.position;

        //     // Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
        //     // Vector3 objPos = new Vector3(touchPos.x, planePos, touchPos.y);

        //     // gameObject.transform.position = objPos;

        //     // Calculate the difference between the current and previous touch positions
        //     touchDelta = touchPosition - previousTouchPosition;
        //     Debug.Log($"{touchPosition} -- {touchDelta}");

        //     // // Move the leaf blower in the direction of the touch delta
        //     transform.position += new Vector3(touchDelta.x, 0.0f, touchDelta.y);
        //     // // Store the current touch position for the next frame
        //     previousTouchPosition = touchPosition;
        // }
        // Get a list of all the rigidbody objects in the scene
        var rigidbodies = FindObjectsOfType<Rigidbody>();
        // Apply a force to each rigidbody in the scene
        foreach (var rb in rigidbodies)
        {
            // Calculate the distance between the leaf blower and the rigidbody
            float distance = Vector3.Distance(transform.position, rb.transform.position);
            // Only apply the force if the rigidbody is within the radius of the leaf blower
            if (distance <= radius)
            {
                // Calculate the direction from the leaf blower to the rigidbody
                Vector3 direction = (rb.transform.position - transform.position).normalized;
                // Apply a force to the rigidbody in the direction of the leaf blower
                rb.AddForce(direction * force, ForceMode.Impulse);
            }
        }
    }
}