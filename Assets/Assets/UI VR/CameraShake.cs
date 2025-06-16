using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float shakeMagnitude = 0.02f; // Força do tremor
    public float dampingSpeed = 1f; // Velocidade para diminuir o tremor

    private float currentShakeTime = 0f;
    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        if (currentShakeTime > 0)
        {
            transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;
            currentShakeTime -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            currentShakeTime = 0f;
            transform.localPosition = initialPosition;
        }
    }

    public void StartShake(float duration)
    {
        currentShakeTime = duration;
    }

    public void StopShake()
    {
        currentShakeTime = 0f;
        transform.localPosition = initialPosition;
    }
}


