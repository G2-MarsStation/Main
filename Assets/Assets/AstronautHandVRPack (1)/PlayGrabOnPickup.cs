using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class PlayGrabOnPickup : MonoBehaviour
{
    public Animator hands;
    public Animator handsright;

    private XRGrabInteractable grabInteractable;

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
    }

    private void OnEnable()
    {
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    private void OnDisable()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrab);
        grabInteractable.selectExited.RemoveListener(OnRelease);
    }

    private void OnGrab(SelectEnterEventArgs args)

    {

        Debug.Log("OnGrab chamado por: " + args.interactorObject.transform.name);

        if (args.interactorObject.transform.name.Contains("Left"))
        {
            hands.SetTrigger("Grab");
        }
        else if (args.interactorObject.transform.name.Contains("Right"))
        {
            handsright.SetTrigger("Grab");
        }
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        if (args.interactorObject.transform.name.Contains("Left"))
        {
            hands.SetTrigger("Release");
        }
        else if (args.interactorObject.transform.name.Contains("Right"))
        {
            handsright.SetTrigger("Release");
        }
    }
}
