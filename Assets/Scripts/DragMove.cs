using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

public class DragMove : MonoBehaviour
{
    public float rotationSpeed = 10f;
    bool isMoving = false;
    Vector2 lastTouchPos;
    Vector3 moveDir;
    List<Rigidbody> caughtRigidbodies = new List<Rigidbody>();

    AudioSource sound;

    void Start()
    {
        sound = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
            Vector3 objPos = new Vector3(touchPos.x, touchPos.y, gameObject.transform.position.z);

            switch (touch.phase)
            {
                //When a touch has first been detected, change the message and record the starting position
                case TouchPhase.Began:
                    lastTouchPos = touchPos;
                    isMoving = true;
                    break;

                //Determine if the touch is a moving touch
                case TouchPhase.Moved:


                    // Determine direction by comparing the current touch position with the initial one
                    gameObject.transform.position = objPos;
                    Vector2 tempMoveDir = (new Vector2(objPos.x, objPos.y) - lastTouchPos).normalized;
                    if (tempMoveDir.x > 0) {
                        moveDir = Vector3.right;
                    } else if (tempMoveDir.x < 0) {
                        moveDir = Vector3.left;
                    }
                    // Debug.Log(moveDir);

                    if (!sound.isPlaying) {
                        sound.Play();
                    } else {
                        if (lastTouchPos == touchPos) {
                            sound.Stop();
                        }
                    }

                    lastTouchPos = touchPos;

                    break;

                case TouchPhase.Ended:
                    // Report that the touch has ended when it ends
                    isMoving = false;

                    if (sound.isPlaying) {
                        sound.Stop();
                    }
                    // lastTouchPos = Vector3.zero;
                    // moveDir = Vector3.zero;
                    break;
            }
        }
    }

    void FixedUpdate()
    {
        float rotationAngle = 12f;
        if (moveDir == Vector3.left) {
            rotationAngle = 168f;
        }
        Quaternion rot = Quaternion.Euler(new Vector3(0, rotationAngle, 0));
        for (int i = 0; i < caughtRigidbodies.Count; i++)
        {
            // Vector3 dir = (transform.position - caughtRigidbodies[i].transform.position).normalized;
            // Quaternion rot = Quaternion.LookRotation(dir);
            caughtRigidbodies[i].transform.rotation = Quaternion.Slerp(caughtRigidbodies[i].transform.rotation, rot, Time.fixedDeltaTime * rotationSpeed);
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
                // r.transform.rotation = Quaternion.identity;
                caughtRigidbodies.Remove(r);
            }
        }
    }
}
