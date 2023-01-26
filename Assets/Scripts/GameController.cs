using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public GameObject apple;
    public int totalApples;
    public float minTimeBetweenEvents = 0.5f;
    public float maxTimeBetweenEvents = 2.0f;
    public GameObject particles;
    public AudioSource sound;
    public int playerScore = 0;

    private Vector2 sprite;
    private float nextEvent = 2.0f;
    private float eventTimer = 0f;

    void Start()
    {
        sprite = apple.GetComponent<SpriteRenderer>().bounds.size;
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

        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
            RaycastHit2D hit = Physics2D.Raycast(touchPos, Vector2.zero);

            if (hit.collider != null)
            {
                if (hit.transform.gameObject.tag == "Apple") {
                    Apple apple = hit.transform.gameObject.GetComponent<Apple>();
                    Debug.Log(apple.appleState);

                    if (apple.appleState == Apple.AppleState.Ripe) {
                        GameObject particle = Instantiate(particles, hit.transform.position, Quaternion.identity);
                        particle.GetComponent<ParticleSystem>().Play();
                        sound.Play();
                        Object.Destroy(hit.transform.gameObject);
                        playerScore += 1;
                    }
                }

            }
        }

    }


    public void addApple() {

        float spawnX = Random.Range
            (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x + (sprite.x / 2), Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x - (sprite.x / 2));
        float spawnY = Random.Range
            (Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height * 0.3f)).y + (sprite.y / 2), Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y - (sprite.y / 2));

        Vector2 spawnPosition = new Vector2(spawnX, spawnY);
        Instantiate(apple, spawnPosition, Quaternion.identity);
    }
}
