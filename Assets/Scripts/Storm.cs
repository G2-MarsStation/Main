using System.Collections;
using UnityEngine;

public class Storm : MonoBehaviour
{
    public ParticleSystem sandstormParticles;
    //public AudioSource sandstormSound;
    public float duration = 20f;
    public float delayBetweenStorms = 10f;

    private bool stormActive = false;

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
        if(sandstormParticles != null)
        {
            sandstormParticles.Play();
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
        RenderSettings.fog = false;

        Debug.Log("paro a tempestade");
    }
}
