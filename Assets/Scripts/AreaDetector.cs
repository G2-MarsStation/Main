using UnityEngine;

public class AreaDetector : MonoBehaviour
{
    public float snapDistance = 1f;

    public Transform snapRotationReference;

    [Tooltip("Vincule o script TutorialOnPickup aqui")]
    public TutorialUI tutorialScript;

    public bool IsObjectInside(GameObject grababble)
    {
        // Salva a distância em que o player pode agarrar o objeto
        float distance = Vector3.Distance(grababble.transform.position, transform.position);

        return distance <= snapDistance;
    }

    // O método OnTriggerEnter serve para quando temos um collider
    // com isTrigger ativado, que detecta outro Collider na área.
    private void OnTriggerEnter(Collider other)
    {
        //Verifica se o objeto que entrou na área possui a tag indicada
        if (other.CompareTag("Grabbable") || other.CompareTag("Potato"))
        {
            GrabbableObject grabbable = other.GetComponent<GrabbableObject>();

          

            // Se o componente existir, notifica que o objeto está dentro da área
            if (grabbable != null)
            {
                grabbable.NotifyEnterArea(this);
            }
        }
    }



    private void OnTriggerExit(Collider other)
    {
        //Verifica se o objeto que saiu aa área possui a tag indicada
        if (other.CompareTag("Grabbable") || other.CompareTag("Potato"))
        {
            GrabbableObject grabbable = other.GetComponent<GrabbableObject>();

            // Se o componente existir, notifica que o objeto está fora da área
            if (grabbable != null)
            {
                grabbable.NotifyEnterArea(this);
            }
        }
    }

    // Esse método desenha uma caixa verde na cena para auxiliar na visualização na área de detecção.
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}