using UnityEngine;

public class OxygenRefil : MonoBehaviour
{
    public float refilRate = 5f;
    public AudioSource refilAudio;

    public GameObject oxygen;


     

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            oxygen.SetActive(true);

            //Pega o script do PlayerOxygen
            PlayerOxygen playerOxygen = other.GetComponent<PlayerOxygen>();

            //Se o Objeto tem o script (PlayerOxygen) e o oxig�nio atual for menor que o oxig�nio m�ximo
            if (playerOxygen != null && playerOxygen.currentOxygen < playerOxygen.maxOxygen)
            {
                
                if (Input.GetKeyDown(KeyCode.F))
                {
                    //abastece e limita o oxig�nio do player para n�o passar do m�ximo permitido
                    refilAudio.Play();
                    playerOxygen.currentOxygen += refilRate;
                    playerOxygen.currentOxygen = Mathf.Min(playerOxygen.currentOxygen, playerOxygen.maxOxygen);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            oxygen.SetActive(false);
            Debug.Log("Saiu de perto.");
            // Aqui voc� pode remover a UI se tiver
        }
    }


}

