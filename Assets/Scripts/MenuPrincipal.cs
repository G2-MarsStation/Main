using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    // Nome das cenas que serão carregadas
    public string JOGOVR = "Jogo";
    public string nomeCenaConfiguracoes = "Configuracoes";

    // Chamada ao clicar no botão "Jogar"
    public void Jogar()
    {
        SceneManager.LoadScene(1);
    }

    // Chamada ao clicar no botão "Configurações"
    public void AbrirConfiguracoes()
    {
        SceneManager.LoadScene(nomeCenaConfiguracoes);
    }

    // Chamada ao clicar no botão "Sair"
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

