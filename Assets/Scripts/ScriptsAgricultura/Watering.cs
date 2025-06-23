using UnityEngine;

public class Watering : MonoBehaviour
{
    public KeyCode waterKey = KeyCode.Mouse0;
    public float finalTimer = 5f;
    private float currentTime = 0f;

    private bool nearSoil = false;
    private GameObject targetSoil;
    private bool isDoingAction = false;

    public ParticleSystem wateringParticles; // Partículas de rega
    public AudioSource AudioWater;           // Som de rega

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

            if (wateringParticles != null && wateringParticles.isPlaying)
                wateringParticles.Stop();

            if (AudioWater != null && AudioWater.isPlaying)
                AudioWater.Stop();
        }
    }

    public void Update()
    {
        if (SoilManager.instance.currentPhase != SoilPhase.Water &&
            SoilManager.instance.currentPhase != SoilPhase.Water2) return;

        if (!nearSoil || targetSoil == null) return;

        if (targetSoil.TryGetComponent(out SoilState soilState))
        {
            if (soilState.isWatered)
            {
                if (isDoingAction)
                {
                    isDoingAction = false;
                    currentTime = 0f;
                    ActionSliderUI.instance.HideSlider();

                    if (wateringParticles != null && wateringParticles.isPlaying)
                        wateringParticles.Stop();

                    if (AudioWater != null && AudioWater.isPlaying)
                        AudioWater.Stop();
                }
                return;
            }

            if (Input.GetKey(waterKey))
            {
                if (!isDoingAction)
                {
                    isDoingAction = true;
                    currentTime = 0f;
                    ActionSliderUI.instance.ShowSlider();

                    if (wateringParticles != null && !wateringParticles.isPlaying)
                        wateringParticles.Play();

                    if (AudioWater != null && !AudioWater.isPlaying)
                        AudioWater.Play();
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

                    if (wateringParticles != null && wateringParticles.isPlaying)
                        wateringParticles.Stop();

                    if (AudioWater != null && AudioWater.isPlaying)
                        AudioWater.Stop();
                }
            }
            else
            {
                if (isDoingAction)
                {
                    isDoingAction = false;
                    currentTime = 0f;
                    ActionSliderUI.instance.HideSlider();

                    if (wateringParticles != null && wateringParticles.isPlaying)
                        wateringParticles.Stop();

                    if (AudioWater != null && AudioWater.isPlaying)
                        AudioWater.Stop();
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
