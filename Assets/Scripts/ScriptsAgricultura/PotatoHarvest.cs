using UnityEngine;

public class PotatoHarvest : MonoBehaviour
{
    public Transform player;
    public float interactionDistance = 2.5f;

    private bool readyToHarvest = true; // Pode controlar se a batata já está na fase final

    public SoilState soilState; // referência para o solo onde a batata está plantada

    void Start()
    {
        // Só precisa se quiser garantir algo no start
    }

    void Update()
    {
        // Colhe com tecla F, quando perto
        if (readyToHarvest && player != null)
        {
            float dist = Vector3.Distance(transform.position, player.position);

            if (dist <= interactionDistance && Input.GetKeyDown(KeyCode.F))
            {
                Harvest();
            }
        }
    }

    void Harvest()
    {
        Debug.Log("Batata colhida!");

        // Marca essa batata como colhida no SoilState
        if (soilState != null)
        {
            soilState.MarkPotatoHarvested(this.gameObject);
        }

        Destroy(gameObject);
    }
}