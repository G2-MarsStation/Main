using UnityEngine;
using System.Collections.Generic;
using UnityEngine.XR;

public class Sleeping : MonoBehaviour
{
    public CicloDiaNoite cicloDiaNoite;
    public KeyCode dormirKey = KeyCode.E;
    [SerializeField] SleepFadeUI sleepFade;

    private bool jogadorPerto = false;

    private ChecklistUIManager checklist;

    public GameObject faseDormir;

    [Header("Selecione o controle (Left, Right)")]
    public InputDeviceCharacteristics controllerCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
    public bool isGrabbingObject = false; // Defina como true quando estiver pegando algo


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

                Debug.Log("Você dormiu. Novo dia começou! Segunda rega ativa.");
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
            faseDormir.SetActive(true);
            Debug.Log("Pressione 'F' para dormir.");
            // Aqui você pode adicionar uma UI que aparece escrito: "Pressione E para dormir"
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jogadorPerto = false;
            faseDormir.SetActive(false);
            Debug.Log("Saiu da cama.");
            // Aqui você pode remover a UI se tiver
        }
    }
}
