using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    public GameObject finalScreen; // arraste a tela final aqui
    public AudioSource endingMusic;

    private bool gameEnded = false;

    void Start()
    {
        //habilitar novamente o mouse na cena

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void TriggerEndGame()
    {
        if (gameEnded) return;

        gameEnded = true;
        finalScreen.SetActive(true);
        endingMusic.Play();
        Time.timeScale = 0f; // pausa o jogo
    }

    public void VoltarMenu()
    {
        SceneManager.LoadScene(2); // volta para a cena do game
        Debug.Log("volta ao menu");
    }
}

