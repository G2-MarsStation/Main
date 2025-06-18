using UnityEngine;

public class Sleeping : MonoBehaviour
{
    public CicloDiaNoite cicloDiaNoite;
    public KeyCode dormirKey = KeyCode.E;
    [SerializeField] SleepFadeUI sleepFade;

    private bool jogadorPerto = false;

    private ChecklistUIManager checklist;

    public GameObject faseDormir;

    void Start()
    {
        if (cicloDiaNoite == null)
        {
            cicloDiaNoite = FindFirstObjectByType<CicloDiaNoite>();
        }

        checklist = FindObjectOfType<ChecklistUIManager>();

        faseDormir.SetActive(false);

    }

    void Update()
    {
        if (jogadorPerto && Input.GetKeyDown(dormirKey))
        {
            if (cicloDiaNoite != null && cicloDiaNoite.jaMudouParaNoite)
            {
                cicloDiaNoite.VoltarParaDia();
                SoilManager.instance.AvancarParaSegundaRega();

                Debug.Log("Voc� dormiu. Novo dia come�ou! Segunda rega ativa.");
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
            faseDormir.SetActive(true);
            Debug.Log("Pressione 'F' para dormir.");
            // Aqui voc� pode adicionar uma UI que aparece escrito: "Pressione E para dormir"
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jogadorPerto = false;
            faseDormir.SetActive(false);
            Debug.Log("Saiu da cama.");
            // Aqui voc� pode remover a UI se tiver
        }
    }
}
