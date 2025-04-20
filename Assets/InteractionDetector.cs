using UnityEngine;
using UnityEngine.InputSystem;


public class InteractionDetector : MonoBehaviour
{
    private IInteractable interactableRange = null;
    public GameObject interactionIcon;

    private void Start()
    {
        interactionIcon.SetActive(false);
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            interactableRange?.Interact();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable) && interactable == interactableRange)
        {
            interactableRange = null;
            interactionIcon.SetActive(false);
        }
    }
}
