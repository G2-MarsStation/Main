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
            // Verifica se a tarefa de colheita j� foi feita
            if (checklistUI != null && checklistUI.harvestCheck != null && checklistUI.harvestCheck.gameObject.activeSelf)
            {
                jaAtivou = true;

                // Marca na UI que voltou pra base
                checklistUI.MarcarTarefaBase();
                Debug.Log("Tarefa da base marcada!");

                // Toca o �udio final
                if (audioFinal != null)
                {
                    audioFinal.Play();
                    Debug.Log("�udio final iniciado.");

                    // Espera o �udio acabar pra carregar a cena final
                    Invoke(nameof(CarregarCenaFinal), audioFinal.clip.length);
                }
                else
                {
                    Debug.LogWarning("�udio final n�o est� atribu�do.");
                    CarregarCenaFinal();
                }
            }
            else
            {
                Debug.Log("Voc� precisa colher a batata antes de voltar para a base!");
            }
        }
    }

    void CarregarCenaFinal()
    {
        Debug.Log("Carregando cena final...");
        SceneManager.LoadScene(cenaFinal);
    }
}