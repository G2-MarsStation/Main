using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class PotatoPlantVR : MonoBehaviour
{
    public float plantTime = 1f;
    private float currentTime = 0f;
    private bool isHeld = false;
    private bool isPlanting = false;

    private GameObject targetSoil;

    [Header("Cabeça do jogador")]
    public Transform playerHead;

    [Header("Controlador usado")]
    public InputDeviceCharacteristics controllerCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
    private InputDevice targetDevice;

    [Header("Colisor de plantio")]
    public Collider plantingCollider;

    private XRGrabInteractable grabInteractable;

    void Start()
    {
        TryInitialize();
        grabInteractable = GetComponent<XRGrabInteractable>();

        if (plantingCollider != null)
            plantingCollider.enabled = false;

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
        isPlanting = false;
        currentTime = 0f;

        if (plantingCollider != null)
            plantingCollider.enabled = false;

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
            if (isPressed && !isPlanting && targetSoil != null)
            {
                StartPlanting();
            }
            else if (!isPressed && isPlanting)
            {
                StopPlanting();
            }
        }

        if (isPlanting && targetSoil != null)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= plantTime)
            {
                PlantPotato();
                currentTime = 0f;
                StopPlanting();
            }
        }
    }

    void StartPlanting()
    {
        isPlanting = true;
        if (plantingCollider != null)
            plantingCollider.enabled = true;
    }

    void StopPlanting()
    {
        isPlanting = false;
        if (plantingCollider != null)
            plantingCollider.enabled = false;
        currentTime = 0f;
    }

    void PlantPotato()
    {
        if (targetSoil.TryGetComponent(out SoilStateVR soilState))
        {
            if (!soilState.plantedSoil)
            {
                soilState.PlantPotato();
                Debug.Log("Batata plantada.");
            }
            else
            {
                Debug.Log("Solo já possui planta.");
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
        if (other.CompareTag("Dirt") && other.gameObject == targetSoil)
        {
            targetSoil = null;
            StopPlanting();
        }
    }
}
