using UnityEngine;

public class Key : MonoBehaviour, IInteractable
{
    [field:SerializeField] public KeyType Type { get; private set; }

    [ContextMenu("Interact")]
    public void Interact()
    {
        gameObject.SetActive(false);
        GameManager.Singleton.KeyManager.CollectKey(this);
    }

    public void Reset()
    {
        gameObject.SetActive(true);
    }
}
