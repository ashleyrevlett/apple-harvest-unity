using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GridManager : MonoBehaviour
{

    public GameObject itemPrefab;
    public float zPos = 0f;

    public float gap = .1f;

    public float inset = 0f;

    int columns, rows;
    List<GameObject> items = new List<GameObject>();

    void Start()
    {
        MeshRenderer s = itemPrefab.GetComponent<MeshRenderer>();
        float w = s.bounds.size.x + gap;
        float h = s.bounds.size.y + gap;
        // inset = w * 2f;

        // Vector3 insetVector = Camera.main.WorldToScreenPoint(new Vector3(inset, 0f, 0f));
        // float insetInScreen = Mathf.Abs((Screen.width / 2f) - insetVector.x);
        float insetInScreen = 0f;
        // Debug.Log($"{Screen.width} x {Screen.height}, {inset}: {insetInScreen}");

        float screenAspect = (float) (Screen.width - insetInScreen) / (float) (Screen.height - insetInScreen);
        float camHalfHeight = Camera.main.orthographicSize - inset;
        float camHalfWidth = screenAspect * (camHalfHeight - inset);
        float camWidth = 2.0f * camHalfWidth;
        float camHeight = 2.0f * camHalfHeight;

        columns = (int)Mathf.Floor(camWidth / w);
        rows = (int)Mathf.Floor(camHeight / h);
        Quaternion rot = Quaternion.Euler(new Vector3(0, 12f, 0));

        float yTile = (1f / rows) / screenAspect;
        float xTile = yTile / screenAspect;
        Vector2 tiling = new Vector2(xTile, yTile);

        Debug.Log($"{camWidth}x{camHeight}, {screenAspect}%, {columns}x{rows}, tiling: ({tiling.x}, {tiling.y}, size: {w} x {h})");

        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                float x = (i * w) - camHalfWidth + inset - (w);
                float y = (j * h) - camHalfHeight + inset - (h * 1.5f);
                // float x+ (i * (w) - camHalfWidth + (w / 2));
                // float y = inset + (j * (h) - camHalfHeight + (h / 2));
                Vector3 pos = new Vector3(x, y, zPos);

                GameObject l = Instantiate(itemPrefab, pos, rot);
                l.name = $"Sequin ({i}, {j})";
                // Debug.Log(pos);
                foreach(var material in l.GetComponent<Renderer>().materials) {
                    // Debug.Log(pos);
                    if (material.name == "Material.Back (Instance)") {
                        // float xOffset = (((x + camHalfWidth)) / camWidth) - (tiling.x);
                        float xOffset = ((x - (w / 2f) + camHalfWidth) / camWidth);
                        float yOffset = ((y - (h * screenAspect) + camHalfHeight) / camHeight);
                        // float xOffset = ((x + camHalfWidth) * .5f / camWidth) + textureOffset;
                        // float yOffset = ((y + camHalfHeight) * .5f / camHeight) + textureOffset;
                        material.mainTextureOffset = new Vector2(xOffset, yOffset);
                        material.mainTextureScale = tiling;
                    }

                }
                items.Add(l);

                // Renderer rend = l.GetComponent<Renderer> ();
                // rend.material.mainTextureOffset = new Vector2(xOffset, 0);
                // Debug.Log(rend.material.name);
                // Debug.Log(rend.material.mainTextureOffset);

            }
        }
    }

}
