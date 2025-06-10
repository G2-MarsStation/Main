using UnityEngine;

public class SoilState : MonoBehaviour
{
    public bool treatedSoil = false;
    public bool plowedSoil = false;


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
