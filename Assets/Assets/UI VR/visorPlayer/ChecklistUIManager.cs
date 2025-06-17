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

    public GameObject Tarefas01;
    public GameObject Tarefas02;
    private CanvasGroup tarefa01Group;
    public bool tarefasCompletas = false;

    public Sprite checkSprite; // o ícone de "check"


    void Start()
    {
        tarefa01Group = Tarefas01.GetComponent<CanvasGroup>();
        tarefa01Group.alpha = 1f; // garante que esteja visível no início
        Tarefas02.SetActive(false); // desativa tarefas 2 no começo

        // Deixa as imagens de check invisíveis no começo
        treatCheck.gameObject.SetActive(false);
        plowCheck.gameObject.SetActive(false);
        plantCheck.gameObject.SetActive(false);
        waterCheck.gameObject.SetActive(false);

        sleepCheck.gameObject.SetActive(false);

    }

    void Update()
    {
        // Atualiza os checks na UI conforme o estado do solo
        treatCheck.gameObject.SetActive(soilManager.AllSoilsTreated());
        plowCheck.gameObject.SetActive(soilManager.AllSoilsPlowed());
        plantCheck.gameObject.SetActive(soilManager.AllSoilsPlanted());
        waterCheck.gameObject.SetActive(soilManager.AllSoilsWatered());

        // Verifica se todas as tarefas foram completadas
        if (!tarefasCompletas &&
            soilManager.AllSoilsTreated() &&
            soilManager.AllSoilsPlowed() &&
            soilManager.AllSoilsPlanted() &&
            soilManager.AllSoilsWatered())
        {
            tarefa01Concluida = true;
            tarefasCompletas = true;
            StartCoroutine(FadeOutTarefa01());
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

    [HideInInspector] public bool tarefa01Concluida = false;
}

