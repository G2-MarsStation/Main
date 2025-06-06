using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VRController : MonoBehaviour
{
    [Header("Movimenta��o")]
    public float speed = 1.5f;
    public float jumpForce = 5f;
    public float gravity = -3.721f;
    public float groundCheckDistance = 0.1f;

    [Header("Atributos de Vida")]
    public float maxHealth = 3;
    public float currentHealth;
    public Scrollbar ScrollHealthBar;

    [Header("VR Inputs")]
    public InputActionProperty moveInput;       // Anal�gico (Vector2)
    public InputActionProperty jumpAction;      // Bot�o de pulo

    [Header("Detec��o de Solo")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;

    private CharacterController controller;
    private Transform cameraTransform;
    private float yVelocity;
    private Vector3 moveDirection;
    private bool isGrounded;
    private bool isDead = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;

        currentHealth = maxHealth;
    }

    void Update()
    {
        if (transform.position.y <= -5 || currentHealth <= 0)
        {
            SceneManager.LoadScene("DeathScene");
            return;
        }

        // Checa se est� no ch�o
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckDistance, groundMask);

        // Recebe o input do anal�gico (X = horizontal, Y = vertical)
        Vector2 input = moveInput.action.ReadValue<Vector2>();

        // Calcula dire��o baseada na rota��o da cabe�a (camera)
        Vector3 forward = new Vector3(cameraTransform.forward.x, 0f, cameraTransform.forward.z).normalized;
        Vector3 right = new Vector3(cameraTransform.right.x, 0f, cameraTransform.right.z).normalized;
        moveDirection = (right * input.x + forward * input.y).normalized;

        // Pulo
        if (isGrounded && jumpAction.action.WasPerformedThisFrame())
        {
            yVelocity = Mathf.Sqrt(jumpForce * -2f * gravity);
        }

        // Gravidade
        yVelocity += gravity * Time.deltaTime;

        // Movimento final
        Vector3 move = moveDirection * speed;
        move.y = yVelocity;
        controller.Move(move * Time.deltaTime);

        // Atualiza barra de vida, se existir
        if (ScrollHealthBar != null)
            ScrollHealthBar.size = currentHealth / maxHealth;
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckDistance);
    }
}