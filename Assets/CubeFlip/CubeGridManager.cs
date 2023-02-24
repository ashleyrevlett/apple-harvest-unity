using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CubeGridManager : MonoBehaviour
{

    public GameObject itemPrefab;
    public float zPos = 0f;
    public float gap = .1f;
    public float inset = 0f;
    int columns, rows;
    List<GameObject> items = new List<GameObject>();

    Quaternion GetRandomRotation() {
        int[] values = new int[] {-270, -180, -90, 0, 90, 180, 270};
        int randomXIndex = Random.Range(0, values.Length);
        int randomYIndex = Random.Range(0, values.Length);
        return Quaternion.Euler(new Vector3(values[randomXIndex], values[randomYIndex], 0));
    }

    void Start()
    {
        MeshRenderer s = itemPrefab.GetComponent<MeshRenderer>();
        float w = s.bounds.size.x + gap;
        float h = s.bounds.size.y + gap;
        float insetInScreen = 0f;
        Debug.Log($"{Screen.width} x {Screen.height}, {inset}: {insetInScreen}");

        float screenAspect = (float) (Screen.width - insetInScreen) / (float) (Screen.height - insetInScreen);
        float camHalfHeight = Camera.main.orthographicSize - inset;
        float camHalfWidth = screenAspect * (camHalfHeight - inset);
        float camWidth = 2.0f * camHalfWidth;
        float camHeight = 2.0f * camHalfHeight;

        columns = (int)Mathf.Floor(camWidth / w);
        rows = (int)Mathf.Floor(camHeight / h);

        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                float x = (i * w) - camHalfWidth + inset + (w/2 + gap);
                float y = (j * h) - camHalfHeight + inset + h;
                Vector3 pos = new Vector3(x, y, zPos);
                Quaternion rot = GetRandomRotation();

                GameObject l = Instantiate(itemPrefab, pos, rot);
                l.name = $"Cube ({i}, {j})";
                items.Add(l);
            }
        }
    }

    public List<GameObject> GetItems() {
        return items;
    }


}
