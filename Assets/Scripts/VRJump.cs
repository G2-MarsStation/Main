using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR; // (opcional, mas útil em VR)
using UnityEngine.XR.Interaction.Toolkit; // necessário para InputActionProperty

[RequireComponent(typeof(CharacterController))]
public class VRJump : MonoBehaviour
{
    public InputActionProperty jumpAction; // Botão de pulo
    public float jumpForce = 5f;
    public float gravity = -9.81f;

    private CharacterController characterController;
    private float verticalVelocity = 0f;
    private bool isGrounded;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        isGrounded = characterController.isGrounded;

        if (isGrounded && verticalVelocity < 0f)
        {
            verticalVelocity = -1f; // mantém o personagem colado ao chão
        }

        if (isGrounded && jumpAction.action != null && jumpAction.action.WasPressedThisFrame())
        {

            Debug.Log("pulo");
            verticalVelocity = jumpForce;
        }

        verticalVelocity += gravity * Time.deltaTime;

        Vector3 move = new Vector3(0f, verticalVelocity, 0f);
        characterController.Move(move * Time.deltaTime);
    }
}