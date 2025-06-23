using UnityEngine;

public class DirtApplication : MonoBehaviour
{
    public float finalTimer = 5f;
    private float currentTime = 0f;

    public KeyCode applyKey = KeyCode.Mouse0;

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
        // Só executa se estiver na fase de pulverizar
        if (SoilManager.instance.currentPhase != SoilPhase.ApplyProduct) return;
        if (!nearSoil || targetSoil == null) return;

        if (targetSoil.TryGetComponent(out SoilState soilState))
        {
            // Verifica se já está tratado
            if (soilState.treatedSoil)
            {
                if (isDoingAction)
                {
                    isDoingAction = false;
                    currentTime = 0f;
                    ActionSliderUI.instance.HideSlider();
                }
                return;
            }

            //  Caso NÃO esteja tratado
            if (Input.GetKey(applyKey))
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
                    ApplyProduct(soilState);
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

    void ApplyProduct(SoilState soilState)
    {
        if (!soilState.treatedSoil)
        {
            soilState.CheckSoilTreated();
            Debug.Log("Produto aplicado com sucesso.");
        }
        else
        {
            Debug.Log("Este solo já está tratado.");
        }
    }
}
