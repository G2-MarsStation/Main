using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthVR : MonoBehaviour
{
    [Header("Configuração de Vida")]
    public float maxHealth = 100f;
    public float currentHealth;

    [Header("UI")]
    public Image healthBar; // barra de vida no HUD VR (se tiver)

    [Header("Status")]
    public bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    void Update()
    {
        if (currentHealth <= 0 && !isDead)
        {
            Die();
        }
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        if (isDead) return;

        currentHealth += amount;
        currentHealth = Mathf.Min(currentHealth, maxHealth);
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = currentHealth / maxHealth;
        }
    }

    void Die()
    {
        isDead = true;
        Debug.Log("Player morreu no VR!");
        // Aqui você pode adicionar lógica para mostrar tela de morte, reiniciar cena, etc.
    }
}

