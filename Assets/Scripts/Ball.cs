using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private AudioClip[] bounceSounds;

    private Rigidbody2D rb;
    private AudioSource audioSource;
    
    private float speed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        speed = FindObjectOfType<GameManager>().GetBallSpeed();
    }

    void FixedUpdate()
    {
        if (rb.velocity.magnitude < speed-1f || rb.velocity.magnitude > speed+1f)
        {
            rb.velocity = rb.velocity.normalized * speed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var audioClip = bounceSounds[Random.Range(0, bounceSounds.Length)];
        audioSource.PlayOneShot(audioClip);
    }
}
