using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieController : MonoBehaviour
{
    public float SWIPE_THRESHOLD = .1f;
    public float rotateTime = .3f;
    float rotateDegrees = 90f;
    bool rotating = false;
    Vector2 magnetStartPosition;

    void Swipe(Vector3 direction) {
        float threshold = .3f;
        Vector3 flipDir = Vector3.zero;
        if (direction.x < threshold * -1) {
            flipDir = Vector3.right;
            Debug.Log("Flip right");
        } else if (direction.x > threshold) {
            flipDir = Vector3.left;
            Debug.Log("Flip left");
        } else if (direction.y < threshold * -1) {
            flipDir = Vector3.up;
            Debug.Log("Flip up");
        } else if (direction.y > threshold) {
            flipDir = Vector3.down;
            Debug.Log("Flip down");
        }
        Flip(flipDir);
    }

    private IEnumerator Rotate(Vector3 rotateAxis, float degrees, float totalTime)
    {
        if (rotating)
            yield return null;
        rotating = true;

        Quaternion startRotation = transform.rotation;
        Vector3 startPosition = transform.position;
        // Get end position;
        transform.RotateAround(transform.position, rotateAxis, degrees);
        Quaternion endRotation = transform.rotation;
        Vector3 endPosition = transform.position;
        transform.rotation = startRotation;
        transform.position = startPosition;

        float rate = degrees / totalTime;
        //Start Rotate
        for (float i = 0.0f; Mathf.Abs(i) < Mathf.Abs(degrees); i += Time.deltaTime * rate)
        {
            transform.RotateAround(transform.position, rotateAxis, Time.deltaTime * rate);
            yield return null;
        }

        transform.rotation = endRotation;
        transform.position = endPosition;
        rotating = false;

        Debug.Log($"Rotated, {GetFaceUpSide()}");
    }

    void Flip(Vector3 direction) {
        /*
        @param direction is normalized vector == right, left, up or down
        flip left = Y +90 deg
        flip right = Y -90 deg
        flip up = X +90 deg
        flip down = X -90 deg
        */
        if (direction == Vector3.left) {
            StartCoroutine(Rotate(Vector3.up, rotateDegrees, rotateTime));
        } else if (direction == Vector3.right) {
            StartCoroutine(Rotate(Vector3.up, -rotateDegrees, rotateTime));
        } else if (direction == Vector3.up) {
            StartCoroutine(Rotate(Vector3.right, rotateDegrees, rotateTime));
        } else if (direction == Vector3.down) {
            StartCoroutine(Rotate(Vector3.right, -rotateDegrees, rotateTime));
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("TouchMagnet")) {
            // Debug.Log("Magnet entered");
            magnetStartPosition = new Vector2(other.gameObject.transform.position.x, other.gameObject.transform.position.y);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("TouchMagnet")) {
            // Debug.Log("Magnet exited");

            Vector2 lastPos = new Vector2(other.gameObject.transform.position.x, other.gameObject.transform.position.y);
            float distance = Vector2.Distance(magnetStartPosition, lastPos);

            // Debug.Log(distance);
            if (distance < SWIPE_THRESHOLD) {
                Debug.Log("Doing flip");
                Flip(Vector3.left);
            } else {
                Vector2 swipeDir = (magnetStartPosition - lastPos).normalized;
                Swipe(swipeDir);
            }
        }

    }

    public int GetFaceUpSide() {
        if (rotating) {
            // still in motion
            return 0;
        }
        float threshold = 1f;
        float angle1 = Vector3.Angle(transform.forward, -Vector3.forward);
        float angle2 = Vector3.Angle(-transform.forward, -Vector3.forward);
        float angle3 = Vector3.Angle(transform.up, -Vector3.forward);
        float angle4 = Vector3.Angle(-transform.up, -Vector3.forward);
        float angle5 = Vector3.Angle(-transform.right, -Vector3.forward);
        float angle6 = Vector3.Angle(transform.right, -Vector3.forward);
        int t;
        if (angle1 <= threshold) {
            // t = transform.up;
            t = 1;
        } else if (angle2 <= threshold) {
            // t = -transform.up;
            t = 6;
        } else if (angle3 <= threshold) {
            // t = transform.right;
            t = 3;
        } else if (angle4 <= threshold) {
            // t = -transform.right;
            t = 4;
        } else if (angle5 <= threshold) {
            // t = transform.forward;
            t = 2;
        } else {
            // t = -transform.forward;
            t = 5;
        }
        // Debug.Log($"{gameObject.name}: {angle1}, {angle2}, {angle3}, {angle4}, {angle5}, {angle6} == {t}");
        return t;
    }

    void Start() {
        // test which side is up
        // Debug.Log(GetFaceUpSide());
    }
}
