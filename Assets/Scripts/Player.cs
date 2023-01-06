using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private float screenXUnits = 16f;
    [SerializeField] private float screenYUnits = 12f;
    [SerializeField] private Vector2 minV2 = new Vector2(-7.5f, -5.5f);
    [SerializeField] private Vector2 maxV2 = new Vector2(7.5f, 5.5f);

    [SerializeField] float kickPower = 50f;
    [SerializeField] float kickDistance = 2f;
    [SerializeField] float kickDuration = 0.35f;
    float kickCooldown;

    [SerializeField] Sprite kickSprite;
    Sprite defaultSprite;

    [SerializeField] Image kickBar;

    SpriteRenderer spriteRenderer;
    Ball ball;
    Rigidbody2D ballRB;

    float kickCounter;

    private void Start()
    {
        Cursor.visible = false;

        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultSprite = spriteRenderer.sprite;
        
        ball = FindObjectOfType<Ball>();
        ballRB = ball.GetComponent<Rigidbody2D>();

        kickCooldown = FindObjectOfType<GameManager>().GetKickCooldown();
        kickCounter = kickCooldown;
    }

    void Update()
    {
        FollowMouse();

        kickCounter += Time.deltaTime;
        UpdateKickBarUI();
        if (Input.GetMouseButtonDown(0))
        {
            KickBall();
        }
    }

    private void UpdateKickBarUI()
    {
        var fill = Mathf.Clamp((kickCooldown - kickCounter) / kickCooldown, 0f, 1f);
        kickBar.fillAmount = fill;
        kickBar.color = Color.Lerp(Color.clear, Color.white, fill);
    }

    private void FollowMouse()
    {
        float xPos = (Input.mousePosition.x / Screen.width * screenXUnits) - (screenXUnits / 2);
        xPos = Mathf.Clamp(xPos, minV2.x, maxV2.x);
        float yPos = (Input.mousePosition.y / Screen.height * screenYUnits) - (screenYUnits / 2);
        yPos = Mathf.Clamp(yPos, minV2.y, maxV2.y);
        var mousePos = new Vector2(xPos, yPos);

        transform.position = mousePos;
        transform.up = ball.transform.position - transform.position;
    }

    private void KickBall()
    {
        if (kickCounter >= kickCooldown)
        {
            StartCoroutine(PlayKickAnimation());

            var ballPos = ball.transform.position;
            var ballDistance = Vector2.Distance(transform.position, ballPos);

            if (ballDistance <= kickDistance)
            {
                Vector2 dir = (ballPos - transform.position).normalized;
                ballRB.AddForce(dir * kickPower, ForceMode2D.Impulse);
                
                kickCounter = 0f;
            }
        }
    }

    private IEnumerator PlayKickAnimation()
    {
        spriteRenderer.sprite = kickSprite;
        yield return new WaitForSeconds(kickDuration);
        spriteRenderer.sprite = defaultSprite;
    }
}
