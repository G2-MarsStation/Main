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
            targetSoil = GetComponent<GameObject>();
        };
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Dirt"))
        {
            nearSoil = false;
            targetSoil = GetComponent<GameObject>();
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
            }
        }   
    }

    void ApplyProduct()
    {
        Debug.Log("Produto aplicado");

        targetSoil.GetComponent<Renderer>().material.color = Color.green;

        Destroy(gameObject);
    }
}
