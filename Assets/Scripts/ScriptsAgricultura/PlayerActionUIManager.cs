using UnityEngine;

public class PlayerActionUIManager : MonoBehaviour
{
    public GameObject uiIdle;
    public GameObject uiPulverizar;
    public GameObject uiArar;
    public GameObject uiPlantar;
    public GameObject uiRegar;
    public GameObject uiOxigenio;
    public GameObject uiCama;
    public GameObject uiConsertarPainel;

    public GrabSystem grabSystem; // arrasta seu GrabSystem aqui

    private bool isNearOxygen = false;
    private bool isNearBed = false;
    public bool pertoDoPainel = false;


    void Start()
    {
        ShowIdleUI();
    }

    void Update()
    {
        // Se est� perto do oxig�nio, prioriza essa UI
        if (isNearOxygen)
        {
            ShowOxygenUI();
            return;
        }

        // Se est� perto da cama, prioriza essa UI
        if (isNearBed)
        {
            ShowBedUI();
            return;
        }
        if (pertoDoPainel)
        {
            ShowConsertarPainelUI();
            return;
        }

        // Se est� segurando algo, verifica o que �
        if (grabSystem.IsHoldingSomething())
        {
            GameObject heldObject = grabSystem.GetHeldObject();

            if (heldObject.CompareTag("Pulverizador"))
            {
                ShowPulverizarUI();
            }
            else if (heldObject.CompareTag("Arador"))
            {
                ShowArarUI();
            }
            else if (heldObject.CompareTag("Potato"))
            {
                ShowPlantarUI();
            }
            else if (heldObject.CompareTag("Regador"))
            {
                ShowRegarUI();
            }
            else
            {
                ShowIdleUI();
            }
        }
        else
        {
            // Se n�o segura nada e n�o est� perto de nada, mostra Idle
            ShowIdleUI();
        }
    }

    public void PlayerNearOxygen(bool state)
    {
        isNearOxygen = state;
    }

    public void PlayerNearBed(bool state)
    {
        isNearBed = state;
    }

    // ----- FUN��ES PARA MOSTRAR CADA UI -----

    private void ShowIdleUI()
    {
        EnableOnlyThis(uiIdle);
    }

    private void ShowPulverizarUI()
    {
        EnableOnlyThis(uiPulverizar);
    }

    private void ShowArarUI()
    {
        EnableOnlyThis(uiArar);
    }

    private void ShowPlantarUI()
    {
        EnableOnlyThis(uiPlantar);
    }

    private void ShowRegarUI()
    {
        EnableOnlyThis(uiRegar);
    }

    private void ShowOxygenUI()
    {
        EnableOnlyThis(uiOxigenio);
    }

    private void ShowBedUI()
    {
        EnableOnlyThis(uiCama);
    }

    // ----- M�TODO QUE GARANTE QUE S� UMA UI FICA ATIVA -----

    private void EnableOnlyThis(GameObject activeUI)
    {
        uiIdle.SetActive(activeUI == uiIdle);
        uiPulverizar.SetActive(activeUI == uiPulverizar);
        uiArar.SetActive(activeUI == uiArar);
        uiPlantar.SetActive(activeUI == uiPlantar);
        uiRegar.SetActive(activeUI == uiRegar);
        uiOxigenio.SetActive(activeUI == uiOxigenio);
        uiCama.SetActive(activeUI == uiCama);
        uiConsertarPainel.SetActive(activeUI == uiConsertarPainel);
    }
    private void ShowConsertarPainelUI()
    {
        EnableOnlyThis(uiConsertarPainel);
    }

    public void SetPertoDoPainel(bool estado)
    {
        pertoDoPainel = estado;
    }
}