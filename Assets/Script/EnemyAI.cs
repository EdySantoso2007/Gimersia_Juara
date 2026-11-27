using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Stats")]
    public float moveSpeed = 2.5f;
    public float contactDamage = 15f;

    private Transform targetPlayer;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            targetPlayer = playerObj.transform;
        }
    }

    void FixedUpdate()
    {
        if (targetPlayer == null || !targetPlayer.gameObject.activeInHierarchy)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        // Hitung arah menuju player
        Vector2 direction = (targetPlayer.position - transform.position).normalized;
        rb.linearVelocity = direction * moveSpeed;

        // Balik arah sprite mengikuti pergerakan
        if (direction.x > 0)
            spriteRenderer.flipX = false;
        else if (direction.x < 0)
            spriteRenderer.flipX = true;
    }

    // Menyerang saat bersentuhan fisik dengan Player
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(contactDamage);
            }
        }
    }
}