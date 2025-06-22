using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SleepFadeUI : MonoBehaviour
{
    [Tooltip("Arraste aqui a Image preta dentro do Canvas")]
    public Image fadeImage;

    public float fadeDuration = 2f;
    public float sleepDuration = 2f; // tempo que fica totalmente preto (player dormindo)

    private bool isFading = false;

    // NOVO: flag para controlar se o fade pode ser executado
    public bool podeFazerFade = true;

    void Start()
    {
        if (fadeImage != null)
        {
            Color c = fadeImage.color;
            c.a = 0f; // começa transparente
            fadeImage.color = c;
            fadeImage.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Fade Image não foi atribuída no SleepUIManager.");
        }
    }

    public void TriggerSleepUI()
    {
        // ALTERADO: só começa o fade se estiver permitido (podeFazerFade)
        if (!isFading && fadeImage != null && podeFazerFade)
        {
            StartCoroutine(FadeRoutine());
        }
        else if (!podeFazerFade)
        {
            Debug.Log("Fade bloqueado: não é hora de dormir.");
        }
    }

    private IEnumerator FadeRoutine()
    {
        isFading = true;
        fadeImage.gameObject.SetActive(true);

        // Fade in (de transparente para preto)
        yield return StartCoroutine(FadeColor(0f, 1f));

        // Espera o tempo do "sono"
        yield return new WaitForSeconds(sleepDuration);

        // Fade out (de preto para transparente)
        yield return StartCoroutine(FadeColor(1f, 0f));

        fadeImage.gameObject.SetActive(false);
        isFading = false;
    }

    private IEnumerator FadeColor(float startAlpha, float endAlpha)
    {
        float elapsed = 0f;

        Color c = fadeImage.color;
        c.a = startAlpha;
        fadeImage.color = c;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / fadeDuration);
            c.a = alpha;
            fadeImage.color = c;
            yield return null;
        }

        c.a = endAlpha;
        fadeImage.color = c;
    }
}
