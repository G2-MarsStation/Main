using UnityEngine;

public class SolarPanelTriggerUI : MonoBehaviour
{
    public PlayerActionUIManager uiManager;
    public SolarPanelFix painelFix;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && painelFix != null && painelFix.ativado)
        {
            uiManager.SetPertoDoPainel(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            uiManager.SetPertoDoPainel(false);
        }
    }
}
