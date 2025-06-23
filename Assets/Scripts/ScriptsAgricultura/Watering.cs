using UnityEngine;

public class Watering : MonoBehaviour
{
    public KeyCode waterKey = KeyCode.Mouse0;
    public float finalTimer = 5f;
    private float currentTime = 0f;

    private bool nearSoil = false;
    private GameObject targetSoil;
    private bool isDoingAction = false;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dirt"))
        {
            nearSoil = true;
            targetSoil = other.gameObject;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Dirt"))
        {
            nearSoil = false;
            targetSoil = null;
            currentTime = 0f;
            isDoingAction = false;
            ActionSliderUI.instance.HideSlider();
        }
    }

    public void Update()
    {
        // Funciona nas fases de Water e Water2
        if (SoilManager.instance.currentPhase != SoilPhase.Water &&
            SoilManager.instance.currentPhase != SoilPhase.Water2) return;

        if (!nearSoil || targetSoil == null) return;

        if (targetSoil.TryGetComponent(out SoilState soilState))
        {
            // Verifica se já está regado
            if (soilState.isWatered)
            {
                if (isDoingAction)
                {
                    isDoingAction = false;
                    currentTime = 0f;
                    ActionSliderUI.instance.HideSlider();
                }
                return;
            }

            // Se NÃO está regado
            if (Input.GetKey(waterKey))
            {
                if (!isDoingAction)
                {
                    isDoingAction = true;
                    currentTime = 0f;
                    ActionSliderUI.instance.ShowSlider();
                }

                currentTime += Time.deltaTime;
                float progress = currentTime / finalTimer;
                ActionSliderUI.instance.UpdateSlider(progress);

                if (progress >= 1f)
                {
                    WaterSoil(soilState);
                    currentTime = 0f;
                    isDoingAction = false;
                    ActionSliderUI.instance.HideSlider();
                }
            }
            else
            {
                // Soltou o botão antes de concluir
                if (isDoingAction)
                {
                    isDoingAction = false;
                    currentTime = 0f;
                    ActionSliderUI.instance.HideSlider();
                }
            }
        }
    }

    private void WaterSoil(SoilState soilState)
    {
        if (!soilState.isWatered)
        {
            soilState.GrowPlants();
            soilState.isWatered = true;
            Debug.Log("Terra regada com sucesso!");
        }
        else
        {
            Debug.Log("Essa terra já está regada.");
        }
    }
}
