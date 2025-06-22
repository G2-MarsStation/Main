using System.Security.Cryptography;
using UnityEngine;

public class OxygenRefil : MonoBehaviour
{
    public float refilRate = 5f;
    public PainelSolarManager painelSolarManager; // arraste no Inspector
    private bool jaAtivouPainelTorto = false; // para ativar s� uma vez



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
                        //abastece e limita o oxig�nio do player para n�o passar do m�ximo permitido
                        playerOxygen.currentOxygen += refilRate;
                        playerOxygen.currentOxygen = Mathf.Min(playerOxygen.currentOxygen, playerOxygen.maxOxygen);

                        
                    }
                }


            } else
            {
                PlayerOxygen playerOxygen = other.GetComponent<PlayerOxygen>();

                //Se o Objeto tem o script (PlayerOxygen) e o oxig�nio atual for menor que o oxig�nio m�ximo
                if (playerOxygen != null && playerOxygen.currentOxygen < playerOxygen.maxOxygen)
                {
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        //abastece e limita o oxig�nio do player para n�o passar do m�ximo permitido
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
            jaAtivouPainelTorto = true; // evita ativar v�rias vezes
            Debug.Log("Pain�is solares ficaram tortos!");
        }
    }
}

