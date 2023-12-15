using UnityEngine.UI;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    [SerializeField] float interactionRange = 2f;
    [SerializeField] float interactionRadius = 0.2f;
    [SerializeField] LayerMask interactionLayer;

    [Header("Viewmodel")]
    [SerializeField] GameObject rodViewmodel;
    [SerializeField] GameObject duckViewmodel;
    [SerializeField] GameObject keyViewmodel;

    [Header("Prefabs")]
    [SerializeField] GameObject duckPrefab;
    [SerializeField] GameObject keyPrefab;

    bool hasRod;
    bool hasDuck;

    [Header("Items")]
    [SerializeField] ItemSO rodItem;
    [SerializeField] ItemSO duckItem;
    [SerializeField] ItemSO tempDuckKeyItem;

    InteractionActivator interactable;
    InteractionActivator lastInteractable;
    float interactionTime;

    [Header("UI")]
    [SerializeField] Color normalColor;
    [SerializeField] Color cantInteractColor;
    [SerializeField] RectTransform crosshair;
    [SerializeField] Image crosshairDot;
    [SerializeField] RectTransform interactionIndicator;
    [SerializeField] RectTransform interactionIndicatorDot;

    bool holdingInteract;


    void ItemAdded(int itemID)
    {
        if (itemID == rodItem.ID)
        {
            rodViewmodel.SetActive(true);
            hasRod = true;
        }
        else if (itemID == duckItem.ID)
        {
            duckViewmodel.SetActive(true);
            hasDuck = true;
        }
        else if (itemID == tempDuckKeyItem.ID) keyViewmodel.SetActive(true);
    }

    void ItemRemoved(int itemID)
    {
        if (itemID == duckItem.ID)
        {
            duckViewmodel.SetActive(false);
            hasDuck = false;
        }
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameData.Paused) GameManager.Singleton.UnPause();
            else GameManager.Singleton.Pause();
        }


        if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0)) holdingInteract = true;
        else if (Input.GetKeyUp(KeyCode.E) || Input.GetMouseButtonUp(0)) holdingInteract = false;

        if (holdingInteract && GameData.DisplayingItem)
        {
            GameManager.Singleton.UIManager.StopDisplayItem();
            holdingInteract = false;
            return;
        }

        if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
        {
            if (hasRod && hasDuck)
            {
                if (GameManager.Singleton.ItemManager.HasItem(tempDuckKeyItem))
                {
                    GameManager.Singleton.ItemManager.RemoveItem(tempDuckKeyItem);

                    GameObject key = Instantiate(keyPrefab, duckViewmodel.transform.position, duckViewmodel.transform.rotation);
                    key.AddComponent<Rigidbody>();

                    rodViewmodel.SetActive(false);
                    hasRod = false;
                }
                else Instantiate(duckPrefab, duckViewmodel.transform.position, duckViewmodel.transform.rotation);

                GameManager.Singleton.ItemManager.RemoveAllItemsOfType(duckItem);
                holdingInteract = false;

                return;
            }
        }
        else if (Physics.SphereCast(GameData.Player.CameraTransform.position, interactionRadius, GameData.Player.CameraTransform.forward, out RaycastHit hit, interactionRange, interactionLayer))
        {
            if (hit.transform.TryGetComponent(out interactable))
            {
                if (interactable != lastInteractable) interactionTime = 0f;
                if (holdingInteract && interactable.CanInteract()) interactionTime += Time.deltaTime;
                else interactionTime = 0f;
            }
            else if (Physics.Raycast(GameData.Player.CameraTransform.position, GameData.Player.CameraTransform.forward, out hit, interactionRange, interactionLayer))
            {
                if (hit.transform.TryGetComponent(out interactable))
                {
                    if (interactable != lastInteractable) interactionTime = 0f;
                    if (holdingInteract && interactable.CanInteract()) interactionTime += Time.deltaTime;
                    else interactionTime = 0f;
                }
                else interactable = null;
            }
            else interactable = null;

            if (interactable != null && holdingInteract && interactable.CanInteract() && interactionTime >= interactable.interactionTime)
            {
                interactable.Interact();
                holdingInteract = false;
                interactionTime = 0f;
            }
        }
        else
        {
            interactionTime = 0f;
            interactable = null;
        }

        if (interactable && interactable.CanInteract())
        {
            //looking at interactable and can interact

            interactionIndicator.gameObject.SetActive(true);
            crosshair.gameObject.SetActive(false);

            Vector3 baseSize = Vector3.one * 0.9f;
            float sizeMultiplier = interactionTime / (interactable.interactionTime == 0f ? 1f : interactable.interactionTime);
            interactionIndicatorDot.localScale = baseSize * sizeMultiplier;
        }
        else if (interactable) 
        {
            //looking at interactable but can't interact

            interactionIndicator.gameObject.SetActive(false);
            crosshair.gameObject.SetActive(true);
            crosshairDot.color = cantInteractColor;
        }
        else
        {
            //not looking at interactable

            interactionIndicator.gameObject.SetActive(false);
            crosshair.gameObject.SetActive(true);
            crosshairDot.color = normalColor;
        }


        lastInteractable = interactable;
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
