using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    public GameObject finalScreen; // arraste a tela final aqui
    public AudioSource endingMusic;

    

    void Start()
    {
        //habilitar novamente o mouse na cena

        //Cursor.lockState = CursorLockMode.None;
       // Cursor.visible = true;
    }

    

    public void VoltarMenu()
    {
        SceneManager.LoadScene(2);
        Debug.Log("volta ao menu");
    }
}

