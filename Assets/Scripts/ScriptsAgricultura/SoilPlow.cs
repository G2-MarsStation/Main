using UnityEngine;

public class SoilPlow : MonoBehaviour
{
    public KeyCode plowKey = KeyCode.Mouse0;
    public bool nearSoil = false;
    private GameObject targetSoil;
    public float finalTimer = 5;
    public float currentTimer = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dirt"))
        {
            nearSoil = true;
            targetSoil = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Dirt"))
        {
            nearSoil = false;
            targetSoil = null;
            currentTimer = 0;
        }
    }

    private void Update()
    {
        // Faz voltar se não estiver nessa fase
        if (SoilManager.instance.currentPhase != SoilPhase.Plow) return;

        if(nearSoil && Input.GetKey(plowKey))
        {
            Debug.Log("Arando a terra");
            currentTimer += Time.deltaTime;

            if(currentTimer >= finalTimer)
            {
                TryPlow();
                currentTimer = 0;
            }
        }
    }

    private void TryPlow()
    {
        if (targetSoil.TryGetComponent(out SoilState soilState))
        {
            //if (!soilState.treatedSoil)
            //{
            //    Debug.Log("nao arou o solo ainda");
            //    return;
            //} 
            if (!soilState.plowedSoil)
            {
                soilState.CheckPlowedSoil();
                Debug.Log("Terra arada");
            }
            else
            {
                Debug.Log("Ta arado Já");
            }
        }
    }
}
