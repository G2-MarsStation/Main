using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class DirtApplicatorVR : MonoBehaviour
{
    public float finalTimer = 5f;
    private float currentTime = 0f;
    private bool isHeld = false;
    private bool isApplying = false;

    private GameObject targetSoil;

    [Header("Referência da cabeça do jogador")]
    public Transform playerHead;

    [Header("Dispositivo de controle (esquerdo ou direito)")]
    public InputDeviceCharacteristics controllerCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
    private InputDevice targetDevice;

    [Header("Colisor de detecção do produto")]
    public Collider sprayCollider; // Deixe ativo só quando aplicando o produto

    private XRGrabInteractable grabInteractable;

    void Start()
    {
        TryInitialize();
        grabInteractable = GetComponent<XRGrabInteractable>();

        if (sprayCollider != null)
            sprayCollider.enabled = false;

        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    void OnDestroy()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrab);
        grabInteractable.selectExited.RemoveListener(OnRelease);
    }

    void TryInitialize()
    {
        var devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);

        if (devices.Count > 0)
            targetDevice = devices[0];
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        isHeld = true;
        TryInitialize();
    }

    void OnRelease(SelectExitEventArgs args)
    {
        isHeld = false;
        isApplying = false;
        currentTime = 0f;

        if (sprayCollider != null)
            sprayCollider.enabled = false;

        // Reposiciona o aplicador na frente do jogador
        if (playerHead != null)
        {
            Vector3 forward = playerHead.forward;
            forward.y = 0;
            forward.Normalize();
            transform.position = playerHead.position + forward * 1f + Vector3.down * 0.5f;
            transform.rotation = Quaternion.LookRotation(forward);
        }
    }

    void Update()
    {
        if (!isHeld) return;
        if (!targetDevice.isValid)
        {
            TryInitialize();
            return;
        }

        if (targetDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool isPressed))
        {
            if (isPressed && !isApplying && targetSoil != null)
            {
                StartApplying();
            }
            else if (!isPressed && isApplying)
            {
                StopApplying();
            }
        }

        if (isApplying && targetSoil != null)
        {
            currentTime += Time.deltaTime;

            if (currentTime >= finalTimer)
            {
                ApplyProduct();
                currentTime = 0f;
                StopApplying();
            }
        }
    }

    void StartApplying()
    {
        isApplying = true;
        if (sprayCollider != null)
            sprayCollider.enabled = true;
    }

    void StopApplying()
    {
        isApplying = false;
        if (sprayCollider != null)
            sprayCollider.enabled = false;
        currentTime = 0f;
    }

    void ApplyProduct()
    {
        if (targetSoil.TryGetComponent(out SoilState soilState))
        {
            if (!soilState.treatedSoil)
            {
                soilState.CheckSoilTreated();
                Debug.Log("Produto aplicado na terra.");
            }
            else
            {
                Debug.Log("Terra já tratada.");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dirt"))
        {
            targetSoil = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Dirt"))
        {
            if (targetSoil == other.gameObject)
            {
                targetSoil = null;
                StopApplying();
            }
        }
    }
}
