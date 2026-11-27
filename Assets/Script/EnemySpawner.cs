using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public GameObject enemyPrefab;
    public Transform playerTransform;
    public float spawnInterval = 2f;    // Muncul setiap 2 detik
    public float spawnRadius = 11f;     // Jarak spawn di luar batas layar kamera

    private float timer;

    void Update()
    {
        if (enemyPrefab == null || playerTransform == null || !playerTransform.gameObject.activeInHierarchy)
            return;

        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    void SpawnEnemy()
    {
        // Pilih sudut acak 360 derajat di sekeliling player
        float randomAngle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        Vector2 spawnOffset = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle)) * spawnRadius;
        Vector3 spawnPosition = playerTransform.position + (Vector3)spawnOffset;

        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}