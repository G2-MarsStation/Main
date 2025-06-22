using UnityEngine;

public class PotatoHarvest : MonoBehaviour
{
    public Transform player;
    public float interactionDistance = 2.5f;

    private bool readyToHarvest = true; // Pode controlar se a batata j� est� na fase final

    public SoilState soilState; // refer�ncia para o solo onde a batata est� plantada

    void Start()
    {
        // S� precisa se quiser garantir algo no start
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