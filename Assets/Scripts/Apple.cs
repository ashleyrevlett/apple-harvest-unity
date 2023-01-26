using UnityEngine;

public class Apple : MonoBehaviour
{

    public enum AppleState {
        Unripe,
        Ripe,
        Rotten
    }
    public float growSpeed;
    public AppleState appleState;
    public float timeToRot = 3.0f;
    public Sprite unripeSprite;
    public Sprite ripeSprite;
    public Sprite rottenSprite;

    private AudioSource fallSound;
    private float maxScale = 1.0f;
    private float timer = 0.0f;
    private bool canGrow = true;
    private Vector3 growVector;
    private Vector3 positionOffset;
    private Rigidbody2D body;
    private HingeJoint2D joint;
    private SpriteRenderer spriteRenderer;
    private GameController gameController;

    void Start()
    {
        // cache objs
        body = gameObject.transform.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        joint = gameObject.GetComponent<HingeJoint2D>();
        fallSound = gameObject.GetComponent<AudioSource>();

        GameObject gc =  GameObject.FindWithTag("GameController");
        gameController = gc.GetComponent<GameController>();

        // compute change vectors
        growVector = new Vector3(growSpeed, growSpeed, growSpeed);

        // start small and gravity-free
        appleState = AppleState.Unripe;
        gameObject.transform.localScale = new Vector3(0.01f, .01f, .01f);
    }

    void Update()
    {

        switch (appleState)
        {
            case AppleState.Unripe:
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
                if (joint) {
                    Object.Destroy(joint);
                    fallSound.Play();
                }
                break;
        }
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Floor") {
            fallSound.Stop();
        }
    }
}
