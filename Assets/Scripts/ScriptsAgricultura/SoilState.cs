using UnityEngine;

public class SoilState : MonoBehaviour
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
            terraArada.SetActive(false);

            pointsUsed = new bool[plantPoints.Length];
            potatoWatered = new bool[plantPoints.Length];
        }
    }
    public void CheckSoilTreated()
    {
        treatedSoil = true;
         
    Debug.Log("Terra arada apareceu!");

        Debug.Log("Solo tratado");
    }

    public void CheckPlowedSoil()
    {
        plowedSoil = true;
        terraArada.SetActive(true);
        Debug.Log("Solo arado");
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

    public bool PlantBatata()
    {
        if (!plowedSoil || plantPoints == null) return false;

        for (int i = 0; i < plantPoints.Length; i++)
        {
            if (!pointsUsed[i])
            {
                GameObject planted = Instantiate(plantedObject, plantPoints[i].position, Quaternion.identity, plantPoints[i]);

                // Garante que a batata tem PlantGrow
                if (planted.GetComponent<PlantGrow>() == null)
                {
                    planted.AddComponent<PlantGrow>();
                }

                pointsUsed[i] = true;

                CheckPlantedSoil();
                return true;
            }
        }

        return false;
    }

    public void CheckWateredSoil()
    {
        isWatered = true;
        Debug.Log("Solo totalmente regado!");
    }
}
