using UnityEngine;
using UnityEngine.UI;

public class VidaVR : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    public Image healthBar; // barra de vida no HUD

    public bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    void Update()
    {
        // Se quiser, pode atualizar a barra todo frame (ou atualizar só quando mudar a vida)
        UpdateHealthBar();

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
        Debug.Log("Player morreu!");
        // Aqui você pode adicionar lógica para reiniciar o jogo, mostrar tela de morte, etc.
    }
}
