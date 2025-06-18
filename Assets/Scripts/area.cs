using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Teste : MonoBehaviour
{
    [SerializeField] XRGrabInteractable grabbedObject;
    private Pose pontoDeOrigem;
    private Rigidbody rb;

    private void OnEnable() => grabbedObject.selectExited.AddListener(ObjetoSolto);
    private void OnDisable() => grabbedObject.selectExited.RemoveListener(ObjetoSolto);

    private void Awake()
    {
        pontoDeOrigem.position = this.transform.position;
        pontoDeOrigem.rotation = this.transform.rotation;
        rb = GetComponent<Rigidbody>();
    }

    private void ObjetoSolto(SelectExitEventArgs args)
    {
        // Desativa o Rigidbody (impede movimentação)
        rb.Sleep();

        // Desativa o Collider para não colidir ao mover
        GetComponent<Collider>().enabled = false;

        // Retorna o objeto para a posição de origem
        this.transform.position = pontoDeOrigem.position;
        this.transform.rotation = pontoDeOrigem.rotation;

        // Reativa o Rigidbody
        rb.WakeUp();

        // Reativa o Collider
        GetComponent<Collider>().enabled = true;
    }
}
