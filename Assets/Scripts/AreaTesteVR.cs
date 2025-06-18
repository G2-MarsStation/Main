using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class AreaTesteVR : MonoBehaviour
{
    [SerializeField] private XRGrabInteractable grabbedObject;

    private Vector3 pontoDeOrigemPosicao;
    private Quaternion pontoDeOrigemRotacao;
    private Rigidbody rb;
    private Collider col;

    void Awake()
    {
        // Guarda a posição e rotação de origem
        pontoDeOrigemPosicao = transform.position;
        pontoDeOrigemRotacao = transform.rotation;

        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    void OnEnable()
    {
        if (grabbedObject != null)
            grabbedObject.selectExited.AddListener(ObjetoSolto);
    }

    void OnDisable()
    {
        if (grabbedObject != null)
            grabbedObject.selectExited.RemoveListener(ObjetoSolto);
    }

    private void ObjetoSolto(SelectExitEventArgs args)
    {
        // Desativa o Rigidbody
        rb.Sleep();

        // Desativa o Collider
        col.enabled = false;

        // Retorna o objeto para a posição e rotação de origem
        transform.SetPositionAndRotation(pontoDeOrigemPosicao, pontoDeOrigemRotacao);

        // Reativa o Rigidbody e o Collider
        rb.WakeUp();
        col.enabled = true;
    }
}

