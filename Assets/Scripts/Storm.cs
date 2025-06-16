using System.Collections;
using UnityEngine;

public class Storm : MonoBehaviour
{
    public ParticleSystem sandstormParticles;
    public AudioSource stormSound;
    public float duration = 20f;
    public float delayBetweenStorms = 10f;

    private bool stormActive = false;

    public SandstormAlertUI alertUI; // arraste o script da UI no Inspector

    void Start()
    {
        StartCoroutine(StormRoutine());
    }
    IEnumerator StormRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(delayBetweenStorms);
            StartStorm();
            yield return new WaitForSeconds(duration);
            EndStorm();
        }
    }

    void StartStorm()
    {
        stormActive = true;
        stormSound.Play();
        if(sandstormParticles != null)
        {
            sandstormParticles.Play();
        }

        if (alertUI != null)
        {
            alertUI.ShowAlert(); // <- ALERTA VISUAL!
        }
        //perguntar pro chat q porressa
        RenderSettings.fog = true;
        RenderSettings.fogColor = new Color(0.8f, 0.6f, 0.4f, 1f);
        RenderSettings.fogDensity = 1f;
        RenderSettings.fogStartDistance = 0f;
        RenderSettings.fogEndDistance = 50f;

        Debug.Log("tempestade iniciada");
    }

    void EndStorm()
    {
        stormActive = false;
        if (sandstormParticles != null)
        {
            sandstormParticles.Stop();
        }
        stormSound.Stop();

        if (alertUI != null)
        {
            alertUI.HideAlert(); // <- PARA O ALERTA VISUAL
        }

        RenderSettings.fog = false;

        Debug.Log("paro a tempestade");
    }
}
