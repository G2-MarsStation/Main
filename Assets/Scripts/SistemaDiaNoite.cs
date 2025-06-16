using System.Collections;
using UnityEngine;

public class SistemaDiaNoite : MonoBehaviour
{
    public enum CicloTempo { Dia, Noite }
    public CicloTempo cicloAtual = CicloTempo.Dia;

    public int diaAtual = 1;

    [Header("Tarefas")]
    public bool tarefa01Concluida = false;
    public bool tarefa02Concluida = false;

    [Header("Referências Visuais")]
    public Light direcionalLuz;
    public GameObject sol;
    public GameObject lua;
    public GameObject ceuEstrelado;

    [Header("Configurações de Luz")]
    public Color corLuzDia = Color.white;
    public Color corLuzNoite = new Color(0.1f, 0.1f, 0.3f);
    public float intensidadeDia = 1.0f;
    public float intensidadeNoite = 1.2f;

    [Header("Skyboxes")]
    public Material skyboxDia;
    public Material skyboxNoite;

    [Header("Personagem")]
    public GameObject personagem;

    private PlayerController playerController;
    private Animator personagemAnimator;

    private bool estaDormindo = false;

    void Start()
    {
        // Pega componentes essenciais no Start
        if (personagem != null)
        {
            playerController = personagem.GetComponent<PlayerController>();
            personagemAnimator = personagem.GetComponent<Animator>();
        }
        else
        {
            Debug.LogWarning("Personagem não está atribuído no SistemaDiaNoite.");
        }

        cicloAtual = CicloTempo.Dia;
        AtualizarCenario();
    }

    // Removi o Update vazio

    public void ConcluirTarefa1()
    {
        tarefa01Concluida = true;
        VerificarTarefas();
    }

    public void ConcluirTarefa2()
    {
        tarefa02Concluida = true;
        VerificarTarefas();
    }

    private void VerificarTarefas()
    {
        if (tarefa01Concluida && tarefa02Concluida && cicloAtual == CicloTempo.Dia && !estaDormindo)
        {
            MudarParaNoite();
        }
    }

    private void MudarParaNoite()
    {
        cicloAtual = CicloTempo.Noite;
        AtualizarCenario();
        StartCoroutine(ForcarPersonagemADormir());
    }

    private IEnumerator ForcarPersonagemADormir()
    {
        estaDormindo = true;

        if (playerController != null)
            playerController.enabled = false;

        if (personagemAnimator != null)
            personagemAnimator.SetTrigger("Dormir");

        // Tempo para animação ou efeito de dormir
        yield return new WaitForSeconds(5f);

        diaAtual++;
        cicloAtual = CicloTempo.Dia;

        tarefa01Concluida = false;
        tarefa02Concluida = false;

        AtualizarCenario();

        if (playerController != null)
            playerController.enabled = true;

        estaDormindo = false;
    }

    private void AtualizarCenario()
    {
        if (direcionalLuz == null || sol == null || lua == null || ceuEstrelado == null)
        {
            Debug.LogWarning("Algumas referências visuais não foram atribuídas no SistemaDiaNoite.");
            return;
        }

        if (cicloAtual == CicloTempo.Dia)
        {
            direcionalLuz.color = corLuzDia;
            direcionalLuz.intensity = intensidadeDia;

            sol.SetActive(true);
            lua.SetActive(false);
            ceuEstrelado.SetActive(false);

            if (skyboxDia != null)
                RenderSettings.skybox = skyboxDia;
        }
        else
        {
            direcionalLuz.color = corLuzNoite;
            direcionalLuz.intensity = intensidadeNoite;

            sol.SetActive(false);
            lua.SetActive(true);
            ceuEstrelado.SetActive(true);

            if (skyboxNoite != null)
                RenderSettings.skybox = skyboxNoite;
        }

        DynamicGI.UpdateEnvironment();
    }
}

