using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Management;

public class CheckVR : MonoBehaviour
{
    public static CheckVR checkVR;

    public static bool IsVR()
    {
        if (XRSettings.isDeviceActive)
        {
            Debug.Log("VR Ativo. Dispositivo: " + XRSettings.loadedDeviceName);
            return true;
        }
        else
        {
            Debug.Log("Nenhum dispositivo VR detectado.");
            return false;
        }
    }
}
