using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Stats")]
    public float speed = 12f;
    public float damage = 15f;
    public float waterGrowthBoost = 1.5f; // Setiap tembakan air mempercepat tumbuh 1.5 detik
    public float lifeTime = 2f;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        rb.linearVelocity = transform.right * speed;
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 1. Jika mengenai musuh
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemy = other.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            Destroy(gameObject);
            return;
        }

        // 2. Jika mengenai pohon yang masih dalam masa pertumbuhan
        TreeGrowth tree = other.GetComponent<TreeGrowth>();
        if (tree != null && !tree.isFullyGrown)
        {
            tree.WaterPlant(waterGrowthBoost);
            Destroy(gameObject); // Peluru air terserap oleh tanaman
        }
    }
}