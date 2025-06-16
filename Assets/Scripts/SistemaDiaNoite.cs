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
    public float intensidadeNoite = 1.2f;

    public GameObject personagem;




    void Start()
    {
        AtualizarCenario();
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
        AtualizarCenario();
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

        AtualizarCenario();

        personagem.GetComponent<PlayerController>().enabled = true;

    }

    void AtualizarCenario()
    {
        if (cicloAtual == CicloTempo.Dia)
        {
            direcionalLuz.color = corLuzDia;
            direcionalLuz.intensity = intensidadeDia;

            sol.SetActive(true);
            lua.SetActive(false);
            ceuEstrelado.SetActive(false);
        }
        else
        {
            direcionalLuz.color = corLuzNoite;
            direcionalLuz.intensity = intensidadeNoite;

            sol.SetActive(false);
            lua.SetActive(true);
            ceuEstrelado.SetActive(true);
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
