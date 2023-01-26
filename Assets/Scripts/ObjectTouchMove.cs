using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

public class ObjectTouchMove : MonoBehaviour
{
    public GameObject objectPrefab;
    public float zPos = -1;
    GameObject _gameObject;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Debug.Log("TOUCH!");
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
            Vector3 objPos = new Vector3(touchPos.x, touchPos.y, zPos);

            switch (touch.phase)
            {
                //When a touch has first been detected, change the message and record the starting position
                case TouchPhase.Began:
                    if (!_gameObject) {
                        _gameObject = Instantiate(objectPrefab, objPos, Quaternion.identity);
                    }
                    break;

                //Determine if the touch is a moving touch
                case TouchPhase.Moved:
                    // Determine direction by comparing the current touch position with the initial one
                    if (_gameObject) {
                        _gameObject.transform.position = objPos;
                    }
                    break;

                case TouchPhase.Ended:
                    // Report that the touch has ended when it ends
                    if (_gameObject) {
                        Object.Destroy(_gameObject);
                        _gameObject = null;
                    }
                    break;
            }
        }
    }
}
