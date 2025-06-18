using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class SoilPlowVR : MonoBehaviour
{
    public float finalTimer = 5f;
    private float currentTimer = 0f;

    private bool isHeld = false;
    private bool isPlowing = false;

    private GameObject targetSoil;

    [Header("Referência da cabeça do jogador")]
    public Transform playerHead;

    [Header("Dispositivo de controle (esquerdo ou direito)")]
    public InputDeviceCharacteristics controllerCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
    private InputDevice targetDevice;

    private XRGrabInteractable grabInteractable;

    void Start()
    {
        TryInitialize();
        grabInteractable = GetComponent<XRGrabInteractable>();

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
        isPlowing = false;
        currentTimer = 0f;

        // Reposiciona na frente do jogador
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

        if (SoilManager.instance.currentPhase != SoilPhase.Plow) return;

        if (targetDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool isPressed))
        {
            if (isPressed && !isPlowing && targetSoil != null)
            {
                StartPlowing();
            }
            else if (!isPressed && isPlowing)
            {
                StopPlowing();
            }
        }

        if (isPlowing && targetSoil != null)
        {
            currentTimer += Time.deltaTime;

            if (currentTimer >= finalTimer)
            {
                TryPlow();
                currentTimer = 0f;
                StopPlowing();
            }
        }
    }

    void StartPlowing()
    {
        isPlowing = true;
    }

    void StopPlowing()
    {
        isPlowing = false;
        currentTimer = 0f;
    }

    private void TryPlow()
    {
        if (targetSoil.TryGetComponent(out SoilState soilState))
        {
            if (!soilState.plowedSoil)
            {
                soilState.CheckPlowedSoil();
                Debug.Log("Terra arada com sucesso!");
            }
            else
            {
                Debug.Log("Terra já foi arada.");
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
                StopPlowing();
            }
        }
    }
}
