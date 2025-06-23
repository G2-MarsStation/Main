using UnityEngine;

public class PainelSolarManager : MonoBehaviour
{
    public SolarPanelFix[] paineisSolares;
    private int consertados = 0;

    public Storm storm; // refer�ncia para o script da tempestade
    public GameObject alertaHUD; // Painel de aviso no Canvas

    private bool alertaJaMostrado = false;
    private float tempoHUD = 0f;
    private float duracaoHUD = 5f; // tempo que o HUD fica na tela

    void Start()
    {
        consertados = 0;

        foreach (var painel in paineisSolares)
        {
            painel.painelSolarManager = this;
        }

        if (alertaHUD != null)
            alertaHUD.SetActive(false);
    }

    void Update()
    {
        // Se o HUD j� foi mostrado e est� vis�vel, contamos o tempo at� escond�-lo
        if (alertaJaMostrado && alertaHUD.activeSelf)
        {
            tempoHUD += Time.deltaTime;

            if (tempoHUD >= duracaoHUD)
            {
                alertaHUD.SetActive(false);
            }

            return; // evita checar de novo se j� mostramos
        }

        // Se ainda n�o mostramos o HUD, verificamos se algum painel est� torto
        foreach (var painel in paineisSolares)
        {
            if (painel.ativado && !alertaJaMostrado)
            {
                if (alertaHUD != null)
                    alertaHUD.SetActive(true);

                alertaJaMostrado = true;
                tempoHUD = 0f; // come�a a contar o tempo
                break;
            }
        }
    }

    public void FicarTortoTodos()
    {
        consertados = 0;

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
