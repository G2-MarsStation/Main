using UnityEngine;

public class OxygenRefil : MonoBehaviour
{
    public float refilRate = 5f;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Pega o script do PlayerOxygen
            PlayerOxygen playerOxygen = other.GetComponent<PlayerOxygen>();

            //Se o Objeto tem o script (PlayerOxygen) e o oxig�nio atual for menor que o oxig�nio m�ximo
            if (playerOxygen != null && playerOxygen.currentOxygen < playerOxygen.maxOxygen)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    //abastece e limita o oxig�nio do player para n�o passar do m�ximo permitido
                    playerOxygen.currentOxygen += refilRate;
                    playerOxygen.currentOxygen = Mathf.Min(playerOxygen.currentOxygen, playerOxygen.maxOxygen);
                }
            }
        }
    }
}

