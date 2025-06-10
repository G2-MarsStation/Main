using UnityEngine;

public class DirtApplication : MonoBehaviour
{
    float finalTimer = 5f;
    float currentTime = 0f;

    public KeyCode applyKey = KeyCode.Mouse0;
    private bool nearSoil = false;
    private GameObject targetSoil;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dirt"))
        {
            nearSoil = true;
            targetSoil = other.gameObject;
        };
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Dirt"))
        {
            nearSoil = false;
            targetSoil = other.gameObject;
            currentTime = 0f;
        }
    }

    void Update()
    {
        if (nearSoil && Input.GetKey(applyKey))
        {
            Debug.Log("Aplicando o produto");
            currentTime += Time.deltaTime;

            if (currentTime >= finalTimer)
            {
                ApplyProduct();
                currentTime = 0f;
            }
        }   
    }

    void ApplyProduct()
    {
        if (targetSoil.TryGetComponent(out SoilState soilState))
        {

            // Se a terra ainda nao foi tratada...
            if (!soilState.treatedSoil)
            {
                soilState.CheckSoilTreated();
                Debug.Log("Produto aplicado");
            }
            else
            {
                Debug.Log("Terra já tratada");
            }
        }

        //Destroy(gameObject);
    }
}
