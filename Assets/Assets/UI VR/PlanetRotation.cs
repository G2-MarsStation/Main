using UnityEngine;

public class PlanetRotation : MonoBehaviour
{

    public Vector3 rotationAxis= Vector3.up;
    public float rotationSpeed = 6f; //grau por seg

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime);
    }
}
