using UnityEngine;

public class SoilState : MonoBehaviour
{
    public bool treatedSoil = false;
    public bool plowedSoil = false;
    public bool plantedSoil = false;
    public bool isWatered = false;

    public GameObject plantedObject;
    public GameObject prefabPhase2;
    public GameObject prefabPhase3;
    public Transform[] plantPoints;
    private bool[] pointsUsed;
    public bool[] potatoWatered;

    public GameObject terraArada;

    public int maxPlants => plantPoints.Length;

    public Vector3 offsetPhase1 = Vector3.zero;
    public Vector3 offsetPhase2 = Vector3.zero;
    public Vector3 offsetPhase3 = Vector3.zero;

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

    //public void GrowPlants()
    //{
    //    for (int i = 0; i < plantPoints.Length; i++)
    //    {
    //        if (pointsUsed[i])
    //        {
    //            Transform point = plantPoints[i];

    //            // Verifica se existe uma planta nesse ponto
    //            if (point.childCount > 0)
    //            {
    //                GameObject currentPlant = point.GetChild(0).gameObject;

    //                // Decide qual é a próxima fase baseado no nome ou algum identificador simples
    //                GameObject nextPrefab = null;

    //                if (currentPlant.name.Contains("Montinho"))
    //                {
    //                    nextPrefab = prefabPhase2;
    //                }
    //                else if (currentPlant.name.Contains("Small"))
    //                {
    //                    nextPrefab = prefabPhase3;
    //                }
    //                else
    //                {
    //                    Debug.Log("Planta já está na fase final.");
    //                    continue;
    //                }

    //                // Destrói a planta atual
    //                Destroy(currentPlant);

    //                // Instancia a próxima fase
    //                GameObject newPlant = Instantiate(nextPrefab, point.position, Quaternion.identity, point);
    //                newPlant.name = nextPrefab.name + "_Instance";
    //            }
    //        }
    //    }

    //    Debug.Log("As plantas desse terreno avançaram para a próxima fase.");
    //}

    public void GrowPlants()
    {
        for (int i = 0; i < plantPoints.Length; i++)
        {
            if (pointsUsed[i])
            {
                Transform point = plantPoints[i];

                // Verifica se existe uma planta nesse ponto
                if (point.childCount > 0)
                {
                    GameObject currentPlant = point.GetChild(0).gameObject;

                    GameObject nextPrefab = null;
                    Vector3 spawnPosition = point.position;

                    if (currentPlant.name.Contains("Montinho"))
                    {
                        nextPrefab = prefabPhase2;

                        // Aplica offset para fase 2 (Y -0.5)
                        spawnPosition += new Vector3(0, -0.8f, 0);
                    }
                    else if (currentPlant.name.Contains("Small"))
                    {
                        nextPrefab = prefabPhase3;

                        // Fase 3 sem alteração
                        spawnPosition = point.position;
                    }
                    else
                    {
                        Debug.Log("Planta já está na fase final.");
                        continue;
                    }

                    Destroy(currentPlant);

                    GameObject newPlant = Instantiate(nextPrefab, spawnPosition, Quaternion.identity, point);
                    newPlant.name = nextPrefab.name + "_Instance";
                }
            }
        }

        Debug.Log("As plantas desse terreno avançaram para a próxima fase.");

    } 
    }
