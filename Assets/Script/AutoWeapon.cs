using UnityEngine;
using UnityEngine.InputSystem;

public class AutoWeapon : MonoBehaviour
{
    [Header("Weapon Settings")]
    public GameObject bulletPrefab;
    public Transform firePoint;         // Titik keluarnya peluru
    public float fireInterval = 0.5f;   // Menembak otomatis setiap 0.5 detik

    private Camera mainCam;
    private float fireTimer;

    void Start()
    {
        mainCam = Camera.main;
    }

    void Update()
    {
        AimAtMouse();
        HandleAutoFire();
    }

    private void AimAtMouse()
    {
        if (Mouse.current == null || mainCam == null) return;

        // Ambil posisi mouse di layar menggunakan New Input System
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPos = mainCam.ScreenToWorldPoint(new Vector3(mouseScreenPos.x, mouseScreenPos.y, 0f));
        mouseWorldPos.z = 0f;

        // Hitung arah dari Player ke kursor mouse lalu putar pivot senjata
        Vector2 aimDirection = (mouseWorldPos - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void HandleAutoFire()
    {
        if (bulletPrefab == null || firePoint == null) return;

        fireTimer += Time.deltaTime;
        if (fireTimer >= fireInterval)
        {
            Shoot();
            fireTimer = 0f;
        }
    }

    private void Shoot()
    {
        // Spawn peluru dengan posisi dan sudut rotasi yang sama dengan firePoint
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}