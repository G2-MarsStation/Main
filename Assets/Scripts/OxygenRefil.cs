using System.Security.Cryptography;
using UnityEngine;

public class OxygenRefil : MonoBehaviour
{
    public float refilRate = 5f;
    public AudioSource audioFinal;
    public PainelSolarManager painelSolarManager; // arraste no Inspector
    //private bool jaAtivouPainelTorto = false; // para ativar s� uma vez


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (CheckVR.IsVR())
            {
                PlayerControllerVR playerOxygen = other.GetComponent<PlayerControllerVR>();

                if (playerOxygen != null && playerOxygen.currentOxygen < playerOxygen.maxOxygen)
                {
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        playerOxygen.currentOxygen += refilRate;
                        playerOxygen.currentOxygen = Mathf.Min(playerOxygen.currentOxygen, playerOxygen.maxOxygen);
                    }
                }
            }
            else
            {
                PlayerOxygen playerOxygen = other.GetComponent<PlayerOxygen>();


                if (playerOxygen != null && playerOxygen.currentOxygen < playerOxygen.maxOxygen)
                {
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        playerOxygen.currentOxygen += refilRate;
                        playerOxygen.currentOxygen = Mathf.Min(playerOxygen.currentOxygen, playerOxygen.maxOxygen);
                    }
                // Toca o �udio final
                if (audioFinal != null)
                {
                    audioFinal.Play();
                    Debug.Log("�udio final iniciado.");
                    
                }
                else
                {
                    Debug.LogWarning("�udio final n�o est� atribu�do.");
                   
                }
                }
            }
        }
    }
}


