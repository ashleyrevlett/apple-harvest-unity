using UnityEngine;

public class Apple : MonoBehaviour
{

    public enum AppleState {
        Unripe,
        Ripe,
        Rotten
    }

    public float growSpeed;
    private float maxScale = 1.0f;

    public float timeToRot = 3.0f;

    public Sprite unripeSprite;
    public Sprite ripeSprite;
    public Sprite rottenSprite;

    private float timer = 0.0f;

    private bool canGrow = true;

    private Vector3 growVector;
    private Vector3 positionOffset;
    private Rigidbody2D body;
    private SpriteRenderer spriteRenderer;
    private AppleState appleState;

    private GameController gameController;


    void Start()
    {
        // cache objs
        body = gameObject.transform.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        GameObject gc =  GameObject.FindWithTag("GameController");
        gameController = gc.GetComponent<GameController>();

        // compute change vectors
        growVector = new Vector3(growSpeed, growSpeed, growSpeed);

        // start small and gravity-free
        appleState = AppleState.Unripe;
        body.gravityScale = 0f;
        gameObject.transform.localScale = new Vector3(0.01f, .01f, .01f);
    }

    void Update()
    {

        switch (appleState)
        {
            case AppleState.Unripe:
                body.gravityScale = 0f;
                spriteRenderer.sprite = unripeSprite;
                if (gameObject.transform.localScale.x < maxScale && canGrow) {
                    gameObject.transform.localScale += growVector * Time.deltaTime;
                } else if (gameObject.transform.localScale.x < maxScale && !canGrow) {
                    appleState = AppleState.Rotten;
                    spriteRenderer.sprite = rottenSprite;
                } else {
                    appleState = AppleState.Ripe;
                    spriteRenderer.sprite = ripeSprite;
                }
                break;

            case AppleState.Ripe:
                timer += Time.deltaTime;
                if (timer >= timeToRot) {
                    appleState = AppleState.Rotten;
                    spriteRenderer.sprite = rottenSprite;
                    timer = 0f;
                }
                break;

            case AppleState.Rotten:
                body.gravityScale = 1f;
                break;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (appleState == AppleState.Unripe && col.gameObject.tag == "Apple") {
            canGrow = false;
        }
    }

    void OnMouseDown() {
        if (appleState == AppleState.Ripe) {
            Object.Destroy(gameObject);
        }
    }
}
