using UnityEngine;
using System.Collections;

public class Watering : MonoBehaviour
{
    
    public KeyCode waterKey = KeyCode.Mouse0;
    public bool nearSoil = false;
    private GameObject targetSoil;
    public float finalTimer = 5f;
    public float currentTimer = 0f;

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
            currentTimer = 0f;
        }
    }

    private void Update()
    {
        // Só funciona na fase de regar
        if (SoilManager.instance.currentPhase != SoilPhase.Water) return;

        if (nearSoil && Input.GetKey(waterKey))
        {
            Debug.Log("Regando a terra...");
            currentTimer += Time.deltaTime;

            if (currentTimer >= finalTimer)
            {
                TryWater();
                currentTimer = 0f;
            }
        }
    }

    private void TryWater()
    {
        if (targetSoil.TryGetComponent(out SoilState soilState))
        {
            if (!soilState.isWatered)
            {
                soilState.isWatered = true;
                Debug.Log("Terra regada com sucesso!");
            }
            else
            {
                Debug.Log("Essa terra já está regada.");
            }
        }
    }
}
