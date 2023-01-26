using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{

    public GameObject itemPrefab;
    public float zPos = 0f;

    public float gap = .1f;

    int columns, rows;
    List<GameObject> items = new List<GameObject>();

    void Start()
    {
        float screenAspect = (float) Screen.width / (float) Screen.height;
        float camHalfHeight = Camera.main.orthographicSize;
        float camHalfWidth = screenAspect * camHalfHeight;
        float camWidth = 2.0f * camHalfWidth;

        MeshRenderer s = itemPrefab.GetComponent<MeshRenderer>();

        float w = s.bounds.size.x + gap;
        float h = s.bounds.size.y + gap;
        columns = (int)Mathf.Ceil(camWidth / w);
        rows = (int)Mathf.Ceil((Camera.main.orthographicSize * 2) / h);
        Quaternion rot = Quaternion.Euler(new Vector3(0, 8f, 0));

        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                float x = i * (w ) - camHalfWidth + (w / 2);
                float y = j * (h) - camHalfHeight + (h / 2);
                Vector3 pos = new Vector3(x, y, zPos);
                GameObject l = Instantiate(itemPrefab, pos, rot);
                items.Add(l);
            }
        }
    }

}
