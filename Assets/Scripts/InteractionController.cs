using UnityEngine;

public class InteractionController : MonoBehaviour
{
    [SerializeField] float interactionRange = 2f;
    [SerializeField] float interactionRadius = 0.2f;
    [SerializeField] LayerMask interactionLayer;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.SphereCast(GameData.Player.CameraTransform.position, interactionRadius, GameData.Player.CameraTransform.forward, out RaycastHit hit, interactionRange, interactionLayer))
            {
                if (hit.transform.TryGetComponent(out IInteractable interactable)) interactable.Interact();
            }
        }
    }

    void OnDrawGizmos()
    {
        if (GameData.Player.CameraTransform == null) return;

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(GameData.Player.CameraTransform.position - GameData.Player.CameraTransform.up * 0.01f, GameData.Player.CameraTransform.forward * interactionRange);

        if (Physics.SphereCast(GameData.Player.CameraTransform.position, interactionRadius, GameData.Player.CameraTransform.forward, out RaycastHit hit, interactionRange, interactionLayer))
        {
            if (hit.transform.TryGetComponent(out IInteractable interactable)) Gizmos.color = Color.green;
            else Gizmos.color = Color.red;

            Gizmos.DrawWireSphere(hit.point - GameData.Player.CameraTransform.forward * interactionRadius, interactionRadius);
        }
    }
}
