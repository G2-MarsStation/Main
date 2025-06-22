using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    // Nome das cenas que ser�o carregadas
    public string JOGOVR = "Jogo";
    public string nomeCenaConfiguracoes = "Configuracoes";

    // Chamada ao clicar no bot�o "Jogar"
    public void JogarVR()
    {
        SceneManager.LoadScene(5);
    }

    // Chamada ao clicar no bot�o "Configura��es"
    public void JogarPC()
    {
        SceneManager.LoadScene(0);
    }

    // Chamada ao clicar no bot�o "Sair"
    public void Sair()
    {
        Debug.Log("Saindo do jogo...");
        Application.Quit();

        // Para testes no editor:
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}

