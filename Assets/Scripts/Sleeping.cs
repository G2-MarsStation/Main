using UnityEngine;
using System.Collections;
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

    private PlayerController playerController; // referencia para controlar movimento

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

        // Tente pegar o PlayerController no jogador
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            playerController = player.GetComponent<PlayerController>();
    }

    void Update()
    {
        if (jogadorPerto && Input.GetKeyDown(dormirKey))
        {
            if (cicloDiaNoite != null && cicloDiaNoite.jaMudouParaNoite)
            {
                // Bloqueia o movimento do player
                if (playerController != null)
                    playerController.canMove = false;

                cicloDiaNoite.VoltarParaDia();
                SoilManager.instance.AvancarParaSegundaRega();

                Debug.Log("Você dormiu. Novo dia começou! Segunda rega ativa.");

                sleepFade.podeFazerFade = true;
                sleepFade.TriggerSleepUI();

                checklist?.MarcarTarefaDormir();

                // Inicia uma coroutine para esperar o fim do fade e liberar o movimento
                StartCoroutine(LiberarMovimentoAposFade());
            }
            else
            {
                Debug.Log("Ainda não está de noite para dormir.");
                sleepFade.podeFazerFade = false;
            }
        }
    }

    private IEnumerator LiberarMovimentoAposFade()
    {
        // Espera o tempo de fade + sleepDuration
        float tempoEspera = sleepFade.fadeDuration * 2 + sleepFade.sleepDuration;
        yield return new WaitForSeconds(tempoEspera);

        // Libera o movimento do player
        if (playerController != null)
            playerController.canMove = true;

        Debug.Log("Player liberado para andar");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jogadorPerto = true;
            faseDormir.SetActive(true);
            Debug.Log("Pressione 'F' para dormir.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jogadorPerto = false;
            faseDormir.SetActive(false);
            Debug.Log("Saiu da cama.");
        }
    }
}
