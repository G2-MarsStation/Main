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


                Debug.Log("Voc� dormiu. Um novo dia come�ou!");
                sleepFade.TriggerSleepUI();

                checklist?.MarcarTarefaDormir();
            }
            else
            {
                Debug.Log("Ainda n�o est� de noite para dormir.");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jogadorPerto = true;
            Debug.Log("Pressione 'E' para dormir.");
            // Aqui voc� pode adicionar uma UI que aparece escrito: "Pressione E para dormir"
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jogadorPerto = false;
            Debug.Log("Saiu da cama.");
            // Aqui voc� pode remover a UI se tiver
        }
    }
}
