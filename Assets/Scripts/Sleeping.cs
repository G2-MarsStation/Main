using UnityEngine;

public class Sleeping : MonoBehaviour
{
    public CicloDiaNoite cicloDiaNoite;
    public KeyCode dormirKey = KeyCode.E;
    [SerializeField] SleepFadeUI sleepFade;

    private bool jogadorPerto = false;

    private ChecklistUIManager checklist;

    void Start()
    {
        if (cicloDiaNoite == null)
        {
            cicloDiaNoite = FindFirstObjectByType<CicloDiaNoite>();
        }

        checklist = FindObjectOfType<ChecklistUIManager>();

    }

    void Update()
    {
        if (jogadorPerto && Input.GetKeyDown(dormirKey))
        {
            if (cicloDiaNoite != null && cicloDiaNoite.jaMudouParaNoite)
            {
                cicloDiaNoite.VoltarParaDia();


                Debug.Log("Você dormiu. Um novo dia começou!");
                sleepFade.TriggerSleepUI();

                checklist?.MarcarTarefaDormir();
            }
            else
            {
                Debug.Log("Ainda não está de noite para dormir.");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jogadorPerto = true;
            Debug.Log("Pressione 'E' para dormir.");
            // Aqui você pode adicionar uma UI que aparece escrito: "Pressione E para dormir"
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jogadorPerto = false;
            Debug.Log("Saiu da cama.");
            // Aqui você pode remover a UI se tiver
        }
    }
}
