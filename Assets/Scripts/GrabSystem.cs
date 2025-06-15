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
                    if (raycastHit.collider.CompareTag("Grababble") || raycastHit.collider.CompareTag("Potato") || raycastHit.collider.CompareTag("Regador"))
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

        if (Input.GetKeyDown(KeyCode.G) && holdObject != null && holdObject.CompareTag("Potato"))
        {
            Ray ray2 = new Ray(cameraTransform.position, cameraTransform.forward);
            if (Physics.Raycast(ray2, out RaycastHit hit, 2f))
            {
                SoilState soil = hit.collider.GetComponent<SoilState>();

                // Verifica se o solo é válido, está arado e se a fase atual é PLANT
                if (soil != null
                    && soil.plowedSoil
                    && SoilManager.instance != null
                    && SoilManager.instance.currentPhase == SoilPhase.Plant)
                {
                    bool planted = soil.PlantBatata();
                    if (planted)
                    {
                        if (holdObject != null)
                        {
                            Destroy(holdObject);
                            holdObject = null;
                            holdScript = null;
                            Debug.Log("Batata plantada com sucesso!");
                            return;
                        }   
                    }
                    else
                    {
                        Debug.Log("Este solo já está com todas as batatas plantadas.");
                    }
                }
                else
                {
                    Debug.Log("Você ainda não pode plantar! Certifique-se de que todos os solos foram arados.");
                }
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

    public bool IsHolding(GameObject obj)
    {
        return holdObject == obj;
    }
}