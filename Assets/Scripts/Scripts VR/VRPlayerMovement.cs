using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(CharacterController))]
public class VRPlayerMovement : MonoBehaviour
{
    public XRNode inputSource = XRNode.LeftHand; // Controle esquerdo
    public float walkSpeed = 2f;
    public float runSpeed = 4f;
    public float gravity = -9.81f;
    public float jumpHeight = 1.2f;

    private float speed;
    private Vector2 inputAxis;
    private CharacterController characterController;
    private XROrigin xrOrigin;

    private float fallingSpeed;
    private bool isGrounded;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        xrOrigin = GetComponent<XROrigin>();
        if (xrOrigin == null)
            Debug.LogError("XROrigin component not found on the GameObject.");
    }

    //void Update()
    //{
    //    InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
    //    if (!device.isValid) return;

    //    device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);

    //    isGrounded = characterController.isGrounded;

    //    if (isGrounded && fallingSpeed < 0)
    //        fallingSpeed = 0f;

    //    // Correr com gatilho esquerdo (L2)
    //    bool isRunning = false;
    //    if (device.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
    //    {
    //        isRunning = triggerValue > 0.1f;
    //    }

    //    if (device.TryGetFeatureValue(CommonUsages.primaryButton, out bool button))
    //    {
    //        if (button)
    //        {
    //            Jump();
    //        }
    //    }

    //    speed = isRunning ? runSpeed : walkSpeed;
    //}
    private bool lastButtonState = false;
    List<InputFeatureUsage> features = new List<InputFeatureUsage>();

    void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        if (!device.isValid) return;

        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);

        isGrounded = characterController.isGrounded;

        if (isGrounded && fallingSpeed < 0)
            fallingSpeed = 0f;

        // Correr com o gatilho
        bool isRunning = false;
        if (device.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            isRunning = triggerValue > 0.1f;
        }

        // Pular com botão primário
        if (device.TryGetFeatureValue(CommonUsages.primaryButton, out bool button))
        {
            if (button && !lastButtonState)
            {
                Jump();
            }
            lastButtonState = button;
        }

        if (device.TryGetFeatureUsages(features))
        {
            foreach (var feature in features)
            {
                if (feature.type == typeof(bool))
                {
                    if (device.TryGetFeatureValue(feature.As<bool>(), out bool value) && value)
                    {
                        Debug.Log($"Botão pressionado: {feature.name}");
                    }
                }
            }
        }

        var rightHand = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        if (rightHand.TryGetFeatureValue(new InputFeatureUsage<bool>("PrimaryButton"), out bool pressed) && pressed)
        {
            Debug.Log("Botão A ou X pressionado (PrimaryButton)");
        }

        if (rightHand.TryGetFeatureValue(new InputFeatureUsage<bool>("SecondaryButton"), out bool secPressed) && secPressed)
        {
            Debug.Log("Botão B ou Y pressionado (SecondaryButton)");
        }

        if (rightHand.TryGetFeatureValue(new InputFeatureUsage<bool>("button1"), out bool b1) && b1)
        {
            Debug.Log("button1 pressionado");
        }

        if (rightHand.TryGetFeatureValue(new InputFeatureUsage<bool>("button2"), out bool b2) && b2)
        {
            Debug.Log("button2 pressionado");
        }

        speed = isRunning ? runSpeed : walkSpeed;
    }


    void FixedUpdate()
    {
        if (xrOrigin == null) return;

        Quaternion headYaw = Quaternion.Euler(0, xrOrigin.Camera.transform.eulerAngles.y, 0);
        Vector3 direction = headYaw * new Vector3(inputAxis.x, 0, inputAxis.y);

        characterController.Move(direction * speed * Time.fixedDeltaTime);

        fallingSpeed += gravity * Time.fixedDeltaTime;
        characterController.Move(Vector3.up * fallingSpeed * Time.fixedDeltaTime);
    }

    public void Jump()
    {
        if (isGrounded)
        {
            Debug.Log("PULOU!");
            fallingSpeed = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
}