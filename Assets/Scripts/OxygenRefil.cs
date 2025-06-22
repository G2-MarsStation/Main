using System.Security.Cryptography;
using UnityEngine;

public class OxygenRefil : MonoBehaviour
{
    public float refilRate = 5f;
    public PainelSolarManager painelSolarManager; // arraste no Inspector
    private bool jaAtivouPainelTorto = false; // para ativar só uma vez



    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            painelSolarManager.FicarTortoTodos();

            //Pega o script do PlayerOxygen
            if (CheckVR.IsVR())
            {
                PlayerControllerVR playerOxygen = other.GetComponent<PlayerControllerVR>();
                
                if (playerOxygen != null && playerOxygen.currentOxygen < playerOxygen.maxOxygen)
                {
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        //abastece e limita o oxigênio do player para não passar do máximo permitido
                        playerOxygen.currentOxygen += refilRate;
                        playerOxygen.currentOxygen = Mathf.Min(playerOxygen.currentOxygen, playerOxygen.maxOxygen);

                        
                    }
                }


            } else
            {
                PlayerOxygen playerOxygen = other.GetComponent<PlayerOxygen>();

                //Se o Objeto tem o script (PlayerOxygen) e o oxigênio atual for menor que o oxigênio máximo
                if (playerOxygen != null && playerOxygen.currentOxygen < playerOxygen.maxOxygen)
                {
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        //abastece e limita o oxigênio do player para não passar do máximo permitido
                        playerOxygen.currentOxygen += refilRate;
                        playerOxygen.currentOxygen = Mathf.Min(playerOxygen.currentOxygen, playerOxygen.maxOxygen);
                        AtivarPainelTorto();

                    }
                }
            }
                
        }
    }
    void AtivarPainelTorto()
    {
        if (!jaAtivouPainelTorto && painelSolarManager != null)
        {
            painelSolarManager.FicarTortoTodos();
            jaAtivouPainelTorto = true; // evita ativar várias vezes
            Debug.Log("Painéis solares ficaram tortos!");
        }
    }
}

