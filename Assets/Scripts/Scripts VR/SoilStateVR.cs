using UnityEngine;

public class SoilStateVR : MonoBehaviour
{
    public bool treatedSoil = false;
    public bool plowedSoil = false;
    public bool plantedSoil = false;
    public bool isWatered = false;

    public GameObject plantedObject;
    public Transform[] plantPoints;
    private bool[] pointsUsed;
    public bool[] potatoWatered;

    public GameObject terraArada;

    public int maxPlants => plantPoints.Length;

    void Start()
    {
        if (plantPoints != null && plantPoints.Length > 0)
        {
            terraArada?.SetActive(false);

            pointsUsed = new bool[plantPoints.Length];
            potatoWatered = new bool[plantPoints.Length];
        }
    }

    public void CheckSoilTreated()
    {
        treatedSoil = true;
        Debug.Log("Solo tratado (VR)");
    }

    public void CheckPlowedSoil()
    {
        plowedSoil = true;
        if (terraArada != null)
            terraArada.SetActive(true);

        Debug.Log("Solo arado (VR)");
    }

    private void CheckPlantedSoil()
    {
        foreach (bool used in pointsUsed)
        {
            if (!used)
            {
                plantedSoil = false;
                return;
            }
        }

        plantedSoil = true;
        Debug.Log("Solo totalmente plantado!");
    }

    // === Método chamado pelo plantador VR ===
    public void PlantPotato()
    {
        if (!plowedSoil || plantPoints == null || plantedObject == null)
        {
            Debug.Log("Condições de plantio não atendidas.");
            return;
        }

        for (int i = 0; i < plantPoints.Length; i++)
        {
            if (!pointsUsed[i])
            {
                GameObject planted = Instantiate(plantedObject, plantPoints[i].position, Quaternion.identity, plantPoints[i]);

                if (planted.GetComponent<PlantGrow>() == null)
                {
                    planted.AddComponent<PlantGrow>();
                }

                pointsUsed[i] = true;
                Debug.Log("Batata plantada no ponto " + i);
                CheckPlantedSoil();
                return;
            }
        }

        Debug.Log("Todos os pontos de plantio já foram usados.");
    }

    public void CheckWateredSoil()
    {
        isWatered = true;
        Debug.Log("Solo totalmente regado!");
    }
}
