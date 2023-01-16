using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public enum Direction {
//     Left,
//     Right
// }

public class Pig : MonoBehaviour
{

    private Vector3 moveDir = Vector3.right;

    private Transform trans;

    private float minX;
    private float maxX;

    private Vector2 screenBounds;
    private float objWidth;

    public float moveSpeed = 3.0f;

    void Start()
    {

        trans = gameObject.GetComponent<Transform>();

        Vector2 bounds = gameObject.GetComponent<SpriteRenderer>().size;
        Debug.Log(bounds);
        Vector2 pigPosScreen = new Vector2(Screen.width * .5f, Screen.height * .03f);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(pigPosScreen);
        trans.position = new Vector3(worldPos.x, worldPos.y, 0f);

        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        objWidth = gameObject.GetComponent<SpriteRenderer>().bounds.size.x / 2;

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = trans.position + (moveDir * Time.deltaTime * moveSpeed);
        if (newPos.x <= (screenBounds.x * -1) + objWidth || newPos.x >= screenBounds.x - objWidth) {
            moveDir *= -1;
        } else {
            trans.position = newPos;
        }

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Apple") {
            Object.Destroy(col.gameObject);
        }
    }
}
