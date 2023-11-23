using UnityEngine;

public class InteractionController : MonoBehaviour
{
    [SerializeField] float interactionRange = 2f;
    [SerializeField] float interactionRadius = 0.2f;
    [SerializeField] LayerMask interactionLayer;

    [SerializeField] GameObject rodObject;
    [SerializeField] GameObject duckObject;
    [SerializeField] GameObject duckObjectKey;
    [SerializeField] GameObject duckPrefab;
    [SerializeField] GameObject duckKey;

    bool hasRod;
    bool hasDuck;

    [SerializeField] ItemSO rodItem;
    [SerializeField] ItemSO duckItem;
    [SerializeField] ItemSO tempDuckKeyItem;

    void ItemAdded(int itemID)
    {
        if (itemID == rodItem.ID)
        {
            rodObject.SetActive(true);
            hasRod = true;
        }
        else if (itemID == duckItem.ID)
        {
            duckObject.SetActive(true);
            hasDuck = true;
        }
        else if (itemID == tempDuckKeyItem.ID) duckObjectKey.SetActive(true);
    }

    void ItemRemoved(int itemID)
    {
        if (itemID == duckItem.ID)
        {
            duckObject.SetActive(false);
            hasDuck = false;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (hasRod && hasDuck)
            {
                if (GameManager.Singleton.ItemManager.HasItem(tempDuckKeyItem))
                {
                    GameManager.Singleton.ItemManager.RemoveItem(tempDuckKeyItem);

                    Instantiate(duckKey, duckObject.transform.position, duckObject.transform.rotation);
                    rodObject.SetActive(false);
                    hasRod = false;
                }
                else Instantiate(duckPrefab, duckObject.transform.position, duckObject.transform.rotation);
                
                GameManager.Singleton.ItemManager.RemoveAllItemsOfType(duckItem);
                return;
            }

            if (Physics.SphereCast(GameData.Player.CameraTransform.position, interactionRadius, GameData.Player.CameraTransform.forward, out RaycastHit hit, interactionRange, interactionLayer))
            {
                if (hit.transform.TryGetComponent(out IInteractable interactable))
                {
                    if (interactable.CanInteract()) Debug.Log("Interacted");
                    else Debug.Log("Couldn't interact");

                    if (interactable.CanInteract()) interactable.Interact();
                }
                else if (Physics.Raycast(GameData.Player.CameraTransform.position, GameData.Player.CameraTransform.forward, out hit, interactionRange, interactionLayer))
                {
                    if (hit.transform.TryGetComponent(out interactable))
                    {
                        if (interactable.CanInteract()) Debug.Log("Interacted");
                        else Debug.Log("Couldn't interact");

                        if (interactable.CanInteract()) interactable.Interact();
                    }
                }
            }
        }
    }


    void OnEnable()
    {
        GameManager.Singleton.ItemManager.OnItemAdded += ItemAdded;
        GameManager.Singleton.ItemManager.OnItemRemoved += ItemRemoved;
    }

    void OnDisable()
    {
        GameManager.Singleton.ItemManager.OnItemAdded -= ItemAdded;
        GameManager.Singleton.ItemManager.OnItemRemoved -= ItemRemoved;
    }

    void OnDrawGizmos()
    {
        if (GameData.Player.CameraTransform == null) return;

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(GameData.Player.CameraTransform.position, GameData.Player.CameraTransform.forward * interactionRange);

        if (Physics.SphereCast(GameData.Player.CameraTransform.position, interactionRadius, GameData.Player.CameraTransform.forward, out RaycastHit hit, interactionRange, interactionLayer))
        {
            Vector3 point = hit.point;

            if (hit.transform.TryGetComponent(out IInteractable interactable))
            {
                Gizmos.color = Color.green;
            }
            else if (Physics.Raycast(GameData.Player.CameraTransform.position, GameData.Player.CameraTransform.forward, out hit, interactionRange, interactionLayer))
            {
                point = hit.point;

                if (hit.transform.TryGetComponent(out interactable))
                {
                    Gizmos.color = Color.green;
                }
            }

            Gizmos.DrawWireSphere(point - GameData.Player.CameraTransform.forward * interactionRadius, interactionRadius);
        }
    }
}
