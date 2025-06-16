using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SandstormAlertUI : MonoBehaviour
{
    public CanvasGroup alertBarGroup;  // CanvasGroup do painel vermelho (para controlar alpha)
    public TextMeshProUGUI alertText;  // Texto que pisca
    public RectTransform iconTransform; // �cone para girar
    public AudioSource alertAudio;      // �udio da sirene

    public float pulseSpeed = 2f;       // Velocidade do fade pulsante do painel e texto
    public float iconRotationSpeed = 90f; // Graus por segundo para girar �cone

    private bool alertActive = false;
    public GameObject ALERTA;
    public CameraShake cameraShake; // arraste a c�mera com o script CameraShake

    void Update()
    {
        if (alertActive)
        {
            // Faz o �cone girar
            //iconTransform.Rotate(Vector3.forward, -iconRotationSpeed * Time.deltaTime);
        }
    }

    public void ShowAlert()
    {
        if (!alertActive)
        {
            ALERTA.SetActive(true); // ativa o painel, se for necess�rio
            alertActive = true;
            alertAudio.Play();
            StartCoroutine(PulseAlert());
            if (cameraShake != null)
                cameraShake.StartShake(999f); // dura��o longa, para durar enquanto o alerta estiver ativo
        }
    }

    public void HideAlert()
    {
        alertActive = false;
        alertAudio.Stop();
        alertBarGroup.alpha = 0f;

        if (cameraShake != null)
            cameraShake.StopShake();

    }

    IEnumerator PulseAlert()
    {
        while (alertActive)
        {
            // Faz o alpha oscilar entre 0.5 e 1 (pulsante)
            float alpha = 0.5f + 0.5f * Mathf.Sin(Time.time * pulseSpeed * Mathf.PI * 2);
            alertBarGroup.alpha = alpha;
            alertText.alpha = alpha;

            yield return null;
        }

        // Quando parar, garante alpha zero
        alertBarGroup.alpha = 0f;
        alertText.alpha = 0f;
    }
}

