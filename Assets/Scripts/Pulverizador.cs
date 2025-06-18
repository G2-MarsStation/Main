using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Pulverizador : MonoBehaviour
{
    [Header("Selecione o controle (Left, Right)")]
    public InputDeviceCharacteristics controllerCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;

    private InputDevice targetDevice;

    // === Adições ===
    public ParticleSystem pegarParticle; // Partícula que será ativada ao pegar o objeto
    public bool isGrabbingObject = false; // Defina como true quando estiver pegando algo

    void Start()
    {
        TryInitialize();
    }

    void TryInitialize()
    {
        var devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);

        if (devices.Count > 0)
        {
            targetDevice = devices[0];
            Debug.Log("Dispositivo encontrado: " + targetDevice.name);
        }
        else
        {
            Debug.LogWarning("Nenhum dispositivo encontrado com essas características.");
        }
    }

    void Update()
    {
        if (!targetDevice.isValid)
        {
            TryInitialize(); // tenta reconectar se desconectou
            return;
        }

        // Trigger como valor float (0.0 a 1.0)
        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            if (triggerValue > 0.1f)
            {
                Debug.Log("Trigger pressionado: " + triggerValue);
            }

            // === Ativa/Desativa a partícula se estiver pegando algo ===
            if (triggerValue > 0.1f && isGrabbingObject)
            {
                if (!pegarParticle.isPlaying)
                    pegarParticle.Play();
            }
            else
            {
                if (pegarParticle.isPlaying)
                    pegarParticle.Stop();
            }
        }

        // Trigger como botão booleano (on/off)
        if (targetDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool isPressed))
        {
            if (isPressed)
            {
                //Debug.Log("Trigger button pressionado");
            }
        }
    }
}
