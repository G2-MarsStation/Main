using UnityEngine;

public class PotatoBox : MonoBehaviour
{
    public GameObject potatoPrefab;
    public Transform spawnPoint;
    public float spawnDistance = 2f;
    public KeyCode grabPotatoKey = KeyCode.F;

    void Update()
    {
        if (Input.GetKeyDown(grabPotatoKey))
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, spawnDistance))
            {
                Debug.Log("Raycast acertou: " + hit.collider.name);
                if (hit.collider.gameObject == gameObject)
                {
                    Debug.Log("Spawnando batata...");
                    Instantiate(potatoPrefab, spawnPoint.position, Quaternion.identity);
                }
            }
        }
    }
}