using UnityEngine;

public class PotatoUI : MonoBehaviour
{
    public GameObject potatoInfoHUD;
    private bool hudShownOnce = false;
    private bool isPlayerInside = false;

    void Start()
    {
        if (potatoInfoHUD != null)
            potatoInfoHUD.SetActive(false);
    }

    void Update()
    {
        if (isPlayerInside && potatoInfoHUD.activeSelf && !hudShownOnce)
        {
            if (Input.GetKeyDown(KeyCode.Return)) // agora usa a tecla Enter
            {
                ClosePotatoHUD();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hudShownOnce)
        {
            isPlayerInside = true;

            if (potatoInfoHUD != null)
                potatoInfoHUD.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
        }
    }

    private void ClosePotatoHUD()
    {
        if (potatoInfoHUD != null)
            potatoInfoHUD.SetActive(false);

        hudShownOnce = true;
    }
}