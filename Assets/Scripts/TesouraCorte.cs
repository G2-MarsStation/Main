using UnityEngine;
using UnityEngine.InputSystem; // necessário se estiver usando o Input System

public class TriggerAnimation : MonoBehaviour
{
    public Animator targetAnimator; // arraste o Animator do objeto aqui
    public string triggerName = "Cortar"; // nome do trigger no Animator

    public InputActionReference inputAction; // arraste o botão desejado do XR Controller aqui

    private void OnEnable()
    {
        inputAction.action.performed += OnButtonPressed;
        inputAction.action.Enable();
    }

    private void OnDisable()
    {
        inputAction.action.performed -= OnButtonPressed;
        inputAction.action.Disable();
    }

    private void OnButtonPressed(InputAction.CallbackContext context)
    {
        if (targetAnimator != null)
        {
            targetAnimator.SetTrigger(triggerName);
        }
    }
}