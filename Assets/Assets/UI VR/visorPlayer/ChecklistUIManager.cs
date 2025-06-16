using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ChecklistUIManager : MonoBehaviour
{
    public SoilState soilState;

    public Image treatCheck;
    public Image plowCheck;
    public Image plantCheck;
    public Image waterCheck;

    public GameObject Tarefas01;
    public GameObject Tarefas02;
    private CanvasGroup tarefa01Group;
    public bool tarefasCompletas = false;

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
    }

    void Update()
    {
        // Atualiza os checks na UI conforme o estado do solo
        treatCheck.gameObject.SetActive(soilState.treatedSoil);
        plowCheck.gameObject.SetActive(soilState.plowedSoil);
        plantCheck.gameObject.SetActive(soilState.plantedSoil);
        waterCheck.gameObject.SetActive(soilState.isWatered);

        // Verifica se todas as tarefas foram completadas
        if (!tarefasCompletas &&
            soilState.treatedSoil &&
            soilState.plowedSoil &&
            soilState.plantedSoil &&
            soilState.isWatered)
        {
            tarefa01Concluida = true;
            tarefasCompletas = true;
            StartCoroutine(FadeOutTarefa01());
        }
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

