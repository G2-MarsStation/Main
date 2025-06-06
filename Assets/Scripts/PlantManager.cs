using UnityEngine;

public class PlantManager : MonoBehaviour
{

    public GameObject soloPreparado;
    public GameObject semente;
    public GameObject broto;
    public GameObject plantaCrescendo;
    public GameObject plantaFinal;
    public GameObject ervaDaninha;

    private int etapa = 0;

    void Start()
    {
        AtualizarEtapa();
    }

    public void PreparaSolo()
    {
        if (etapa == 0)
        {
            etapa++;
            AtualizarEtapa();
        }
    }

    public void Plantar()
    {
        if (etapa == 1)
        {
            etapa++;
            AtualizarEtapa();
        }
    }

    public void Regar()
    {
        if (etapa == 2)
        {
            etapa++;
            AtualizarEtapa();
        }
        if (etapa == 4)
        {
            etapa++;
            AtualizarEtapa();
        }
        if (etapa == 6)
        {
            etapa++;
            AtualizarEtapa();
        }
    }

    public void TirarErvaDaninha()
    {
        if (etapa == 4)
        {
            ervaDaninha.SetActive(false);
            etapa++;
            AtualizarEtapa();
        }
    }

    public void AtualizarEtapa()
    {
        soloPreparado.SetActive(false);
        semente.SetActive(false);
        broto.SetActive(false);
        plantaCrescendo.SetActive(false);
        plantaFinal.SetActive(false);
        ervaDaninha.SetActive(false);

        switch (etapa)
        {
            case 1:
                soloPreparado.SetActive(true);
                break;
            case 2:
                semente.SetActive(true);
                break;
            case 3:
                broto.SetActive(true);
                break;
            case 4:
                plantaCrescendo.SetActive(true);
                ervaDaninha.SetActive(true);
                break;
            case 5:
                plantaCrescendo.SetActive(true);
                break;
            case 6:
                plantaFinal.SetActive(true);
                break;
            case 7:
                plantaFinal.SetActive(true);
                break;
        }
    }

}
