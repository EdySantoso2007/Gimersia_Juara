using UnityEngine;

public class TreeGrowth : MonoBehaviour
{
    [Header("Growth Settings")]
    public float totalGrowTime = 10f;       // Waktu tumbuh normal (10 detik)
    public float startScaleMultiplier = 0.25f; // Ukuran awal benih (25% dari ukuran penuh)
    public bool isFullyGrown = false;

    [Header("Visual Feedback")]
    public Color growingColor = new Color(0.6f, 0.9f, 0.4f); // Hijau muda saat masih kecil
    public Color grownColor = new Color(0.15f, 0.65f, 0.2f); // Hijau tua saat sudah besar

    private float currentGrowTime = 0f;
    private Vector3 targetScale;
    private Vector3 initialScale;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        targetScale = transform.localScale;
        initialScale = targetScale * startScaleMultiplier;

        // Mulai dari ukuran kecil
        transform.localScale = initialScale;
        if (spriteRenderer != null)
        {
            spriteRenderer.color = growingColor;
        }
    }

    void Update()
    {
        if (isFullyGrown) return;

        // Tambah waktu tumbuh secara bertahap setiap detik
        currentGrowTime += Time.deltaTime;
        UpdateGrowthProgress();
    }

    // Dipanggil oleh peluru air saat mengenai tanaman
    public void WaterPlant(float bonusSeconds)
    {
        if (isFullyGrown) return;

        currentGrowTime += bonusSeconds;
        UpdateGrowthProgress();
    }

    private void UpdateGrowthProgress()
    {
        float progress = Mathf.Clamp01(currentGrowTime / totalGrowTime);

        // Perbesar ukuran pohon secara bertahap sesuai persentase progres
        transform.localScale = Vector3.Lerp(initialScale, targetScale, progress);

        if (progress >= 1f)
        {
            isFullyGrown = true;
            transform.localScale = targetScale;

            if (spriteRenderer != null)
            {
                spriteRenderer.color = grownColor;
            }
        }
    }
}