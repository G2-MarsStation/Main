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

}
