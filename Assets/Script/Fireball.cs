using UnityEngine;

public class Fireball : MonoBehaviour
{
    [Header("Fireball Settings")]
    public float speed = 6f;
    public float damage = 15f;
    private Vector3 targetPosition;
    private bool hasTarget = false;

    public void SetTarget(Vector3 target)
    {
        targetPosition = target;
        targetPosition.z = 0f;
        hasTarget = true;
    }

    void Update()
    {
        if (!hasTarget) return;

        // Bergerak menuju titik target di tanah
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Jika sudah sampai di titik target, bakar tanah lalu hancurkan bola api
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            if (GroundBurnManager.Instance != null)
            {
                GroundBurnManager.Instance.BurnGroundAt(targetPosition);
            }
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}