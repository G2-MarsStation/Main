using UnityEngine;

public class PortaAutomatica : MonoBehaviour
{
    public Animator animacaoPorta; // arraste o objeto que tem o Animator aqui no Unity
    public string nomeTrigger = "AbrirPorta"; // nome da trigger da animação

    private void OnTriggerEnter(Collider outro)
    {
        if (outro.CompareTag("Player"))
        {
            animacaoPorta.SetTrigger(nomeTrigger);
        }
    }
}
