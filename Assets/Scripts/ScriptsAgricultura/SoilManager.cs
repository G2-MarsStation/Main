using UnityEngine;

public class SoilManager : MonoBehaviour
{
    public static SoilManager instance;
    private SoilState[] allSoils;
    public SoilPhase currentPhase = SoilPhase.ApplyProduct;

    private void Awake()
    {
        instance = this;
        allSoils = FindObjectsOfType<SoilState>();
    }

    //Vai ser o cara que vai avançar as etapas de agricultura
    void Update()
    {       
        if(currentPhase == SoilPhase.ApplyProduct && AllSoilsTreated())
        {
            currentPhase = SoilPhase.Plow;
        }
        if(currentPhase == SoilPhase.Plow && AllSoilsPlowed())
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
        if (currentPhase == SoilPhase.Water2 && AllSoilsWatered())
        {
            Debug.Log("SEGUNDA REGA COMPLETA! Fase finalizada.");
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
