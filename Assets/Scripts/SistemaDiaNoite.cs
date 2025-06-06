
using System.Collections;
using UnityEditor.UIElements;
using UnityEngine;

public class DayAndNightSystem : MonoBehaviour
{

    public enum CicloTempo { Dia, Noite }
    public CicloTempo cicloAtual = CicloTempo.Dia;

    public int diaAtual = 1;

    [Header("Tarefas")]
    public bool tarefa1Concluida = false;
    public bool tarefa2Concluida = false;

    [Header("Referências Visuais")] //nao sei pra que serve
    public Light direcionalLuz; // sol ou lua, tanto faz.
    public GameObject sol;
    public GameObject lua;
    public GameObject ceuEstrelado;

    [Header("Configurações de Luz")]
    public Color corLuzDia = Color.white;
    public Color corLuzNoite = new Color(0.1f, 0.1f, 0.3f);
    public float intensidadeDia = 1.0f;
    public float intensidadeNoite = 0.5f;


    [Header("Skybox")]
    public Material skyboxDia;
    public Material skyboxNoite;

    public float duracaoTransicao;

    public GameObject personagem;




    void Start()
    {
        AtualizarCenarioInstantaneo();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void VerificarTarefas()
    {
        if (tarefa1Concluida && tarefa2Concluida)
        {
            MudarParaNoite();
        }
    }

    void MudarParaNoite()
    {
        cicloAtual = CicloTempo.Noite;
        AtualizarCenarioInstantaneo();
        ForcarPersonagemADormir();
    }

    void ForcarPersonagemADormir()
    {
        personagem.GetComponent<PlayerController>().enabled = false;
        personagem.GetComponent<Animator>().SetTrigger("Dormir");

        StartCoroutine(EsperarEDespertar());

    }

    IEnumerator EsperarEDespertar()
    {
        yield return new WaitForSeconds(5f);
        diaAtual++;
        cicloAtual = CicloTempo.Dia;


        //Resetar tarefas
        tarefa1Concluida = false;
        tarefa2Concluida = false;

        StartCoroutine(TransicaoGradual());

        personagem.GetComponent<PlayerController>().enabled = true;

    }

    IEnumerator TransicaoGradual()
    {
        //define o alvo da transição
        Color corInicial = direcionalLuz.color;
        Color corFinal = (cicloAtual == CicloTempo.Dia) ? corLuzDia : corLuzNoite;

        float intensidadeInicial = direcionalLuz.intensity;
        float intensidadeFinal = (cicloAtual == CicloTempo.Dia) ? intensidadeDia : intensidadeNoite;

        Material skyboxFinal = (cicloAtual == CicloTempo.Dia) ? skyboxDia : skyboxNoite;

        float tempo = 0f;

        while (tempo < duracaoTransicao)
        {
            float t = tempo / duracaoTransicao;

            direcionalLuz.color = Color.Lerp(corInicial, corFinal, t);
            direcionalLuz.intensity = Mathf.Lerp(intensidadeInicial, intensidadeFinal, t);

            tempo += Time.deltaTime;
            yield return null;
        }

        //garante que termina exatamente com o valor final
        direcionalLuz.color = corFinal;
        direcionalLuz.intensity = intensidadeFinal;
        RenderSettings.skybox = skyboxFinal;

        AtualizarCenarioInstantaneo();

    }

    void AtualizarCenarioInstantaneo()
    {
        if (cicloAtual == CicloTempo.Dia)
        {
            

            sol.SetActive(true);
            lua.SetActive(false);
            ceuEstrelado.SetActive(false);
            RenderSettings.skybox = skyboxDia;
        }
        else
        {
          

            sol.SetActive(false);
            lua.SetActive(true);
            ceuEstrelado.SetActive(true);
            RenderSettings.skybox = skyboxNoite;
        }
    }
    //chamar essas funçoes quando cada tarefa for concluida
    public void ConcluirTarefa1()
    {
        tarefa1Concluida = true;
        VerificarTarefas();
    }

    public void ConcluirTarefa2()
    {
        tarefa2Concluida = true;
        VerificarTarefas();
    }
}
