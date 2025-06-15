using UnityEngine;

public class PlantGrow : MonoBehaviour
{
    public GameObject stage1Prefab; // Semente
    public GameObject stage2Prefab; // Broto
    public GameObject stage3Prefab; // Planta adulta (se quiser futuramente)

    private GameObject currentStage;
    private int stage = 1;

    void Start()
    {
        //// Come�a no est�gio 1 (semente)
        //currentStage = Instantiate(stage1Prefab, transform.position, Quaternion.identity, transform);
    }

    public void Grow()
    {
        if (stage == 1)
        {
            Destroy(currentStage);
            currentStage = Instantiate(stage2Prefab, transform.position, Quaternion.identity, transform);
            stage = 2;
            Debug.Log("Cresceu para o est�gio 2!");
        }
        else if (stage == 2)
        {
            Destroy(currentStage);
            currentStage = Instantiate(stage3Prefab, transform.position, Quaternion.identity, transform);
            stage = 3;
            Debug.Log("Cresceu para o est�gio 3!");
        }
        else
        {
            Debug.Log("J� est� no est�gio m�ximo.");
        }
    }
}