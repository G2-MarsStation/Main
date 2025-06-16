using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathUi : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //habilitar novamente o mouse na cena

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Voltar()
    {
        SceneManager.LoadScene(0); // volta para a cena do game
        Debug.Log("volta ao inicio");
    }
    public void Menu()
    {
        SceneManager.LoadScene(2); // roda menu
        Debug.Log("volta ao menu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
