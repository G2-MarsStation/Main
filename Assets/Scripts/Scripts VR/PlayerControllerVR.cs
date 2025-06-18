using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControllerVR : MonoBehaviour
{
    public float maxOxygen = 5f;
    public float currentOxygen = 0f;
    public float oxygenTimer = 0f;
    public float oxygenInterval = 5f;

    private bool isDeadLoad = true;

    public UnityEngine.UI.Image oxygenBar; // arraste aqui a barra de UI no inspector
    void Start()
    {
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
                Debug.Log(currentOxygen);
            }

        } else
        {
            if (isDeadLoad) SceneManager.LoadScene("Death VR");
            isDeadLoad = false;
        }

        if (oxygenBar != null)
        {
            oxygenBar.fillAmount = currentOxygen / maxOxygen;
        }

    }   
}
