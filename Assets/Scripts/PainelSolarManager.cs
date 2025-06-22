using UnityEngine;

public class PainelSolarManager : MonoBehaviour
{
    public SolarPanelFix[] paineisSolares;
    private int consertados = 0;

    public Storm storm; // refer�ncia para o script da tempestade

    void Start()
    {
        consertados = 0;
        // Opcional: garantir que todos os pain�is tenham refer�ncia para este manager
        foreach (var painel in paineisSolares)
        {
            painel.painelSolarManager = this;
        }
    }

    public void FicarTortoTodos()
    {
        consertados = 0; // resetar contador
        foreach (var painel in paineisSolares)
        {
            painel.FicarTorto();
        }
    }

    public void PainelConsertado()
    {
        consertados++;
        Debug.Log($"Pain�is consertados: {consertados} / {paineisSolares.Length}");

        if (consertados >= paineisSolares.Length)
        {
            Debug.Log("Todos os pain�is foram consertados! Iniciando tempestade.");
            if (storm != null)
            {
                storm.StartStorm();
            }
        }
    }

    public bool TodosPaineisConsertados()
    {
        return consertados >= paineisSolares.Length;
    }
}
