using UnityEngine;

public class SoilState : MonoBehaviour
{
    public bool treatedSoil = false;
    public bool plowedSoil = false;


    public void Start()
    {
        //Parabens pra mim que codei isso e nao tenho cerebro pra lembrar que troquei a ideia do script <3
        //SoilManager.instance?.RegisterSoil(this);
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
}
