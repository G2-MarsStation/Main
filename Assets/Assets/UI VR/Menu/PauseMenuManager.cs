using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    [Header("Painéis")]
    [SerializeField] private GameObject painelPause;
    [SerializeField] private GameObject painelMenuInicial; // painel com os botões: Continuar, Opções, Sair
    [SerializeField] private GameObject painelOpcoes;      // painel com as opções

    [SerializeField] private MonoBehaviour scriptMoviment;

    [SerializeField] private GameObject canvasTarefas;




    private bool jogoPausado = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (jogoPausado)
            {
                Continuar();
            }
            else
            {
                Pausar();
            }
        }
    }

    public void Pausar()
    {
        canvasTarefas.SetActive(false);


        painelPause.SetActive(true);
        painelMenuInicial.SetActive(true);   // Garante que o menu principal aparece
        painelOpcoes.SetActive(false);       // Garante que o painel de opções começa fechado
        Time.timeScale = 0f;
        jogoPausado = true;

        scriptMoviment.enabled= false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Continuar()
    {
        canvasTarefas.SetActive(true);

        painelPause.SetActive(false);
        Time.timeScale = 1f;
        jogoPausado = false;

        scriptMoviment.enabled= true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void AbrirOpcoes()
    {
        painelMenuInicial.SetActive(false);
        painelOpcoes.SetActive(true);
    }

    public void FecharOpcoes()
    {
        painelOpcoes.SetActive(false);
        painelMenuInicial.SetActive(true);
    }

    public void Sair()
    {
        Debug.Log("Saindo do jogo...");
        SceneManager.LoadScene(2);

        // Para testes no editor:
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}

