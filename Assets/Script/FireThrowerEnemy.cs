using UnityEngine;

public class FireThrowerEnemy : MonoBehaviour
{
    [Header("Movement & Throw Settings")]
    public float moveSpeed = 2f;
    public float stopDistance = 5f;     // Jarak berhenti untuk mulai melempar api
    public float throwInterval = 3f;    // Melempar api setiap 3 detik
    public GameObject fireballPrefab;

    private Transform targetPlayer;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private float throwTimer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) targetPlayer = playerObj.transform;
    }

    void Update()
    {
        if (targetPlayer == null || !targetPlayer.gameObject.activeInHierarchy) return;

        throwTimer += Time.deltaTime;
        if (throwTimer >= throwInterval)
        {
            ThrowFireball();
            throwTimer = 0f;
        }
    }

    void FixedUpdate()
    {
        if (targetPlayer == null || !targetPlayer.gameObject.activeInHierarchy)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        float distance = Vector2.Distance(transform.position, targetPlayer.position);
        Vector2 direction = (targetPlayer.position - transform.position).normalized;

        if (direction.x > 0) spriteRenderer.flipX = false;
        else if (direction.x < 0) spriteRenderer.flipX = true;

        // Kejar sampai jarak lempar, lalu berhenti untuk melempar
        if (distance > stopDistance)
        {
            rb.linearVelocity = direction * moveSpeed;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    void ThrowFireball()
    {
        if (fireballPrefab == null || targetPlayer == null) return;

        // Targetkan posisi player dengan sedikit variasi acak agar membakar area sekitar
        Vector2 randomOffset = Random.insideUnitCircle * 1.5f;
        Vector3 burnTarget = targetPlayer.position + (Vector3)randomOffset;

        GameObject fb = Instantiate(fireballPrefab, transform.position, Quaternion.identity);
        Fireball fireballScript = fb.GetComponent<Fireball>();
        if (fireballScript != null)
        {
            fireballScript.SetTarget(burnTarget);
        }
    }
}