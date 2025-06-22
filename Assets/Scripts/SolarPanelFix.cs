using UnityEngine;

public class SolarPanelFix : MonoBehaviour
{
    public bool ativado = false;
    private Quaternion rotacaoCorreta;
    public PainelSolarManager painelSolarManager; // referência para o manager

    void Start()
    {
        rotacaoCorreta = transform.rotation;
    }

    void OnMouseDown()
    {
        if (ativado)
        {
            transform.rotation = rotacaoCorreta;
            ativado = false;
            Debug.Log($"{gameObject.name} consertado!");

            // Avisar o manager que esse painel foi consertado
            if (painelSolarManager != null)
            {
                painelSolarManager.PainelConsertado();
            }
        }
    }

    public void FicarTorto()
    {
        transform.rotation = rotacaoCorreta * Quaternion.Euler(0, 0, 25f);
        ativado = true;
        Debug.Log($"{gameObject.name} ficou torto!");
    }
}
