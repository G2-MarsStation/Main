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
    private bool tarefasCompletas = false;

    void Start()
    {
        tarefa01Group = Tarefas01.GetComponent<CanvasGroup>();
        Tarefas02.SetActive(false); //desativa as tarefas2

        treatCheck.enabled = false;
        plowCheck.enabled = false;
        plantCheck.enabled = false;
        waterCheck.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (soilState.treatedSoil)
            treatCheck.enabled = true;

        if (soilState.plowedSoil)
            plowCheck.enabled = true;

        if (soilState.plantedSoil)
            plantCheck.enabled = true;

        if (soilState.isWatered)
            waterCheck.enabled = true;

        //verifica tarefas
        if(!tarefasCompletas &&
            soilState.treatedSoil &&
            soilState.plowedSoil &&
            soilState.plantedSoil &&
            soilState.isWatered)
        {
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
            float alpha = Mathf.Lerp(1f, 0f, elapsed / duration);
            tarefa01Group.alpha = alpha;
            elapsed += Time.deltaTime;
            yield return null;
        }

        tarefa01Group.alpha = 0f;
        Tarefas01.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        Tarefas02.SetActive(true);
        
    Debug.Log("Fade concluído.");
    }
}
