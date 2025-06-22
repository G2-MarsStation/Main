using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ChecklistUIManager : MonoBehaviour
{
    public SoilState soilState;
    public SoilManager soilManager;

    public Image treatCheck;
    public Image plowCheck;
    public Image plantCheck;
    public Image waterCheck;

    public Image sleepCheck;
    public Image water2Check;
    public Image harvestCheck;
    public Image baseCheck;

    public GameObject Tarefas01;
    public GameObject Tarefas02;
    private CanvasGroup tarefa01Group;
    public bool tarefasCompletas = false;

    //public Sprite checkSprite; // o ícone de "check"

    public GameObject fasePulverizar;
    public GameObject faseArar;
    public GameObject fasePlantar;



    void Start()
    {
        tarefa01Group = Tarefas01.GetComponent<CanvasGroup>();
        tarefa01Group.alpha = 1f;
        Tarefas02.SetActive(false);

        treatCheck.gameObject.SetActive(false);
        plowCheck.gameObject.SetActive(false);
        plantCheck.gameObject.SetActive(false);
        waterCheck.gameObject.SetActive(false);
        water2Check.gameObject.SetActive(false);  // inicializa invisível

        sleepCheck.gameObject.SetActive(false);
        harvestCheck.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!tarefa01Concluida)
        {
            // Atualiza os checks do Checklist 1
            treatCheck.gameObject.SetActive(soilManager.AllSoilsTreated());
            plowCheck.gameObject.SetActive(soilManager.AllSoilsPlowed());
            plantCheck.gameObject.SetActive(soilManager.AllSoilsPlanted());
            waterCheck.gameObject.SetActive(soilManager.currentPhase == SoilPhase.Water && soilManager.AllSoilsWatered());

            // Verifica se terminou todas as tarefas do checklist 1
            if (soilManager.AllSoilsTreated() &&
                soilManager.AllSoilsPlowed() &&
                soilManager.AllSoilsPlanted() &&
                (soilManager.currentPhase == SoilPhase.Water && soilManager.AllSoilsWatered()))
            {
                tarefa01Concluida = true;
                StartCoroutine(FadeOutTarefa01());
            }
        }
        else
        {
            // Atualiza os checks do Checklist 2
            sleepCheck.gameObject.SetActive(soilManager.currentPhase == SoilPhase.Water2); // Fica checkado quando avança para Water2 após dormir
            water2Check.gameObject.SetActive(soilManager.currentPhase == SoilPhase.Water2 && soilManager.AllSoilsWatered());
        }
    }

    public void MarcarTarefaDormir()
    {
        sleepCheck.gameObject.SetActive(true);
        Debug.Log("Tarefa de dormir marcada!");
    }

    IEnumerator FadeOutTarefa01()
    {
        Debug.Log("Iniciando fade out...");
        float duration = 1.2f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            tarefa01Group.alpha = Mathf.Lerp(1f, 0f, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        tarefa01Group.alpha = 0f;
        Tarefas01.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        Tarefas02.SetActive(true);
        Debug.Log("Fade concluído.");
    }

    public void MarcarTarefaColheita()
    {
        if (harvestCheck == null)
        {
            Debug.LogWarning("O harvestCheck não está atribuído no inspetor!");
            return;
        }

        harvestCheck.gameObject.SetActive(true);
        Debug.Log("Tarefa de colher marcada!");
    }

    public void MarcarTarefaBase()
    {
        if (baseCheck != null)
        {
            baseCheck.gameObject.SetActive(true);
            Debug.Log("Tarefa de voltar para a base marcada!");
        }
        else
        {
            Debug.LogWarning("BaseCheck não está atribuído no inspetor.");
        }
    }


    [HideInInspector] public bool tarefa01Concluida = false;
}

