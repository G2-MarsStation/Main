using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Publicos

    public float speed = 5f;
    public float sensitivity = 2f;

    public float jumpForce = 5f;
    public float gravity = -3.721f;
    public float groundCheckDistance = 0.1f;

    public float maxHealth = 3;
    public float currentHealth;
    public Scrollbar ScrollHealthBar;
    public bool isDead = false;

    // Privados 

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;

    private CharacterController controller;
    private Transform myCamera;
    private float yVelocity;
    private float rotationX = 0f;
    private Vector3 moveDirection;
    private bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        myCamera = Camera.main.transform;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Vida Player
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (transform.position.y <= -5)
        {
            SceneManager.LoadScene("DeathScene");
        }

        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);
        myCamera.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckDistance, groundMask);

        if (isGrounded)
        {
            Vector3 forward = transform.forward;
            Vector3 right = transform.right;
            Vector3 inputDir = (right * horizontal + forward * vertical).normalized;

            //if (inputDir.magnitude > 0.1f)
            //    moveDirection = inputDir;
            //else
            //    moveDirection = Vector3.zero;

            moveDirection = inputDir;

            //if (yVelocity < 0)
            //    yVelocity = -0.5f;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                yVelocity = Mathf.Sqrt(jumpForce * -2f * gravity);
                //if (moveDirection == Vector3.zero)
                //    moveDirection = Vector3.zero;
            }
        }

        // Gravidade
        yVelocity += gravity * Time.deltaTime;

        // Movimento final
        Vector3 moveXZ = moveDirection * speed;
        Vector3 finalMove = new Vector3(moveXZ.x, yVelocity, moveXZ.z);
        controller.Move(finalMove * Time.deltaTime);

        if (!isGrounded)
        {
            horizontal = 0;
            vertical = 0;
        }

        

        if (currentHealth <= 0)
        {
            isDead = true;
        }


        if (isDead)
        {
            SceneManager.LoadScene(3);

            Debug.Log("MORREU");
        }
    }
    void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckDistance);
    }
}