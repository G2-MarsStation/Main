using System.Collections;
using UnityEngine;

public class Portas : MonoBehaviour
{
    public Transform portaDireita;
    public Transform portaEsquerda;
    Coroutine corrotinaAbreda;
    Coroutine corrotinaFecheda;
    int porta = 150;
    int valor = 150;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("coiso");
        if (other.CompareTag("Player"))
        {
            if (corrotinaAbreda != null) StopCoroutine(corrotinaAbreda);
            if (corrotinaFecheda != null) StopCoroutine(corrotinaFecheda);

            corrotinaAbreda = StartCoroutine(OpenDoor());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (corrotinaAbreda != null) StopCoroutine(corrotinaAbreda);
            if (corrotinaFecheda != null) StopCoroutine(corrotinaFecheda);

            corrotinaFecheda = StartCoroutine(CloseDoor());
        }
    }

    IEnumerator OpenDoor()
    {
        for (int i = porta; i > 0; i--)
        {
            porta--;

            portaEsquerda.position += new Vector3(0, -0.018375f, 0);
            portaDireita.position += new Vector3(0, -0.018375f, 0);

            yield return new WaitForSeconds(0.0375f);
        }
    }

    IEnumerator CloseDoor()
    {
        for (int i = porta; i < valor; i++)
        {
            porta++;

            portaEsquerda.position += new Vector3(0, 0.018375f, 0);
            portaDireita.position += new Vector3(0, 0.018375f, 0);

            yield return new WaitForSeconds(0.0375f);
        }
    }
}

