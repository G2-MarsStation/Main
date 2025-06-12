using UnityEngine;

public class SoilState : MonoBehaviour
{
    public bool treatedSoil = false;
    public bool plowedSoil = false;
    public bool plantedSoil = false;

    public GameObject plantedObject;
    public Transform[] plantPoints;
    private bool[] pointsUsed;

    public int maxPlants => plantPoints.Length;

    void Start()
    {
        if (plantPoints != null && plantPoints.Length > 0)
        {
            pointsUsed = new bool[plantPoints.Length];
        }
    }
    public void CheckSoilTreated()
    {
        treatedSoil = true;
        Debug.Log("Solo tratado");
    }

    public void CheckPlowedSoil()
    {
        plowedSoil = true;
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
        if (!plowedSoil || pointsUsed == null) return false;

        for (int i = 0; i < plantPoints.Length; i++)
        {
            if (!pointsUsed[i])
            {
                Instantiate(plantedObject, plantPoints[i].position, Quaternion.identity);
                pointsUsed[i] = true;

                CheckPlantedSoil();
                return true;
            }
        }

        return false; // Todos os pontos já foram usados
    }
}
