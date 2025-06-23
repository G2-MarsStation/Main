using UnityEngine;

public class DirtApplication : MonoBehaviour
{
    public float finalTimer = 5f;
    private float currentTime = 0f;

    public KeyCode applyKey = KeyCode.Mouse0;

    private bool nearSoil = false;
    private GameObject targetSoil;
    private bool isDoingAction = false;

    public ParticleSystem Aplication;   // Partículas da aplicação
    public AudioSource spraySound;      // Som do pulverizador

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

            if (Aplication != null && Aplication.isPlaying)
                Aplication.Stop();

            if (spraySound != null && spraySound.isPlaying)
                spraySound.Stop();
        }
    }

    private void Update()
    {
        if (SoilManager.instance.currentPhase != SoilPhase.ApplyProduct) return;
        if (!nearSoil || targetSoil == null) return;

        if (targetSoil.TryGetComponent(out SoilState soilState))
        {
            if (soilState.treatedSoil)
            {
                if (isDoingAction)
                {
                    isDoingAction = false;
                    currentTime = 0f;
                    ActionSliderUI.instance.HideSlider();

                    if (Aplication != null && Aplication.isPlaying)
                        Aplication.Stop();

                    if (spraySound != null && spraySound.isPlaying)
                        spraySound.Stop();
                }
                return;
            }

            if (Input.GetKey(applyKey))
            {
                if (!isDoingAction)
                {
                    isDoingAction = true;
                    currentTime = 0f;
                    ActionSliderUI.instance.ShowSlider();

                    if (Aplication != null && !Aplication.isPlaying)
                        Aplication.Play();

                    if (spraySound != null && !spraySound.isPlaying)
                        spraySound.Play();
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

                    if (Aplication != null && Aplication.isPlaying)
                        Aplication.Stop();

                    if (spraySound != null && spraySound.isPlaying)
                        spraySound.Stop();
                }
            }
            else
            {
                if (isDoingAction)
                {
                    isDoingAction = false;
                    currentTime = 0f;
                    ActionSliderUI.instance.HideSlider();

                    if (Aplication != null && Aplication.isPlaying)
                        Aplication.Stop();

                    if (spraySound != null && spraySound.isPlaying)
                        spraySound.Stop();
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
