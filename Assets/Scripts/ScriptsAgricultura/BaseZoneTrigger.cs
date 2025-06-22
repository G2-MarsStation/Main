using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseZoneTrigger : MonoBehaviour
{
    public ChecklistUIManager checklistUI;
    public AudioSource audioFinal;
    public string cenaFinal;

    private bool jaAtivou = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !jaAtivou)
        {
            // Verifica se a tarefa de colheita já foi feita
            if (checklistUI != null && checklistUI.harvestCheck != null && checklistUI.harvestCheck.gameObject.activeSelf)
            {
                jaAtivou = true;

                // Marca na UI que voltou pra base
                checklistUI.MarcarTarefaBase();
                Debug.Log("Tarefa da base marcada!");

                // Toca o áudio final
                if (audioFinal != null)
                {
                    audioFinal.Play();
                    Debug.Log("Áudio final iniciado.");

                    // Espera o áudio acabar pra carregar a cena final
                    Invoke(nameof(CarregarCenaFinal), audioFinal.clip.length);
                }
                else
                {
                    Debug.LogWarning("Áudio final não está atribuído.");
                    CarregarCenaFinal();
                }
            }
            else
            {
                Debug.Log("Você precisa colher a batata antes de voltar para a base!");
            }
        }
    }

    void CarregarCenaFinal()
    {
        Debug.Log("Carregando cena final...");
        SceneManager.LoadScene(cenaFinal);
    }
}