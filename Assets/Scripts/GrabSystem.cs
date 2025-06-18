using UnityEngine;

public class GrabSystem : MonoBehaviour
{
    public Transform cameraTransform;

    public float grabDistance;

    public KeyCode grabKey = KeyCode.F;
    public KeyCode rotateLeftKey = KeyCode.Q;
    public KeyCode rotateRightKey = KeyCode.E;

    public float rotationSpeed;

    public Transform holdPoint;
    //Referencia ao objeto
    private GameObject holdObject;

    private GrabbableObject holdScript;

    void Update()
    {
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);

        RaycastHit raycastHit;

        Debug.DrawRay(ray.origin, ray.direction, Color.blue);

        if (Input.GetKeyDown(grabKey))
        {
            //Se nao estiver segurando nenhum objeto
            if (holdObject == null)
            {
                if (Physics.Raycast(ray, out raycastHit, grabDistance))
                {
                    if (raycastHit.collider.CompareTag("Grababble"))
                    {
                        holdObject = raycastHit.collider.gameObject;

                        holdScript = holdObject.GetComponent<GrabbableObject>();
                    }

                    if (holdScript != null)
                    {
                        holdScript.Grab(holdPoint);
                    }
                }
            }
            else
            {
                holdScript.Release();

                holdScript = null;
                holdObject = null;

            }
        }

        if (holdObject != null)
        {
            float rotation = 0f;

            // Adiciona rotaçao negativa, gira o objeto para a direita
            if (Input.GetKey(rotateLeftKey))
            {
                rotation -= rotationSpeed * Time.deltaTime;
            }

            // Adiciona rotaçao positiva, gira o objeto para a esquerda
            if (Input.GetKey(rotateRightKey))
            {
                rotation += rotationSpeed * Time.deltaTime;

            }

            // Space.Self -> ele mesmo
            holdObject.transform.Rotate(Vector3.up, rotation, Space.Self);


        }
    }
}