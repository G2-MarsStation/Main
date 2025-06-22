using UnityEngine;

public class SolarPanelFix : MonoBehaviour
{
    public bool ativado = false;
    private Quaternion rotacaoCorreta;
    public PainelSolarManager painelSolarManager;

    public Transform player; 
    public float distanciaPermitida = 2.5f; 

    void Start()
    {
        rotacaoCorreta = transform.rotation;
    }

    void OnMouseDown()
    {
        float distancia = Vector3.Distance(transform.position, player.position);

        if (distancia <= distanciaPermitida)
        {
            if (ativado)
            {
                transform.rotation = rotacaoCorreta;
                ativado = false;
                Debug.Log($"{gameObject.name} consertado!");

                if (painelSolarManager != null)
                {
                    painelSolarManager.PainelConsertado();
                }
            }
        }
        else
        {
            Debug.Log("Chegue mais perto para consertar o painel!");
        }
    }

    public void FicarTorto()
    {
        transform.rotation = rotacaoCorreta * Quaternion.Euler(0, 0, 25f);
        ativado = true;
        Debug.Log($"{gameObject.name} ficou torto!");
    }
}
