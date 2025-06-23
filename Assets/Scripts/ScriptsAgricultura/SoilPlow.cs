using UnityEngine;

public class SoilPlow : MonoBehaviour
{
    public KeyCode plowKey = KeyCode.Mouse0;
    public float finalTimer = 5f;
    private float currentTime = 0f;

    private bool nearSoil = false;
    private GameObject targetSoil;
    private bool isDoingAction = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dirt"))
        {
            nearSoil = true;
            targetSoil = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
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

    private void Update()
    {
        // Garante que só funciona na fase de arar
        if (SoilManager.instance.currentPhase != SoilPhase.Plow) return;
        if (!nearSoil || targetSoil == null) return;

        if (targetSoil.TryGetComponent(out SoilState soilState))
        {
            // Verifica se já está arado
            if (soilState.plowedSoil)
            {
                if (isDoingAction)
                {
                    isDoingAction = false;
                    currentTime = 0f;
                    ActionSliderUI.instance.HideSlider();
                }
                return;
            }

            // Se NÃO está arado
            if (Input.GetKey(plowKey))
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
                    PlowSoil(soilState);
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

    private void PlowSoil(SoilState soilState)
    {
        if (!soilState.plowedSoil)
        {
            soilState.CheckPlowedSoil();
            Debug.Log("Terra arada com sucesso.");
        }
        else
        {
            Debug.Log("Este solo já está arado.");
        }
    }
}
