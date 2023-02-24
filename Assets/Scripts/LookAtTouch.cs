using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTouch : MonoBehaviour
{
    public float gap = .1f;
    public GameObject itemPrefab;
    // public float speed = 1f;

    float depth = 10f;
    int columns, rows;
    List<GameObject> items = new List<GameObject>();
    Plane plane;

    void Start()
    {
        // get size of prefab
        MeshRenderer s = itemPrefab.GetComponent<MeshRenderer>();
        float w = s.bounds.size.x + gap;
        float h = s.bounds.size.y + gap;

        // get size of view frustrum at given depth from camera
        float halfFieldOfView = Camera.main.fieldOfView * 0.5f * Mathf.Deg2Rad;
        float halfHeightAtDepth = depth * Mathf.Tan(halfFieldOfView);
        float halfWidthAtDepth = Camera.main.aspect * halfHeightAtDepth;

        // how many prefabs will fit in view
        columns = (int)Mathf.Floor((halfWidthAtDepth * 2f) / w) + 1;
        rows = (int)Mathf.Floor((halfHeightAtDepth * 2f) / h) + 1;
        Debug.Log($"{halfFieldOfView}, {halfHeightAtDepth}, {halfWidthAtDepth}; {columns} x {rows}");

        // lay them out
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                float x = (i * w) - halfWidthAtDepth + (w / 2f);
                float y = (j * h) - halfHeightAtDepth + (h);
                Vector3 pos = new Vector3(x, y, 0f);
                GameObject l = Instantiate(itemPrefab, pos, Quaternion.identity);
                items.Add(l);
            }
        }

        // needed for raycasting touch position in 3d
        plane = new Plane(-Vector3.forward, new Vector3(0, 0, -1f));
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            // create ray from the camera and passing through the touch position
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            float distance = 0; // return the distance from the camera
            if (plane.Raycast(ray, out distance)){ // if plane
                Vector3 pos = ray.GetPoint(distance); // get the point in the plane you've touched
                foreach (GameObject item in items)
                {
                    // item.transform.LookAt(pos);
                    LookAtLerp script = item.GetComponent<LookAtLerp>();
                    script.lookPosition = pos;
                    // Vector3 direction = pos - item.transform.position;
                    // Quaternion toRotation = Quaternion.FromToRotation(item.transform.forward, direction);
                    // item.transform.rotation = Quaternion.Lerp(item.transform.rotation, toRotation, speed * Time.deltaTime);
                }
            }

        }

    }
}
