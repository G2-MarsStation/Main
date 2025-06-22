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
    private GameObject holdObject;

    private GrabbableObject holdScript;

    void Update()
    {
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        RaycastHit raycastHit;

        Debug.DrawRay(ray.origin, ray.direction, Color.blue);

        if (Input.GetKeyDown(grabKey))
        {
            // Se não estiver segurando nenhum objeto
            if (holdObject == null)
            {
                if (Physics.Raycast(ray, out raycastHit, grabDistance))
                {
                    if (raycastHit.collider.CompareTag("Grababble") ||
                        raycastHit.collider.CompareTag("Potato") ||
                        raycastHit.collider.CompareTag("Regador"))
                    {
                        holdObject = raycastHit.collider.gameObject;
                        holdScript = holdObject.GetComponent<GrabbableObject>();
                    }

                    if (holdScript != null)
                    {
                        // VERIFICA SE PODE PEGAR
                        if (holdScript.PodeSerPego())
                        {
                            holdScript.Grab(holdPoint);
                        }
                        else
                        {
                            Debug.Log("Você precisa consertar os painéis solares antes de colher.");
                            holdScript = null;
                            holdObject = null;
                        }
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

        // Plantar a batata
        if (Input.GetKeyDown(KeyCode.G) && holdObject != null && holdObject.CompareTag("Potato"))
        {
            Collider[] colliders = Physics.OverlapSphere(holdObject.transform.position, 0.5f);

            foreach (var col in colliders)
            {
                SoilState soil = col.GetComponent<SoilState>();

                if (soil != null &&
                    soil.plowedSoil &&
                    SoilManager.instance != null &&
                    SoilManager.instance.currentPhase == SoilPhase.Plant)
                {
                    bool planted = soil.PlantBatata();
                    if (planted)
                    {
                        Destroy(holdObject);
                        holdObject = null;
                        holdScript = null;
                        Debug.Log("Batata plantada com sucesso!");
                    }
                    else
                    {
                        Debug.Log("Este solo já está com todas as batatas plantadas.");
                    }
                    return;
                }
            }

            Debug.Log("Nenhum terreno encontrado próximo para plantar.");
        }

        // Rotacionar objeto na mão
        if (holdObject != null)
        {
            float rotation = 0f;

            if (Input.GetKey(rotateLeftKey))
            {
                rotation -= rotationSpeed * Time.deltaTime;
            }

            if (Input.GetKey(rotateRightKey))
            {
                rotation += rotationSpeed * Time.deltaTime;
            }

            holdObject.transform.Rotate(Vector3.up, rotation, Space.Self);
        }
    }

    public bool IsHolding(GameObject obj)
    {
        return holdObject == obj;
    }
}