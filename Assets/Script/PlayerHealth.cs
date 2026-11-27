using UnityEngine;
using UnityEngine.UI; // Wajib ditambahkan untuk mengakses komponen UI Slider

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHP = 100f;
    public float currentHP;

    [Header("UI Reference")]
    public Slider healthSlider;

    [Header("Invincibility Frames")]
    public float damageCooldown = 0.5f;
    private float lastDamageTime;

    void Start()
    {
        currentHP = maxHP;

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHP;
            healthSlider.value = currentHP;
        }
    }

    public void TakeDamage(float amount)
    {
        if (Time.time < lastDamageTime + damageCooldown) return;

        lastDamageTime = Time.time;
        currentHP -= amount;
        currentHP = Mathf.Clamp(currentHP, 0f, maxHP);

        UpdateHealthUI();

        if (currentHP <= 0f)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        currentHP += amount;
        currentHP = Mathf.Clamp(currentHP, 0f, maxHP);
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        if (healthSlider != null)
        {
            healthSlider.value = currentHP;
        }
    }

    private void Die()
    {
        Debug.Log("Game Over - HP Player Habis!");
        gameObject.SetActive(false);
    }
}