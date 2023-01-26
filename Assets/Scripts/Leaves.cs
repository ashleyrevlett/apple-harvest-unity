using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

public class Leaves : MonoBehaviour
{

    public GameObject leaf;
    public GameObject effectorPrefab;

    GameObject _effector;
    int columns, rows;
    List<GameObject> leafCollection = new List<GameObject>();

    void Start()
    {
        float screenAspect = (float) Screen.width / (float) Screen.height;
        float camHalfHeight = Camera.main.orthographicSize;
        float camHalfWidth = screenAspect * camHalfHeight;
        float camWidth = 2.0f * camHalfWidth;

        SpriteRenderer s = leaf.GetComponent<SpriteRenderer>();
        float w = s.bounds.size.x;
        float h = s.bounds.size.y * .75f;
        columns = (int)Mathf.Ceil(camWidth / w);
        rows = (int)Mathf.Ceil((Camera.main.orthographicSize * 2) / h);

        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                float offset = 0;
                if (j % 2 == 0) {
                    offset = w / 2;
                }
                float x = i * (w ) - camHalfWidth + (w / 2) + offset;
                float y = j * (h) - camHalfHeight + (h / 2);
                Vector2 pos = new Vector2(x, y);
                GameObject l = Instantiate(leaf, pos, Quaternion.identity);
                leafCollection.Add(l);
            }
        }
    }


    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.touches[0].position);

            switch (touch.phase)
            {
                //When a touch has first been detected, change the message and record the starting position
                case TouchPhase.Began:
                    if (!_effector) {
                        _effector = Instantiate(effectorPrefab, touchPos, Quaternion.identity);
                    }
                    break;

                //Determine if the touch is a moving touch
                case TouchPhase.Moved:
                    // Determine direction by comparing the current touch position with the initial one
                    if (_effector) {
                        _effector.transform.position = touchPos;
                    }
                    break;

                case TouchPhase.Ended:
                    // Report that the touch has ended when it ends
                    if (_effector) {
                        Object.Destroy(_effector);
                        _effector = null;
                    }
                    break;
            }
        }
    }
}
