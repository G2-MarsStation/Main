using UnityEngine;

public class Storm : MonoBehaviour
{
    public ParticleSystem sandstormParticles;
    public float duration = 20f;

    private bool stormActive = false;

    public SandstormAlertUI alertaUI; // arraste no Inspector


    void Start()
    {
        // N�o inicia automaticamente a tempestade
        // StartCoroutine(StormRoutine());
    }

    // M�todo p�blico para iniciar a tempestade (chamado pelo PainelSolarManager)
    public void StartStorm()
    {
        if (!stormActive)
        {
            stormActive = true;

            if (sandstormParticles != null)
                sandstormParticles.Play();

            RenderSettings.fog = true;
            RenderSettings.fogColor = new Color(0.8f, 0.6f, 0.4f, 1f);
            RenderSettings.fogDensity = 1f;
            RenderSettings.fogStartDistance = 0f;
            RenderSettings.fogEndDistance = 50f;

            // ATIVA ALERTA UI
            if (alertaUI != null)
                alertaUI.ShowAlert();

            Debug.Log("Tempestade iniciada");

            // Opcional: parar a tempestade depois da dura��o
            Invoke(nameof(EndStorm), duration);
        }
    }

    // M�todo para encerrar a tempestade
    public void EndStorm()
    {
        if (stormActive)
        {
            stormActive = false;

            if (sandstormParticles != null)
                sandstormParticles.Stop();

            RenderSettings.fog = false;

            // DESATIVA ALERTA UI
            if (alertaUI != null)
                alertaUI.HideAlert();

            Debug.Log("Tempestade terminou");
        }
    }
}
