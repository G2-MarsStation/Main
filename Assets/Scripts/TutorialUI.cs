using UnityEngine;

public class TutorialUI : MonoBehaviour
{

    public GameObject oxygen;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        oxygen.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            oxygen.SetActive(true);
            Debug.Log("esta pertoo.");
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            oxygen.SetActive(false);
            Debug.Log("Saiu de perto.");
            // Aqui você pode remover a UI se tiver
        }
    }
}
