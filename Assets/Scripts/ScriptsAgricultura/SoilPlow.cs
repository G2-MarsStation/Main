using UnityEngine;

public class SoilPlow : MonoBehaviour
{
    public KeyCode plowKey = KeyCode.Mouse0;
    public float finalTimer = 5f;
    private float currentTime = 0f;

    private bool nearSoil = false;
    private GameObject targetSoil;
    private bool isDoingAction = false;

    public AudioSource plowSound;  // Som de arar a terra

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

            if (plowSound != null && plowSound.isPlaying)
                plowSound.Stop();
        }
    }

    private void Update()
    {
        if (SoilManager.instance.currentPhase != SoilPhase.Plow) return;
        if (!nearSoil || targetSoil == null) return;

        if (targetSoil.TryGetComponent(out SoilState soilState))
        {
            if (soilState.plowedSoil)
            {
                if (isDoingAction)
                {
                    isDoingAction = false;
                    currentTime = 0f;
                    ActionSliderUI.instance.HideSlider();

                    if (plowSound != null && plowSound.isPlaying)
                        plowSound.Stop();
                }
                return;
            }

            if (Input.GetKey(plowKey))
            {
                if (!isDoingAction)
                {
                    isDoingAction = true;
                    currentTime = 0f;
                    ActionSliderUI.instance.ShowSlider();

                    if (plowSound != null && !plowSound.isPlaying)
                        plowSound.Play();
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

                    if (plowSound != null && plowSound.isPlaying)
                        plowSound.Stop();
                }
            }
            else
            {
                if (isDoingAction)
                {
                    isDoingAction = false;
                    currentTime = 0f;
                    ActionSliderUI.instance.HideSlider();

                    if (plowSound != null && plowSound.isPlaying)
                        plowSound.Stop();
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
