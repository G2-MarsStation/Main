using UnityEngine;

public class PlayerOxygen : MonoBehaviour
{
    PlayerController playerController;
    public float maxOxygen = 5f;
    public float currentOxygen = 0f;
    public float oxygenTimer = 0f;
    public float oxygenInterval = 5f;
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        //Oxigênio do player
        currentOxygen = maxOxygen;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentOxygen > 0)
        {
            oxygenTimer -= Time.deltaTime;
            if (oxygenTimer <= 0)
            {
                currentOxygen -= 1;
                currentOxygen = Mathf.Max(currentOxygen, 0);
                oxygenTimer = oxygenInterval;
                Debug.Log($"Oxigênio: {currentOxygen} | Vida: {playerController.currentHealth:F1}");
            }

        }
        else
        {
            playerController.currentHealth -= 1 * Time.deltaTime;
        }
    }
}
