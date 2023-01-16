using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public GameObject apple;
    public int totalApples;
    private Vector2 s;

    public float minTimeBetweenEvents = 0.5f;
    public float maxTimeBetweenEvents = 2.0f;

    private float nextEvent = 2.0f;

    private float eventTimer = 0f;

    void Start()
    {
        s = apple.GetComponent<SpriteRenderer>().bounds.size;
        Debug.Log(s);

        // spriteSizeInScreen = Camera.main.WorldToScreenPoint(s);
        // Debug.Log(spriteSizeInScreen);

        // for (int i = 0; i < totalApples; i++) {
        //     addApple();
        // }
    }

    void Update()
    {
        if (eventTimer < nextEvent) {
            eventTimer += Time.deltaTime;
        } else {
            eventTimer = 0f;
            nextEvent = Random.Range(minTimeBetweenEvents, maxTimeBetweenEvents);
            addApple();
        }

    }


    public void addApple() {

        float spawnX = Random.Range
            (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x + (s.x / 2), Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x - (s.x / 2));
        float spawnY = Random.Range
            (Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height * 0.3f)).y + (s.y / 2), Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y - (s.y / 2));

        // Debug.Log($"X: {spawnX}, Y: {spawnY}");

        Vector2 spawnPosition = new Vector2(spawnX, spawnY);
        Instantiate(apple, spawnPosition, Quaternion.identity);
    }
}
