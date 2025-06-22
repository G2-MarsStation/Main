using UnityEngine;

public class SoilManager : MonoBehaviour
{
    public static SoilManager instance;
    private SoilState[] allSoils;
    public SoilPhase currentPhase = SoilPhase.ApplyProduct;

    public PainelSolarManager painelSolarManager;

    public bool painelEventoAtivo = false;
    public bool painelEventoConcluido = false;

    private void Awake()
    {
        instance = this;
        allSoils = FindObjectsOfType<SoilState>();
    }

    //Vai ser o cara que vai avançar as etapas de agricultura
    void Update()
    {
        if (currentPhase == SoilPhase.ApplyProduct && AllSoilsTreated())
        {
            currentPhase = SoilPhase.Plow;
        }


        if (currentPhase == SoilPhase.Plow && AllSoilsPlowed())
        {
            currentPhase = SoilPhase.Plant;
        }
        if (currentPhase == SoilPhase.Plant && AllSoilsPlanted())
        {
            currentPhase = SoilPhase.Water;
            Debug.Log("Fase avançada para: Regar");
        }
        if (currentPhase == SoilPhase.Water && AllSoilsWatered())
        {
            Debug.Log("Concluido");
        }
        if (currentPhase == SoilPhase.Water2 && AllSoilsWatered() && !painelEventoAtivo && !painelEventoConcluido)
        {
            Debug.Log("SEGUNDA REGA COMPLETA! Fase finalizada.");
            painelEventoAtivo = true;
            painelSolarManager.FicarTortoTodos();
        }
        if (painelEventoAtivo && painelSolarManager.TodosPaineisConsertados())
        {
            painelEventoAtivo = false;
            painelEventoConcluido = true;
            Debug.Log("Painéis consertados! Você pode continuar.");

            // Aqui você decide se quer avançar para outra fase ou finalizar
            Debug.Log("Evento dos painéis finalizado. Jogador pode prosseguir.");
        }
    }

    public bool AllSoilsTreated()
    {
        foreach (var soil in allSoils)
        {
            if (!soil.treatedSoil) return false;
        } return true;
    } 

    public bool AllSoilsPlowed()
    {
        foreach(var soil in allSoils)
        {
            if (!soil.plowedSoil) return false;
        } return true;
    }

    public bool AllSoilsPlanted()
    {
        foreach (var soil in allSoils)
        {
            if (!soil.plantedSoil) return false;
        }
        return true;
    }

    public bool AllSoilsWatered()
    {
        foreach (var soil in allSoils)
        {
            if (!soil.isWatered) return false;
        }
        return true;
    }
    public void AvancarParaSegundaRega()
    {
        currentPhase = SoilPhase.Water2;
        ResetWaterState();
        Debug.Log("Novo dia! Avançou para fase de REGAR 2.");
    }

    private void ResetWaterState()
    {
        foreach (var soil in allSoils)
        {
            soil.isWatered = false;
        }
    }


}
