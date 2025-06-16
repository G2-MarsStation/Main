using UnityEngine;
using System.Collections;

public class CicloDiaNoite : MonoBehaviour
{
    [Header("Skyboxes")]
    public Material skyboxDia;
    public Material skyboxNoite;

    [Header("Luz Direcional (Sol)")]
    public Light directionalLight;
    public Vector3 sunRotationDia = new Vector3(50f, 0f, 0f);
    public Vector3 sunRotationNoite = new Vector3(-30f, 0f, 0f);

    [Header("Intensidade de luz")]
    public float intensidadeDia = 1f;
    public float intensidadeNoite = 0.2f;

    [Header("Referência do Checklist (Manager)")]
    public ChecklistUIManager checklist;

    private bool jaMudouParaNoite = false;
    private bool trocandoSkybox = false;

    void Start()
    {
        // Começa de dia
        RenderSettings.skybox = skyboxDia;
        AjustarLuz(sunRotationDia, intensidadeDia);
        DynamicGI.UpdateEnvironment();
    }

    void Update()
    {
        VerificarConclusaoDasTarefas();
    }

    void VerificarConclusaoDasTarefas()
    {
        if (!jaMudouParaNoite && checklist != null)
        {
            if (checklist.tarefa01Concluida)
            {
                Debug.Log(" Tarefa 01 concluída  mudando para noite!");
                IniciarNoite();
                jaMudouParaNoite = true;
            }
        }
    }

    void IniciarNoite()
    {
        StartCoroutine(TrocarSkyboxComFade(skyboxNoite));
        AjustarLuz(sunRotationNoite, intensidadeNoite);
    }

    void AjustarLuz(Vector3 rotacao, float intensidade)
    {
        if (directionalLight != null)
        {
            directionalLight.transform.rotation = Quaternion.Euler(rotacao);
            directionalLight.intensity = intensidade;
        }
    }

    IEnumerator TrocarSkyboxComFade(Material novaSkybox)
    {
        if (trocandoSkybox) yield break;
        trocandoSkybox = true;

        float duracaoFade = 5f;
        float tempo = 0f;

        // Cria cópia temporária do skybox atual
        Material skyboxAntigo = new Material(RenderSettings.skybox);

        // Fade-out do skybox atual
        while (tempo < duracaoFade)
        {
            tempo += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, tempo / duracaoFade);
            if (skyboxAntigo.HasProperty("_Exposure"))
                skyboxAntigo.SetFloat("_Exposure", alpha);

            RenderSettings.skybox = skyboxAntigo;
            DynamicGI.UpdateEnvironment();
            yield return null;
        }

        // Troca para o novo skybox (noite)
        RenderSettings.skybox = novaSkybox;
        DynamicGI.UpdateEnvironment();

        tempo = 0f;
        // Fade-in do novo skybox
        while (tempo < duracaoFade)
        {
            tempo += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, tempo / duracaoFade);
            if (novaSkybox.HasProperty("_Exposure"))
                novaSkybox.SetFloat("_Exposure", alpha);

            RenderSettings.skybox = novaSkybox;
            DynamicGI.UpdateEnvironment();
            yield return null;
        }

        trocandoSkybox = false;
    }
}
