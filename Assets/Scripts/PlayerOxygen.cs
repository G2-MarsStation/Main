using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerOxygen : MonoBehaviour
{
    PlayerController playerController;

    public float maxOxygen = 5f;
    public float currentOxygen = 0f;
    public float oxygenTimer = 0f;
    public float oxygenInterval = 5f;

    public UnityEngine.UI.Image oxygenBar;

    [Header("Tempestade")]
    public Storm storm; // arraste o objeto Storm no inspetor
    public float stormDamageMultiplier = 5f;

    [Header("Alerta de Oxigênio")]
    public AudioSource lowOxygenAudio;            //  AudioSource com o som de alerta
    public float lowOxygenThreshold = 20f;        // porcentagem de oxigênio que ativa o alerta
    private bool isLowOxygenPlaying = false;      // controle para não tocar várias vezes

    void Start()
    {
        playerController = GetComponent<PlayerController>();
        currentOxygen = maxOxygen;
    }

    void Update()
    {
        // Gerenciamento de oxigênio
        if (currentOxygen > 0)
        {
            oxygenTimer -= Time.deltaTime;
            if (oxygenTimer <= 0)
            {
                currentOxygen -= 1;
                currentOxygen = Mathf.Max(currentOxygen, 0);
                oxygenTimer = oxygenInterval;
            }
        }
        else
        {
            float dano = 1f;

            if (storm != null && storm.IsStormActive())
            {
                dano *= stormDamageMultiplier;
            }

            playerController.currentHealth -= dano * Time.deltaTime;
        }

        // Atualiza UI da barra de oxigênio
        if (oxygenBar != null)
        {
            oxygenBar.fillAmount = currentOxygen / maxOxygen;
        }

        //  porcentagem de oxigênio para tocar alerta
        float oxygenPercent = (currentOxygen / maxOxygen) * 100f;

        if (oxygenPercent < lowOxygenThreshold)
        {
            if (!isLowOxygenPlaying && lowOxygenAudio != null)
            {
                lowOxygenAudio.Play();
                isLowOxygenPlaying = true;
            }
        }
        else
        {
            if (isLowOxygenPlaying && lowOxygenAudio != null)
            {
                lowOxygenAudio.Stop();
                isLowOxygenPlaying = false;
            }
        }
    }
}
